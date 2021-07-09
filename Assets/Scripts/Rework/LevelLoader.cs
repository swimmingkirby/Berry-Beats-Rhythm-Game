using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace BerryBeats.Rework
{
    public class LevelLoader : MonoBehaviour
    {
        //! Variables
        #region Private Variables
        [Header("Components")]
        [SerializeField] private GameObject notePrefab;
        [SerializeField] private string fileName = "";


        [Header("Properties")]
        [SerializeField] private int poolSize;
        [SerializeField] private Vector2 offset;
        [SerializeField] private Vector2 scale;
        #endregion

        #region Cache
        BinaryFormatter formatter;
        private Vector2[] coords;
        private GameObject[] pool;
        private int noteIndex;
        #endregion

        private const string PATH = "Assets/Levels/";

        //! Methods
        #region Unity Methods
        private void Start()
        {
            pool = new GameObject[poolSize];
            noteIndex = 0;

            initialize();
            LoadLevel();
        }
        #endregion

        #region Public Methods
        public void DestroyNote(GameObject note)
        {
            note.SetActive(false);

            if(noteIndex < coords.Length)
            {
                note.transform.localPosition = coords[noteIndex] * scale - offset;
                note.SetActive(true);
                noteIndex++;
            }
        }

        private void CreateNote(Vector2 position)
        {
            for (int i = 0; i < poolSize; i++)
            {
                if (pool[i].activeInHierarchy)
                {
                    continue;
                }
                else
                {
                    pool[i].transform.localPosition = position * scale - offset;
                    pool[i].SetActive(true);
                    pool[i].GetComponent<Note>().Reposition();
                    noteIndex++;
                    break;
                }
            }
        }
        #endregion

        public int LevelSize()
        {
            if(coords != null)
                return coords.Length;
            else
            {
                return 0;
            }
        }

        #region Private Methods
        private void initialize()
        {
            if (fileName.Equals(""))
            {
                Debug.LogError("File Name Cannot be Empty");
                return;
            }
            try
            {
                fileName = PATH + fileName.Trim() + ".sng";

                if (File.Exists(fileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream fs = new FileStream(fileName, FileMode.Open);
                    Level level = formatter.Deserialize(fs) as Level;
                    fs.Close();
                    performLoad(level.LevelArray);
                }
                else
                {
                    Debug.Log("File Not Found");
                    performLoad(new ArrayPosition[0]);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private void LoadLevel()
        {
            for(int i=0; i < poolSize; i++)
            {
                CreateNote(coords[i]);
            }
        }

        private void performLoad(ArrayPosition[] _layout)
        {
            foreach (GameObject child in pool)
            {
                Destroy(child); //Reset
            }

            coords = new Vector2[_layout.Length];
 
            for (int i = 0; i < _layout.Length; i++)
            {
                coords[i] = new Vector2(_layout[i].x, _layout[i].y);
            }

            for (int i = 0; i < poolSize; i++)
            {
                pool[i] = Instantiate(notePrefab, this.transform);
                pool[i].SetActive(false);
            }
        }
        #endregion
    }
}
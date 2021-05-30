using UnityEngine;
using System.Collections.Generic;

namespace BerryBeats.Rework
{
    public class LevelLoader : MonoBehaviour
    {
        //! Variables
        #region Private Variables
        [Header("Components")]
        [SerializeField] private Texture2D levelImage;
        [SerializeField] private GameObject notePrefab;

        [Header("Properties")]
        [SerializeField] private int poolSize;
        [SerializeField] private Vector2 offset;
        #endregion

        #region Cache
        private List<Vector2> coords;
        private GameObject[] pool;
        private int noteIndex;
        #endregion

        //! Methods
        #region Unity Methods
        private void Start()
        {
            coords = new List<Vector2>();
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

            if(noteIndex < coords.Count - 1)
            {
                note.transform.position = coords[noteIndex] + (Vector2)transform.position;
                note.SetActive(true);
                noteIndex++;
            }
        }
        #endregion

        #region Private Methods
        private void initialize()
        {
            for (int y = 0; y < levelImage.height; y++)
            {
                for (int x = 0; x < levelImage.width; x++)
                {
                    if (levelImage.GetPixel(x, y) == Color.black)
                    {
                        coords.Add(new Vector2(x, y) + offset);
                    }
                }
            }

            for (int i = 0; i < poolSize; i++)
            {
                pool[i] = Instantiate(notePrefab, this.transform);
                pool[i].SetActive(false);
            }
        }

        private void LoadLevel()
        {
            for(int i=0; i < poolSize; i++)
            {
                CreateNote(coords[i]);
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
                    pool[i].transform.position = position + (Vector2)transform.position;
                    pool[i].SetActive(true);
                    noteIndex++;
                    break;
                }
            }
        }
        #endregion
    }
}
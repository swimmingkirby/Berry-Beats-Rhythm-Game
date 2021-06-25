using UnityEngine;
using BerryBeats.Rework;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

namespace BerryBeats.Composer
{
    public class ComposerHandler : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform noteHolder;
        [SerializeField] private InputField fileName;

        //! Cache
        private RaycastHit2D hit;
        private Note selectedArrow;
        private int selectedIndex;
        private List<Note> notes;
        BinaryFormatter formatter;

        private const string NOTE_TAG = "Note";
        private const string BG_TAG = "BG";
        private const string PATH = "Assets/Levels/";
        private string currentPath = "";

        private void Start()
        {
            formatter = new BinaryFormatter();
            notes = new List<Note>();
        }

        #region Unity Methods
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SpawnNote();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                DragStart();
            }
            else if (selectedArrow != null && Input.GetKey(KeyCode.Mouse1))
            {
                PerfromDrag();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                DragEnd();
            }
        }
        #endregion

        #region Spawn_Move
        private void SpawnNote()
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!hit || !hit.collider.CompareTag(BG_TAG))
                return;
            Transform t = Instantiate(arrowPrefab, noteHolder).transform;
            t.position = new Vector2(Mathf.RoundToInt(hit.point.x), hit.point.y);
            Note note = t.GetComponent<Note>();
            notes.Add(note);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(t.position, .3f);
            foreach (Collider2D c in colliders)
            {
                if (c == this.GetComponent<Collider2D>() || !c.CompareTag(NOTE_TAG))
                {
                    continue;
                }
                else
                {
                    notes.Remove(c.GetComponent<Note>());
                    Destroy(c.gameObject);
                }
            }

            note.Reposition();
        }

        void DragStart()
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (!hit.collider.CompareTag(NOTE_TAG))
                return;
            selectedArrow = hit.transform.GetComponent<Note>();
        }

        void DragEnd()
        {
            if (!selectedArrow)
                return;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(selectedArrow.transform.position, .3f);

            foreach (Collider2D c in colliders)
            {
                if (c == selectedArrow.GetComponent<Collider2D>() || !c.CompareTag(NOTE_TAG))
                {
                    continue;
                }
                else
                {
                    notes.Remove(c.GetComponent<Note>());
                    Destroy(c.gameObject);
                }
            }
            selectedArrow.Reposition();
            notes.ToArray()[notes.IndexOf(selectedArrow)].transform.position = selectedArrow.transform.position;
            selectedArrow = null;
        }

        void PerfromDrag()
        {
            Vector2 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tempPos.x = Mathf.Round(tempPos.x);

            selectedArrow.transform.position = tempPos;
        }
        #endregion

        public void LoadLevel()
        {
            if(fileName.text.Equals(""))
            {
                Debug.LogError("File Name Cannot be Empty");
                return;
            }
            try
            {
                currentPath = PATH + fileName.text.Trim() + ".sng";
                if (File.Exists(currentPath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream fs = new FileStream(currentPath, FileMode.Open);

                    Level level = formatter.Deserialize(fs) as Level;
                    fs.Close();
                    performLoad(level.LevelArray);
                    Debug.Log("Level Loaded Successfully");
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

        public void SaveLevel()
        {
            if (fileName.text.Equals(""))
            {
                Debug.LogError("File Name Cannot be Empty");
                return;
            }
            try
            {
                currentPath = PATH + fileName.text.Trim() + ".sng";
                if (!Directory.Exists("Levels"))
                    Directory.CreateDirectory("Levels");

                FileStream fs = new FileStream(currentPath, FileMode.Create);

                Level level = new Level(notes.ToArray());
                formatter.Serialize(fs, level);
                Debug.Log("Level Saved Successfully");
                fs.Close();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }

        }

        private void performLoad(ArrayPosition[] _layout)
        {
            foreach(Transform child in noteHolder)
            {
                Destroy(child.gameObject);
            }
            notes = new List<Note>();

            foreach (ArrayPosition pos in _layout)
            {
                Note note = Instantiate(arrowPrefab, noteHolder).GetComponent<Note>();
                note.transform.position = new Vector3(pos.x, pos.y, 0);
                note.Reposition();
                notes.Add(note);
            }
        }
    }
}
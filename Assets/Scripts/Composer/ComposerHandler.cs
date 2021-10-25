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
        [SerializeField] private Transform cameraHolder;
        [SerializeField] private GameObject notePrefab, longnotePrefab;
        [SerializeField] private Transform noteHolder, longNoteHolder, noteHolder_R, longNoteHolder_R;
        [SerializeField] private InputField fileName;
        [SerializeField] private bool longnoteMode;

        //! Cache
        private RaycastHit2D hit;
        private GameObject arrowPrefab;
        private Note selectedArrow;
        private int selectedIndex;
        private List<Note> notes, notes_R, normalNotes, longNotes, normalNotes_R, longNotes_R;
        private Transform currentHolder, currentHolder_R;
        BinaryFormatter formatter;
        private float scale;
        private string NOTE_TAG;

        private const string NOTE_NORMAL_TAG = "Note";
        private const string NOTE_LONG_TAG = "LongNote";
        private const string BG_TAG = "BG";
        private const string PATH = "Assets/Levels/";
        private string currentPath = "";

        private void Start()
        {
            formatter = new BinaryFormatter();
            normalNotes = new List<Note>();
            normalNotes_R = new List<Note>();
            longNotes = new List<Note>();
            longNotes_R = new List<Note>();
            notes = normalNotes;
            notes_R = normalNotes_R;
            arrowPrefab = notePrefab;
            NOTE_TAG = NOTE_NORMAL_TAG;
            currentHolder = noteHolder;
            currentHolder_R = noteHolder_R;
            scale = notePrefab.GetComponent<Note>().ScaleX();
            cameraHolder.localScale = new Vector3(scale, 1, 1);
            cameraHolder.localPosition = new Vector3(scale / 2, 0, cameraHolder.localPosition.z);
            noteHolder.localScale = longNoteHolder.localScale = noteHolder_R.localScale = longNoteHolder_R.localScale = new Vector3(scale, 1, 1);
        }

        #region Unity Methods
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SpawnNote();
                DragStart();
            }
            else if (selectedArrow != null && Input.GetKey(KeyCode.Mouse0))
            {
                PerfromDrag();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                DragEnd();
            }else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                RemoveNote();
            }
        }
        #endregion

        #region Spawn_Move_Delete
        private void SpawnNote()
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!hit || !hit.collider.CompareTag(BG_TAG))
                return;

            Transform t = Instantiate(arrowPrefab).transform;
            t.localScale = new Vector3(1f, 1f / scale, 1);
            t.position = new Vector2(hit.point.x, hit.point.y);
            Note note = t.GetComponent<Note>();
            bool _right = hit.collider.transform.position.x > 2f;
            t.SetParent(_right? currentHolder_R: currentHolder);
            Reposition(note);
            if (_right) notes_R.Add(note); else  notes.Add(note);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(t.position, scale * .4f);
            foreach (Collider2D c in colliders)
            {
                if (c == this.GetComponent<Collider2D>() || !c.CompareTag(NOTE_TAG))
                    continue;
                if (_right)
                {
                    if (notes_R.Contains(c.GetComponent<Note>()))
                        notes_R.Remove(c.GetComponent<Note>());
                }
                else
                {
                    if (notes.Contains(c.GetComponent<Note>()))
                        notes.Remove(c.GetComponent<Note>());
                }

                Destroy(c.gameObject);
            }
        }

        void DragStart()
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (!hit.collider || !hit.collider.CompareTag(NOTE_TAG))    
                return;
            selectedArrow = hit.transform.GetComponent<Note>();
        }

        void DragEnd()
        {
            if (!selectedArrow)
                return;
            bool _right = selectedArrow.transform.position.x > 2f;
            selectedArrow.transform.SetParent(_right?currentHolder_R: currentHolder);
            //Select nearby arrows for deletion
            Collider2D[] colliders = Physics2D.OverlapCircleAll(selectedArrow.transform.position, scale * .4f);

            foreach (Collider2D c in colliders)
            {
                if (c == selectedArrow.GetComponent<Collider2D>() || !c.CompareTag(NOTE_TAG))
                    continue;

                if (_right)
                {
                    if (notes_R.Contains(c.GetComponent<Note>()))
                        notes_R.Remove(c.GetComponent<Note>());
                }
                else
                {
                    if (notes.Contains(c.GetComponent<Note>()))
                        notes.Remove(c.GetComponent<Note>());
                }

                Destroy(c.gameObject);
            }
            selectedArrow.Reposition2();

            //Removing the selected Note from both as the parent is unknown
            if(notes.Contains(selectedArrow)) notes.Remove(selectedArrow);
            if(notes_R.Contains(selectedArrow)) notes_R.Remove(selectedArrow);

            if (_right)
                notes_R.Add(selectedArrow);
            else
                notes.Add(selectedArrow);

            selectedArrow = null;
        }

        void PerfromDrag()
        {
            //TODO: Limit Moving within own range
            Vector2 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedArrow.transform.position = tempPos;
            Reposition(selectedArrow);
        }

        private void RemoveNote()
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (!hit.collider || !hit.collider.CompareTag(NOTE_TAG))
                return;
            selectedArrow = hit.transform.GetComponent<Note>();
            if (notes.Contains(selectedArrow))
                notes.Remove(selectedArrow);
            else
                notes_R.Remove(selectedArrow);
            Destroy(selectedArrow.gameObject);
        }
        #endregion

        public void LoadLevel(bool right = false)
        {
            if (fileName.text.Equals(""))
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
                    performLoad(level.LevelArray, right);
                    Debug.Log("Level Loaded Successfully");
                }
                else
                {
                    Debug.Log("File Not Found");
                    performLoad(new ArrayPosition[0], right);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public void SaveLevel(bool right = false)
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

                var nh = right ? noteHolder_R : noteHolder;
                Level level = new Level(nh.GetComponentsInChildren<Note>());
                if (right)
                    for (int i = 0; i < level.LevelArray.Length; ++i)
                        level.LevelArray[i].x -= 4f;
                // Get Notes from the scene. notes no longer needed.
                formatter.Serialize(fs, level);
                Debug.Log("Level Saved Successfully");
                fs.Close();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public void setLongNoteMode(bool val)
        {
            longNoteHolder.localPosition = Vector3.forward * 5;
            noteHolder.localPosition = Vector3.forward * 5;
            longNoteHolder_R.localPosition = Vector3.forward * 5;
            noteHolder_R.localPosition = Vector3.forward * 5;
            longnoteMode = val;
            arrowPrefab = longnoteMode ? longnotePrefab : notePrefab;
            notes = longnoteMode ? longNotes : normalNotes;
            notes_R = longnoteMode ? longNotes_R : normalNotes_R;
            NOTE_TAG = longnoteMode ? NOTE_LONG_TAG : NOTE_NORMAL_TAG;
            currentHolder = longnoteMode ? longNoteHolder : noteHolder;
            currentHolder_R = longnoteMode ? longNoteHolder_R : noteHolder_R;
            currentHolder.localPosition = currentHolder_R.localPosition = Vector3.zero;
        }

        private void performLoad(ArrayPosition[] _layout, bool right = false)
        {
            var nh = right ? currentHolder_R : currentHolder;

            foreach (Note child in nh)
                Destroy(child.gameObject);

            if (right)
                for (int i = 0; i < _layout.Length; i++)
                    _layout[i].x += 4f;

            foreach (ArrayPosition pos in _layout)
            {
                Note note = Instantiate(arrowPrefab, nh).GetComponent<Note>();
                note.transform.localPosition = new Vector3(pos.x, pos.y, 0);
                note.transform.localScale = new Vector3(1 / scale, 1 / scale, 1);
                note.Reposition2();
                if (right)
                    notes_R.Add(note);
                else
                    notes.Add(note);
            }
        }

        private void Reposition(Note note)
        {
            note.transform.localPosition = new Vector3(Mathf.RoundToInt(note.transform.localPosition.x), note.transform.localPosition.y, note.transform.localPosition.z);
            note.GetComponent<Note>().Reposition2();
        }
    }
}
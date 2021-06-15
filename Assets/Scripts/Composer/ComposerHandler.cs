using UnityEngine;
using BerryBeats.Rework;
using System.Collections.Generic;

namespace BerryBeats.Composer
{
    public class ComposerHandler : MonoBehaviour
    {

        [Header("Component")]
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform noteHolder;
        [SerializeField] private Level levelLayout;

        //! Cache
        private RaycastHit2D hit;
        private Note selectedArrow;
        private int selectedIndex;
        private List<Note> notes;

        private const string tagname = "Note";

        private void Start()
        {
            notes = new List<Note>();
            Vector3[] _layout = levelLayout.LoadLevel();
            foreach (Vector3 pos in _layout)
            {
                Note note = Instantiate(arrowPrefab).GetComponent<Note>();
                note.transform.position = pos;
                note.Reposition();
                notes.Add(note);
            }
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

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                levelLayout.SaveLevel(notes.ToArray());
            }
        }
        #endregion

        #region Spawn_Move
        private void SpawnNote()
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.CompareTag(tagname))
                return;
            Transform t = Instantiate(arrowPrefab, noteHolder).transform;
            t.position = new Vector2(Mathf.RoundToInt(hit.point.x), hit.point.y);
            Note note = t.GetComponent<Note>();
            notes.Add(note);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(t.position, .3f);
            foreach (Collider2D c in colliders)
            {
                if (c == this.GetComponent<Collider2D>() || !c.CompareTag(tagname))
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
            if (!hit.collider.CompareTag(tagname))
                return;
            selectedArrow = hit.transform.GetComponent<Note>();
        }

        void DragEnd()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(selectedArrow.transform.position, .3f);

            foreach (Collider2D c in colliders)
            {
                if (c == selectedArrow.GetComponent<Collider2D>() || !c.CompareTag(tagname))
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
    }
}
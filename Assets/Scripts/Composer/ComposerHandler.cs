using UnityEngine;

namespace BerryBeats.Composer
{
    public class ComposerHandler : MonoBehaviour
    {

        [Header("Component")]
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform noteHolder;

        //! Cache
        private RaycastHit2D hit;
        private Transform selectedArrow;

        private const string tagname = "ComposerNote";

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
            if (hit.collider.CompareTag(tagname))
                return;
            Transform t = Instantiate(arrowPrefab, noteHolder).transform;
            t.position = new Vector2(Mathf.RoundToInt(hit.point.x), hit.point.y);
        }

        void DragStart()
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (!hit.collider.CompareTag(tagname))
                return;
            selectedArrow = hit.transform;
        }

        void DragEnd()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(selectedArrow.position, .3f);

            foreach (Collider2D c in colliders)
            {
                if (c == selectedArrow.GetComponent<Collider2D>() || !c.CompareTag(tagname))
                {
                    continue;
                }
                else
                {
                    Destroy(c.gameObject);
                }
            }
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
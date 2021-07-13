using UnityEngine;

namespace BerryBeats.Rework
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class NoteDetector : MonoBehaviour
    {
        //! Variables
        #region Private Variables
        [Header("Components")]
        [SerializeField] LevelLoader levelLoader;
        private SpriteRenderer spriteRenderer;

        [Header("Properties")]
        [SerializeField] private Sprite defaultImage;
        [SerializeField] private Sprite pressedImage;

        [SerializeField] private KeyCode keyToPress;
        [SerializeField] private bool player1 = true;

        private Note focusedNote;
        #endregion

        //! Methods
        #region Unity Methods
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = defaultImage;       //? Failsafe
        }

        private void Update()
        {
            HandleDetection();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Note"))
                focusedNote = other.GetComponent<Note>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Note") && focusedNote != null)
            {
                focusedNote = null;
            }
        }
        #endregion

        #region Private Methods
        private void HandleDetection()
        {
            if (Input.GetKeyDown(keyToPress))
            {
                if (focusedNote != null)
                {
                    NoteHit(focusedNote);
                }
                else
                {
                    EarlyMiss();
                }

                spriteRenderer.sprite = pressedImage;
            }

            if (Input.GetKey(keyToPress))
            {
                if (focusedNote != null && focusedNote.TryGetComponent<LongNote>(out var _))
                    NoteHitL(focusedNote);
            }

            if (Input.GetKeyUp(keyToPress))
            {
                spriteRenderer.sprite = defaultImage;
            }
        }

        private void NoteHit(Note note)
        {
            if (Mathf.Abs(note.transform.position.y - transform.position.y) > 0.25f)
            {
                GameManager2.Instance.NoteHit(HitTypes.REGULAR, player1);
            }
            else if ((Mathf.Abs(note.transform.position.y - transform.position.y) > 0.1f))
            {
                GameManager2.Instance.NoteHit(HitTypes.GOOD, player1);
            }
            else
            {
                GameManager2.Instance.NoteHit(HitTypes.PERFECT, player1);
            }

            levelLoader.DestroyNote(note.gameObject);
        }

        private void NoteHitL(Note note)
        {
            GameManager2.Instance.NoteHit(HitTypes.REGULAR, player1);
            levelLoader.DestroyNote(note.gameObject);
        }

        private void EarlyMiss()
        {
            GameManager2.Instance.NoteMissed(player1);
        }
        #endregion
    }
}
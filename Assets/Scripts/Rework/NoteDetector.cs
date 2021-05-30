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
        [SerializeField] GameManager2 manager;   //TODO: Replace with Singleton call
        private SpriteRenderer spriteRenderer;

        [Header("Properties")]
        [SerializeField] private Sprite defaultImage;
        [SerializeField] private Sprite pressedImage;

        [SerializeField] private KeyCode keyToPress;

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
            if(other.CompareTag("Note"))
                focusedNote = other.GetComponent<Note>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Note") && focusedNote.gameObject == other.gameObject)
            {
                focusedNote = null;
                manager.NoteMissed();
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
                    focusedNote.Hit();
                    manager.NoteHit();
                }
                else
                {
                    manager.NoteMissed();
                }

                spriteRenderer.sprite = pressedImage;
            }
            if (Input.GetKeyUp(keyToPress))
            {
                spriteRenderer.sprite = defaultImage;
            }
        }
        #endregion
    }
}
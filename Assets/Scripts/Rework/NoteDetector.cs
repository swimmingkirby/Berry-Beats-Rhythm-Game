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

        //[SerializeField] private GameObject hitEffect, goodEffect, perfectEffect, missEffect;

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

               // spriteRenderer.sprite = pressedImage;
            }
          /*  if (Input.GetKeyUp(keyToPress))
            {
                spriteRenderer.sprite = defaultImage;
            }
          */
        }

        private void NoteHit(Note note)
        {
            //GameManager2.Instance.NoteHit();
            levelLoader.DestroyNote(note.gameObject);

            if (Mathf.Abs(transform.position.y) > 0.25)
            {
                GameManager.instance.Hit(HitTypes.Regular);
                //Instantiate(hitEffect, hitEffect.transform.position, hitEffect.transform.rotation);

                Debug.Log("Hit!");
            }
            else if ((Mathf.Abs(transform.position.y) > 0.05f))
            {
                GameManager.instance.Hit(HitTypes.Good);
                //Instantiate(goodEffect, goodEffect.transform.position, goodEffect.transform.rotation);

                Debug.Log("Good!");
            }
            else
            {
                GameManager.instance.Hit(HitTypes.Perfect);
                //Instantiate(perfectEffect, perfectEffect.transform.position, perfectEffect.transform.rotation);

                Debug.Log("Perfect!");
            }

        }

        private void EarlyMiss()
        {
            //GameManager2.Instance.NoteMissed();
            Debug.Log("Early Miss!");
            GameManager.instance.NoteMissed();
            //Instantiate(missEffect, missEffect.transform.position, missEffect.transform.rotation);
        }
        #endregion
    }
}
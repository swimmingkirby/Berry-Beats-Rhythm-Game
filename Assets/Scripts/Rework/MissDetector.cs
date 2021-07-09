using UnityEngine;

namespace BerryBeats.Rework
{
    [RequireComponent(typeof(Rigidbody2D))][RequireComponent(typeof(BoxCollider2D))]

    public class MissDetector : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]private LevelLoader levelLoader;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Note"))
            {
                GameManager2.Instance.NoteMissed(true);
                levelLoader.DestroyNote(other.gameObject);
            }            
        }
    }
}
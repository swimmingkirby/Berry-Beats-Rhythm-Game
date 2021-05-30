using UnityEngine;

namespace BerryBeats.Rework
{
    [RequireComponent(typeof(Rigidbody2D))][RequireComponent(typeof(BoxCollider2D))]
    public class MissDetector : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]private LevelLoader levelLoader;
        [SerializeField]private GameManager2 manager;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Note"))
            {
                manager.NoteMissed();
                levelLoader.DestroyNote(other.gameObject);
            }            
        }
    }
}
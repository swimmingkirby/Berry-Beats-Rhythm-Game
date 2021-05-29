using BerryBeats.ScriptableObjects;
using UnityEngine;

namespace BerryBeats.BattleSystem
{
    public enum ArrowDirection
    {
        Up,
        Left,
        Right,
        Down
    }

    [RequireComponent(typeof(CustomAnimator))]
    public class Dancer : MonoBehaviour
    {
        [SerializeField] private DancerObject dancer;
        private CustomAnimator animator;

        void Start()
        {
            animator = GetComponent<CustomAnimator>();
            animator.SetFrames(dancer.FramesIdle);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Hit(ArrowDirection.Up);
            if (Input.GetKeyDown(KeyCode.DownArrow))
                Hit(ArrowDirection.Down);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                Hit(ArrowDirection.Right);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                Hit(ArrowDirection.Left);
        }

        /// <summary>
        /// Used for notesystem and dancer animations
        /// </summary>
        /// <param name="dir"></param>
        public void Hit(ArrowDirection dir)
        {
            animator.SetFrames(dancer.GetFrames(dir));
        }
    }
}
using BerryBeats.ScriptableObjects;
using UnityEngine;

namespace BerryBeats.BattleSystem
{

    public enum ArrowDirection
    {
        Up,
        Left,
        Right,
        Down,
        Idle
    }

    [RequireComponent(typeof(CustomAnimator))]
    public class Dancer : MonoBehaviour
    {
        public float timeLeft = 0.0f;

        [SerializeField] private DancerObject dancer;
        private CustomAnimator animator;

        void Start()
        {
            animator = GetComponent<CustomAnimator>();
            animator.SetFrames(dancer.FramesIdle);
        }

        public void Update()
        {
            timeLeft += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                
                Hit(ArrowDirection.Up);
                timeLeft = 0.0f;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Hit(ArrowDirection.Down);
                timeLeft = 0.0f;

            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Hit(ArrowDirection.Right);
                timeLeft = 0.0f;

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Hit(ArrowDirection.Left);
                timeLeft = 0.0f;

            }

           if (timeLeft > 1f)
            {
                animator.SetFrames(dancer.FramesIdle);
                timeLeft = 0.0f;
                
            }
               
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
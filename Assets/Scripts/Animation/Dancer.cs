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
        Idle,
        UpLeft,
        DownLeft,
        UpRight,
        DownRight,
        UpDown,
        LeftRight
    }

    [RequireComponent(typeof(CustomAnimator))]
    public class Dancer : MonoBehaviour
    {
        public float timeLeft = 0.0f;
        [SerializeField] private KeyCode left, right, up, down;

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
            if (Input.GetKeyDown(up))
            {
                
                Hit(ArrowDirection.Up);
                timeLeft = 0.0f;
            }
            if (Input.GetKeyDown(down))
            {
                Hit(ArrowDirection.Down);
                timeLeft = 0.0f;

            }
            if (Input.GetKeyDown(right))
            {
                Hit(ArrowDirection.Right);
                timeLeft = 0.0f;

            }
            if (Input.GetKeyDown(left))
            {
                Hit(ArrowDirection.Left);
                timeLeft = 0.0f;

            }
            if (Input.GetKeyDown(left) && Input.GetKeyDown(up))
            {
                Hit(ArrowDirection.UpLeft);
                timeLeft = 0.0f;

            }
            if (Input.GetKeyDown(left) && Input.GetKeyDown(down))
            {
                Hit(ArrowDirection.DownLeft);
                timeLeft = 0.0f;

            }
            if (Input.GetKeyDown(right) && Input.GetKeyDown(up))
            {
                Hit(ArrowDirection.UpRight);
                timeLeft = 0.0f;

            }
            if (Input.GetKeyDown(right) && Input.GetKeyDown(down))
            {
                Hit(ArrowDirection.DownRight);
                timeLeft = 0.0f;

            }
            if (Input.GetKeyDown(up) && Input.GetKeyDown(down))
            {
                Hit(ArrowDirection.UpDown);
                timeLeft = 0.0f;

            }
            if (Input.GetKeyDown(left) && Input.GetKeyDown(right))
            {
                Hit(ArrowDirection.LeftRight);
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
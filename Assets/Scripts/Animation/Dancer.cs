using BerryBeats.ScriptableObjects;
using UnityEngine;
using static InputManager;

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

        [SerializeField] private DancerObject dancer;
        private CustomAnimator animator;

        void Start()
        {
            animator = GetComponent<CustomAnimator>();
            animator.SetFrames(dancer.FramesIdle);
        }

        private static ArrowDirection GetDir((BtnPhase l, BtnPhase r, BtnPhase u, BtnPhase d) btns)
        {
            (var l, var r, var u, var d) = btns;
            if (l != BtnPhase.None && d != BtnPhase.None) return ArrowDirection.DownLeft;
            if (l != BtnPhase.None && u != BtnPhase.None) return ArrowDirection.UpLeft;
            if (r != BtnPhase.None && d != BtnPhase.None) return ArrowDirection.DownRight;
            if (r != BtnPhase.None && u != BtnPhase.None) return ArrowDirection.UpRight;
            if (l != BtnPhase.None) return ArrowDirection.Left;
            if (r != BtnPhase.None) return ArrowDirection.Right;
            if (u != BtnPhase.None) return ArrowDirection.Up;
            if (d != BtnPhase.None) return ArrowDirection.Down;
            return ArrowDirection.Idle;
        }

        public void Update()
        {
            timeLeft += Time.deltaTime;

            (var l, var r, var u, var d) = GetInput();
            if (l == BtnPhase.Down || r == BtnPhase.Down || u == BtnPhase.Down || d == BtnPhase.Down)
                Hit(GetDir((l, r, u, d)));

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
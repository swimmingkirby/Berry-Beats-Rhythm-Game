using UnityEngine;
using BerryBeats.BattleSystem;

namespace BerryBeats.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Dancer_", menuName = "Berry Beats!_OBJECTS/Dancer", order = 1)]
    public class DancerObject : ScriptableObject
    {
        [Header("⚠️ CURRENTLY UNUSED")] public string Name;
        [Header("⚠️ CURRENTLY UNUSED")] [Multiline] public string Description;
        [Header("⚠️ CURRENTLY UNUSED")] public Sprite DialogAvatar;
        [Space]
        [Header("Animation Data")]
        public Sprite[] FramesUp;
        public Sprite[] FramesLeft;
        public Sprite[] FramesRight;
        public Sprite[] FramesDown;
        public Sprite[] FramesIdle;
        public Sprite[] FramesUpLeft;
        public Sprite[] FramesDownLeft;
        public Sprite[] FramesUpRight;
        public Sprite[] FramesDownRight;
        public Sprite[] FramesUpDown;
        public Sprite[] FramesLeftRight;

        /// <summary>
        /// Returns the spritearray for the given direction
        /// </summary>
        /// <param name="dir"></param>
        /// <returns><code>Sprite[]</code></returns>
        public Sprite[] GetFrames(ArrowDirection dir)
        {
            switch (dir)
            {
                case ArrowDirection.Up: return FramesUp;
                case ArrowDirection.Down: return FramesDown;
                case ArrowDirection.Right: return FramesRight;
                case ArrowDirection.Left: return FramesLeft;
                case ArrowDirection.UpLeft: return FramesUpLeft;
                case ArrowDirection.UpRight: return FramesUpRight;
                case ArrowDirection.DownLeft: return FramesDownLeft;
                case ArrowDirection.DownRight: return FramesDownRight;
                case ArrowDirection.Idle: return FramesIdle;
                default:
                    break;
            }

            // If switch statement fails, return error
            Debug.LogError("Unknown AnimationDirection, plese check DancerObject.cs");
            return null;
        }
    }
}

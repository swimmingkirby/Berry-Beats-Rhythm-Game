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
                case ArrowDirection.Up:
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        return FramesUpLeft;
                    }
                    else if (Input.GetKey(KeyCode.RightArrow))
                    {
                        return FramesUpRight;
                    }
                    else if (Input.GetKey(KeyCode.DownArrow))
                    {
                        return FramesUpDown;
                    }
                    else
                    {
                        return FramesUp;
                    }
                case ArrowDirection.Left:
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        return FramesLeftRight;
                    }
                    else if (Input.GetKey(KeyCode.UpArrow))
                    {
                        return FramesUpLeft;
                    }
                    else if (Input.GetKey(KeyCode.DownArrow))
                    {
                        return FramesDownLeft;
                    }
                    else
                    {
                        return FramesLeft;
                    }
                case ArrowDirection.Right:
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        return FramesDownRight;
                    }
                    else if (Input.GetKey(KeyCode.UpArrow))
                    {
                        return FramesUpRight;
                    }
                    else if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        return FramesLeftRight;
                    }
                    else
                    {
                        return FramesRight;
                    }
                case ArrowDirection.Down:
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        return FramesDownLeft;
                    }
                    else if (Input.GetKey(KeyCode.RightArrow))
                    {
                        return FramesDownRight;
                    }
                    else if (Input.GetKey(KeyCode.UpArrow))
                    {
                        return FramesUpDown;
                    }
                    else
                    {
                        return FramesDown;
                    }
                default:
                    break;
            }

            // If switch statement fails, return error
            Debug.LogError("Unknown AnimationDirection, plese check DancerObject.cs");
            return null;
        }
    }
}


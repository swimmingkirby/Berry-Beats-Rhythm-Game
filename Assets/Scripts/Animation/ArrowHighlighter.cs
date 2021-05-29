using BerryBeats.BattleSystem;
using UnityEngine;

namespace BerryBeats.BattleSystem
{
    public class ArrowHighlighter : MonoBehaviour
    {
        public GameObject upArrow;
        public GameObject downArrow;
        public GameObject rightArrow;
        public GameObject leftArrow;

        private SpriteRenderer upRenderer;
        private SpriteRenderer downRenderer;
        private SpriteRenderer rightRenderer;
        private SpriteRenderer leftRenderer;

        private Vector3 ArrowScale;
        private GameObject toTween;
        private Vector3 diffirence = new Vector3(0.1f, 0.1f, 0.1f);

        void Start()
        {
            ArrowScale = upArrow.transform.localScale;

            upRenderer = upArrow.GetComponent<SpriteRenderer>();
            downRenderer = downArrow.GetComponent<SpriteRenderer>();
            rightRenderer = rightArrow.GetComponent<SpriteRenderer>();
            leftRenderer = leftArrow.GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                ArrowHit(ArrowDirection.Up);
            if (Input.GetKeyDown(KeyCode.DownArrow))
                ArrowHit(ArrowDirection.Down);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                ArrowHit(ArrowDirection.Right);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                ArrowHit(ArrowDirection.Left);
        }

        /// <summary>
        /// Animates the arrows upon hit
        /// </summary>
        /// <param name="dir"></param>
        public void ArrowHit(ArrowDirection dir)
        {
            SpriteRenderer tRenderer = null;

            switch (dir)
            {
                case ArrowDirection.Up:
                    toTween = upArrow;
                    tRenderer = upRenderer;
                    break;
                case ArrowDirection.Down:
                    toTween = downArrow;
                    tRenderer = downRenderer;
                    break;
                case ArrowDirection.Right:
                    toTween = rightArrow;
                    tRenderer = rightRenderer;
                    break;
                case ArrowDirection.Left:
                    toTween = leftArrow;
                    tRenderer = leftRenderer;
                    break;
                default:
                    break;
            }

            // Animate chosen arrow's scale with LeanTween
            LeanTween.scale(toTween, ArrowScale-diffirence, 0.05f)
                .setEaseOutSine()
                .setLoopPingPong(1);

            // Animate chosen arrow's Intensity Multiplier with LeanTween
            LeanTween.value(toTween, 3, 1, 0.1f)
                .setOnUpdate((float value) =>
                {
                    tRenderer.material.SetFloat("_Multiplier", value);
                }
            );
        }
    }
}

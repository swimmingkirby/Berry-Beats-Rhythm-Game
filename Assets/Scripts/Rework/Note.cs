using UnityEngine;

namespace BerryBeats.Rework
{
    public class Note : MonoBehaviour
    {
        [Header("Components")]
        private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite downSprite;
        [SerializeField] private Sprite upSprite, leftSprite, rightSprite;

        [SerializeField] private float scaleX = 1;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            Reposition();
        }

        public void Reposition()
        {
            switch (transform.localPosition.x / scaleX)
            {
                case -1.5f:
                    spriteRenderer.sprite = leftSprite;
                    break;
                case -.5f:
                    spriteRenderer.sprite = upSprite;
                    break;
                case .5f:
                    spriteRenderer.sprite = downSprite;
                    break;
                case 1.5f:
                    spriteRenderer.sprite = rightSprite;
                    break;
            }
        }
    }
}
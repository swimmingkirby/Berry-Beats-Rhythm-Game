using UnityEngine;

namespace BerryBeats.Rework
{
    public class Note : MonoBehaviour
    {
        [Header("Components")]
        private SpriteRenderer spriteRenderer;

        [Header("Properties")]
        [SerializeField] private Sprite downSprite;
        [SerializeField] private Sprite upSprite, leftSprite, rightSprite;

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
            switch (transform.localPosition.x)
            {
                case -1f:
                    spriteRenderer.sprite = leftSprite;
                    break;
                case -0f:
                    spriteRenderer.sprite = upSprite;
                    break;
                case 1f:
                    spriteRenderer.sprite = downSprite;
                    break;
                case 2f:
                    spriteRenderer.sprite = rightSprite;
                    break;
            }
        }
    }
}
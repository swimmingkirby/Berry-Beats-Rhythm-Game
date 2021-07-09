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

            Sprite _sprite;
            if(transform.localPosition.x / scaleX < -1.1f)
            {
                _sprite = leftSprite;
            }else if (transform.localPosition.x / scaleX < 0f)
            {
                _sprite = upSprite;
            }
            else if (transform.localPosition.x / scaleX < 1.1f)
            {
                _sprite = downSprite;
            }
            else
            {
                _sprite = rightSprite;
            }

            spriteRenderer.sprite = _sprite;
        }

        public void Reposition2()
        {

            Sprite _sprite;

            switch (transform.localPosition.x)
            {
                case -1f:
                    _sprite = leftSprite;
                    break;
                case 0f:
                    _sprite = upSprite;
                    break;
                case 1f:
                    _sprite = downSprite;
                    break;
                case 2f:
                    _sprite = rightSprite;
                    break;
                default:
                    _sprite = downSprite;
                    break;
            }

            spriteRenderer.sprite = _sprite;
        }

        public float ScaleX()
        {
            return scaleX;
        }
    }
}
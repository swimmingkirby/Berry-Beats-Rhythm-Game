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
            switch (transform.position.x)
            {
                case -1.5f:
                    spriteRenderer.sprite = leftSprite;
                    break;
                case -0.5f:
                    spriteRenderer.sprite = upSprite;
                    break;
                case 0.5f:
                    spriteRenderer.sprite = downSprite;
                    break;
                case 1.5f:
                    spriteRenderer.sprite = rightSprite;
                    break;
            }
        }
    }
}
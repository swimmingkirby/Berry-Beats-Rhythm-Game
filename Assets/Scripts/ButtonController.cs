using UnityEngine;

namespace BerryBeats
{
    public class ButtonController : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        public Sprite defaultImage;
        public Sprite pressedImage;

        public KeyCode keyToPress;

        // Start is called before the first frame update
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            // Inline if statement to change the sprite when pressed
            spriteRenderer.sprite = Input.GetKey(keyToPress) ? pressedImage : defaultImage;
        }
    }
}

using UnityEngine;

namespace BerryBeats.Composer
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed;

        private void LateUpdate()
        {
            float moveVector = 0;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                moveVector += scrollSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                moveVector -= scrollSpeed * Time.deltaTime;

            transform.position += Vector3.up * moveVector;
        }
    }
}
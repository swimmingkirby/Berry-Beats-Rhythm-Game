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
            {
                moveVector -= scrollSpeed * Time.deltaTime;
            }

            if(Input.GetAxis("Mouse ScrollWheel") != 0)
                moveVector += Mathf.Sign(Input.GetAxisRaw("Mouse ScrollWheel")) * Time.deltaTime * scrollSpeed;

            if (transform.position.y + moveVector > 0)
                transform.position += Vector3.up * moveVector;
        }
    }
}
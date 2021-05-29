using UnityEngine;

namespace BerryBeats
{
    public class effectObject : MonoBehaviour
    {
        public float lifetime = 1f;

        private void Update()
        {
            Destroy(gameObject, lifetime);
        }
    }
}

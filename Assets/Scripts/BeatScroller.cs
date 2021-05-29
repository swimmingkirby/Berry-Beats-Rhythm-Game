using UnityEngine;

namespace BerryBeats {
    public class BeatScroller : MonoBehaviour
    {
        [Range(40f, 160f)]
        public float bpm;
        public bool hasStarted;

        private void Start()
        {
            bpm /= 60f;
        }

        private void Update()
        {
            if (hasStarted)
            {
                transform.position -= new Vector3(0f, bpm * Time.deltaTime, 0f);
            }
        }
    }
}

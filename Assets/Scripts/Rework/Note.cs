using UnityEngine;

namespace BerryBeats.Rework
{
    public class Note : MonoBehaviour
    {
        //!Methods
        //*Public
        public void Hit()
        {
            gameObject.SetActive(false);
        }
    }
}
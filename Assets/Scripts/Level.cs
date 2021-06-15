using UnityEngine;

namespace BerryBeats.Rework
{
    [CreateAssetMenu(menuName = "ScriptableObjects/LevelLayout", fileName = "LevelLayout")]
    public class Level : ScriptableObject
    {
        [SerializeField] private Vector3[] LevelArray;

        public Vector3[] LoadLevel()
        {
            if (LevelArray != null && LevelArray.Length > 0)
                return LevelArray;
            else
                return new Vector3[0];
        }

        public void SaveLevel(Note[] notes)
        {
            LevelArray = new Vector3[notes.Length];
            for (int i = 0; i< notes.Length; i++)
            {
                LevelArray[i] = notes[i].transform.position;
            }
        }
    }
}
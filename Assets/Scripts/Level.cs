using UnityEngine;

namespace BerryBeats.Rework
{
    [System.Serializable]
    public class Level
    {
        public ArrayPosition[] LevelArray;

        public Level(Note[] notes)
        {
            LevelArray = new ArrayPosition[notes.Length];

            for(int i = 0; i< notes.Length; i++)
            {
                LevelArray[i] = new ArrayPosition(notes[i].transform.position.x, notes[i].transform.position.y);
            }
        }
    }

    [System.Serializable]
    public class ArrayPosition
    {
        public float x;
        public float y;

        public ArrayPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
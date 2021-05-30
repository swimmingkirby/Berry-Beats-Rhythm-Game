using UnityEngine;

namespace BerryBeats.Rework
{
    public class GameManager2 : MonoBehaviour
    {
        //! Variables
        #region Properties
        public int currentScore { get; private set; }
        public int comboCounter { get; private set; }
        #endregion

        //! Methods
        #region public methods
        public void NoteHit()
        {
            currentScore += 1;
            comboCounter += 1;
        }

        public void NoteMissed()
        {
            comboCounter = 0;
        }
        #endregion

        //! Debug
        private void OnGUI()
        {
            GUI.Box(
                new Rect(0,0, 500, 30),
                "Score: " + currentScore + " Combo: " + comboCounter);
        }

    }
}
using UnityEngine;

namespace BerryBeats.Rework
{
    public class GameManager2 : MonoBehaviour
    {
        //! Variables
        #region Components
        [Header("Components")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private GameObject resultScreen;
        [SerializeField] private BeatScroller beatScroller;
        #endregion

        #region Public Variables
        #endregion

        #region Properties
        public int currentScore { get; private set; }
        public int comboCounter { get; private set; }

        public bool isPlaying { get; private set; }
        #endregion

        #region Private Variables
        private int currentMultiplier;

        #endregion

        #region Constants
        private const int SCORE_PER_NOTE = 100;
        private const int SCORE_PER_GOOD_NOTE = 125;
        private const int SCORE_PER_PERFECT_NOTE = 200;
        #endregion

        // Singleton
        public static GameManager2 Instance;

        //! Methods
        #region Unity Methods
        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            currentScore = 0;
            comboCounter = 0;
        }
        #endregion

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
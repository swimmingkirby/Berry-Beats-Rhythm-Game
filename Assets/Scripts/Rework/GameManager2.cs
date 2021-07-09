using UnityEngine;

namespace BerryBeats.Rework
{
    public class GameManager2 : MonoBehaviour
    {
        //! Variables
        #region Components
        [Header("Components")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private ResultScreen resultScreen;
        [SerializeField] private BeatScroller beatScroller;
        [SerializeField] private LevelLoader levelLoader;

        [Header("Effects")]
        [SerializeField] private ParticleSystem hitEffect;
        [SerializeField] private ParticleSystem goodEffect;
        [SerializeField] private ParticleSystem perfectEffect;
        [SerializeField] private ParticleSystem missEffect;

        [Header("Health Bars")]
        [SerializeField] PlayerHealthbar player_healthbar;
        [SerializeField] float health_modifier = 0.05f;
        #endregion

        #region Public Variables
        #endregion

        #region Properties
        public bool isPlaying = false;
        #endregion

        #region Private Variables
        [SerializeField] private Stats currentStats;
        private int levelSize = 0;

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
            isPlaying = false;
            resultScreen.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!isPlaying)
            {
                if (Input.anyKeyDown)
                {
                    isPlaying = true;
                    beatScroller.hasStarted = true;
                    levelSize = levelLoader.LevelSize();
                    musicSource.Play();
                }
            }
            else
            {

                if ((currentStats.totalHits) >= levelSize)
                {
                    CreateEndCard();
                }
            }

        }

        private void CreateEndCard()
        {
            resultScreen.SetScore(currentStats.score);
            resultScreen.SetNormal(currentStats.normalHits);
            resultScreen.SetGood(currentStats.goodHits);
            resultScreen.SetPerfect(currentStats.perfectHits);
            resultScreen.SetMissed(currentStats.missedHits);
            resultScreen.SetPercent(1f - ((float)currentStats.missedHits /(float)levelSize));

            resultScreen.gameObject.SetActive(true);
        }
        #endregion

        #region public methods
        public void NoteHit(HitTypes hitType)
        {
            switch (hitType)
            {
                case HitTypes.REGULAR:
                    currentStats.score += SCORE_PER_NOTE * currentStats.multiplier;
                    currentStats.normalHits += 1;
                    hitEffect.Play();
                    break;
                case HitTypes.GOOD:
                    currentStats.score += SCORE_PER_GOOD_NOTE * currentStats.multiplier;
                    currentStats.goodHits += 1;
                    goodEffect.Play();
                    break;
                case HitTypes.PERFECT:
                    currentStats.score += SCORE_PER_PERFECT_NOTE * currentStats.multiplier;
                    currentStats.perfectHits += 1;
                    perfectEffect.Play();
                    break;
            }
            currentStats.totalHits += 1;
        }

        public void NoteMissed(bool delete = false)
        {
            currentStats.comboCounter = 0;
            currentStats.multiplier = 1;
            currentStats.missedHits += 1;

            missEffect.Play();
            if (delete)
                currentStats.totalHits += 1;             //TODO: To Calculate Level End
                                                            //TODO: Replace with a collider of some sort

            player_healthbar.ModifyHealth(-health_modifier);
        }
        #endregion

        [System.Serializable]
        public class Stats
        {
            public int score;
            public int comboCounter;
            public int multiplier = 1;
            public int normalHits;
            public int goodHits;
            public int perfectHits;
            public int missedHits;
            public int totalHits;
        }
    }
}
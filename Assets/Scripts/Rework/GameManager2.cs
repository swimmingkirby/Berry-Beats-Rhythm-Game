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
        [SerializeField] private BeatScroller beatScroller2;
        [SerializeField] private BeatScroller longBeatScroller;
        [SerializeField] private BeatScroller longBeatScroller2;
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
        [SerializeField] private Stats enemyStats;
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
            if (Instance != null)
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
                    beatScroller2.hasStarted = true;
                    longBeatScroller.hasStarted = true;
                    longBeatScroller2.hasStarted = true;
                    levelSize = levelLoader.LevelSize();
                    musicSource.Play();
                }
            }
            else
            {
                if (!musicSource.isPlaying)
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
            resultScreen.SetPercent(1f - ((float)currentStats.missedHits / (float)levelSize));

            resultScreen.gameObject.SetActive(true);
        }
        #endregion

        #region public methods
        public void NoteHit(HitTypes hitType, bool player1 = true)
        {
            Stats selectedStats = player1 ? currentStats : enemyStats;
            switch (hitType)
            {
                case HitTypes.REGULAR:
                    selectedStats.score += SCORE_PER_NOTE * selectedStats.multiplier;
                    selectedStats.normalHits += 1;
                    if (player1) hitEffect.Play();
                    break;
                case HitTypes.GOOD:
                    selectedStats.score += SCORE_PER_GOOD_NOTE * selectedStats.multiplier;
                    selectedStats.goodHits += 1;
                    if (player1) goodEffect.Play();
                    break;
                case HitTypes.PERFECT:
                    selectedStats.score += SCORE_PER_PERFECT_NOTE * selectedStats.multiplier;
                    selectedStats.perfectHits += 1;
                    if (player1) perfectEffect.Play();
                    break;
            }
            selectedStats.totalHits += 1;

            player_healthbar.ModifyHealth(health_modifier * (player1 ? +1 : 1));
        }

        public void NoteMissed(bool player1 = true, bool delete = false)
        {
            Stats selectedStats = player1 ? currentStats : enemyStats;

            selectedStats.comboCounter = 0;
            selectedStats.multiplier = 1;
            selectedStats.missedHits += 1;

            if (player1) missEffect.Play();
            if (delete)
                selectedStats.totalHits += 1;             //TODO: To Calculate Level End
                                                          //TODO: Replace with a collider of some sort

            player_healthbar.ModifyHealth(health_modifier * (player1 ? -1 : 1));
        }

        public void PauseResume()
        {
            if (Time.timeScale > 0)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
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
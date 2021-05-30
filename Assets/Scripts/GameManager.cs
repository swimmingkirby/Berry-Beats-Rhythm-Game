using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BerryBeats {
    public enum HitTypes
    {
        Perfect,
        Good,
        Regular
    }

    public class GameManager : MonoBehaviour
    {
        public AudioSource musicSource;

        public bool startPlaying;

        public BeatScroller beatScroller;

        public int currentScore;

        [Header("Scores")]
        public int scorePerNote = 100;
        public int scorePerGoodNote = 125;
        public int scorePerPerfectNote = 200;

        public int currentMultiplier;
        public int multiplierTracker;
        public int[] multiplierThreshholds;

        public List<int> recordedCombos = new List<int>(); // Lists are slow

        [SerializeField]
        private Text scoreText,
            multiText,
            comboText,
            percentHitText,
            normalsText,
            goodsText,
            perfectsText,
            missesText,
            rankText,
            finalScoreText,
            finalComboText;

        private float totalNotes;
        private float normalHits;
        private float goodHits;
        private float missedHits;
        private float perfectHits;

        public GameObject resultsScreen;

        private int currentCombo;

        public static GameManager instance;
        private void Awake()
        {
            // In awake so it will trigger before every Start(), preventing mistakes from desync
            instance = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            scoreText.text = "Score: 0";
            currentMultiplier = 1;
            currentCombo = 0;
            totalNotes = FindObjectsOfType<NoteObject>().Length; // Also very slow
        }

        // Update is called once per frame
        private void Update()
        {
            if (!startPlaying)
            {
                if (Input.anyKeyDown)
                {
                    startPlaying = true;
                    beatScroller.hasStarted = true;

                    musicSource.Play();
                }
            }
            else
            {
                if (!musicSource.isPlaying && !resultsScreen.activeInHierarchy)
                {
                    CreateEndCard();
                }
            }
        }

        public void NoteHit()
        {
            Debug.Log("Note Hit");
            if (currentMultiplier - 1 < multiplierThreshholds.Length)
            {
                multiplierTracker++;
                currentCombo++;

                if (multiplierThreshholds[currentMultiplier - 1] <= multiplierTracker)
                {
                    multiplierTracker = 0;
                    currentMultiplier++;
                }
            }

            multiText.text = "Multiplier: x" + currentMultiplier;
            comboText.text = "Combo: " + currentCombo;
            currentScore += scorePerNote * currentMultiplier;
            scoreText.text = "Score: " + currentScore;
        }

        public void Hit(HitTypes type)
        {
            switch (type)
            {
                case HitTypes.Regular:
                    currentScore += scorePerNote * currentMultiplier;
                    normalHits += 1;
                    break;
                case HitTypes.Good:
                    currentScore += scorePerGoodNote * currentMultiplier;
                    goodHits += 1;
                    break;
                case HitTypes.Perfect:
                    currentScore += scorePerPerfectNote * currentMultiplier;
                    perfectHits += 1;
                    break;
            }

            NoteHit();
            normalHits++;
        }

        public void NoteMissed()
        {
            Debug.Log("Note Missed");
            currentMultiplier = 1;
            multiplierTracker = 0;
            missedHits++;
            multiText.text = "Multiplier: x" + currentMultiplier;
            recordedCombos.Add(currentCombo);
            currentCombo = 0;
            comboText.text = "Combo: " + currentCombo;
        }

        public void CreateEndCard()
        {
            resultsScreen.SetActive(true);
            recordedCombos.Add(currentCombo);
            int maxNumber = recordedCombos.Max();
            normalsText.text = "" + normalHits;
            goodsText.text = goodHits.ToString();
            missesText.text = missedHits.ToString();
            perfectsText.text = perfectHits.ToString();
            finalComboText.text = maxNumber.ToString();
            float totalHit = normalHits + goodHits + perfectHits;
            float percentHit = (totalHit / totalNotes) * 100f;

            percentHitText.text = percentHit.ToString("F1") + "%";

            string rankVal;

            if (percentHit > 95)
                rankVal = "BERRY+";
            else if (percentHit > 85)
                rankVal = "A";
            else if (percentHit > 70)
                rankVal = "B";
            else if (percentHit > 55)
                rankVal = "C";
            else if (percentHit > 40)
                rankVal = "D";
            else
                rankVal = "F";

            rankText.text = rankVal;
            finalScoreText.text = currentScore.ToString();
        }
    }

}

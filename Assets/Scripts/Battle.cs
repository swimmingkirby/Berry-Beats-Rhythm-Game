using BerryBeats.UI.Widgets;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;


namespace BerryBeats.BattleSystem
{
    public class Battle : MonoBehaviour
    {
        public Sprite leftArrow;
        public Sprite upArrow;
        public Sprite downArrow;
        public Sprite rightArrow;
        [Space]
        public ArrowHighlighter highlighter;
        public float BPM;
        public int objectPoolSize = 200;
        public Transform canvas;
        public Texture2D levelData;
        [Space]
        public Vector2 leftPos;
        public Vector2 upPos;
        public Vector2 downPos;
        public Vector2 rightPos;

        // Object Pooling
        private GameObject[] objectPool;
        private int poolIndex;

        // Level Data
        private bool[,] level;
        private bool[,] hasDone;
        private int currentIndex;
        private float currentIndexF;

        private bool started = false;

        //Music
        public AudioSource musicSource;

        //Scoring
        public int currentScore;
        public int scorePerNote = 100;
        public int scorePerGoodNote = 125;
        public int scorePerPerfectNote = 200;
        //Multipliers
        public int currentMultiplier;
        public int multiplierTracker;
        public int[] multiplierThreshholds;
        //Combo
        private int currentCombo;
        public List<int> recordedCombos = new List<int>();      //what do i use apart from a listt??
        //Scores etc output
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


        // Singleton Data
        public static Battle instance;
        private void Awake(){instance=this;}

        private void Start()
        {
            //Sets all variables (to be changes later as not efficient)
            currentMultiplier = 1;
            currentCombo = 0;
            comboText.text = "Combo: " + currentCombo;
            StartCoroutine(Initialize());
        }

        void Update()
        {
            if (started)
            {
                // Refrain from using a timer because it will cause inconsistency
                currentIndexF += Time.deltaTime * (BPM * 4 / 60);
                currentIndex = Mathf.FloorToInt(currentIndexF);

                //start music
                musicSource.Play();

                // Hit detection
                bool left = level[currentIndex, 0];
                bool up = level[currentIndex, 1];
                bool down = level[currentIndex, 2];
                bool right = level[currentIndex, 3];

              
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (left) { NoteHit();  Debug.Log("Hit");}
                    else { Debug.Log("Miss"); NoteMissed(); } 
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (up) { NoteHit(); Debug.Log("Hit"); }
                    else { Debug.Log("Miss"); NoteMissed(); }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (down) { NoteHit(); Debug.Log("Hit"); }
                    else { Debug.Log("Miss"); NoteMissed(); }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (right) { NoteHit(); Debug.Log("Hit"); }
                    else { Debug.Log("Miss"); NoteMissed(); }

                }

                // Visual arrow ques
                if (level.Length > currentIndex + 6)
                {
                    bool leftSpawn = level[currentIndex + 6, 0] && !hasDone[currentIndex + 6, 0];
                    bool upSpawn = level[currentIndex + 6, 1] && !hasDone[currentIndex + 6, 1];
                    bool downSpawn = level[currentIndex + 6, 2] && !hasDone[currentIndex + 6, 2];
                    bool rightSpawn = level[currentIndex + 6, 3] && !hasDone[currentIndex + 6, 3];

                    if (leftSpawn)
                    {
                        hasDone[currentIndex + 6, 0] = true;
                        objectPool[poolIndex].transform.position = leftPos + new Vector2(0, 13);
                        objectPool[poolIndex].SetActive(true);
                        poolIndex++;
                    }
                    if (upSpawn)
                    {
                        hasDone[currentIndex + 6, 1] = true;
                        objectPool[poolIndex].transform.position = upPos + new Vector2(0, 13);
                        objectPool[poolIndex].SetActive(true);
                        poolIndex++;
                    }
                    if (downSpawn)
                    {
                        hasDone[currentIndex + 6, 2] = true;
                        objectPool[poolIndex].transform.position = downPos + new Vector2(0, 13);
                        objectPool[poolIndex].SetActive(true);
                        poolIndex++;
                    }
                    if (rightSpawn)
                    {
                        hasDone[currentIndex + 6, 3] = true;
                        objectPool[poolIndex].transform.position = rightPos + new Vector2(0, 13);
                        objectPool[poolIndex].SetActive(true);
                        poolIndex++;
                    }
                }
                if (level.Length < currentIndex)
                {
                    musicSource.Stop();
                }
            }
     
        }
        //when a note is hit used for scores combos and multipliers
        public void NoteHit()
        {


            /*
             * if (currentMultiplier - 1 < multiplierThreshholds.Length)
            {
                multiplierTracker++;

                if (multiplierThreshholds[currentMultiplier - 1] <= multiplierTracker)
                {
                    multiplierTracker = 0;
                    currentMultiplier++;
                }
            }
            */


            currentCombo++;
            comboText.text = "Combo: " + currentCombo;
            //multiText.text = "Multiplier: x" + currentMultiplier;
            //currentScore += scorePerNote * currentMultiplier;
            //scoreText.text = "Score: " + currentScore;
        }

        public void NoteMissed()
        {
            currentCombo = 0;
            comboText.text = "Combo: " + currentCombo;
            //currentMultiplier = 1;
            //multiplierTracker = 0;
            //missedHits++;
            //multiText.text = "Multiplier: x" + currentMultiplier;
            //recordedCombos.Add(currentCombo);
        }
        public IEnumerator Initialize()
        {
            // Create Progress Bar
            CustomProgress progress = CustomProgress.New(new CustomProgressData()
            {
                position = new Vector2(0, 0),
                max = levelData.width*levelData.height + 500,
                progress = 0
            }, canvas);

            yield return new WaitForEndOfFrame();

            // Create object pool
            GameObject notePrefab = Resources.Load<GameObject>("Note");
            objectPool = new GameObject[objectPoolSize];
            for (var i = 0; i < objectPoolSize; i++)
            {
                objectPool[i] = Instantiate(notePrefab, Vector3.zero, Quaternion.identity, transform);
            }

            // Wait for 1 frame to let unity call all the Start() functions before disabling the objects
            yield return new WaitForEndOfFrame();

            for (var i = 0; i < objectPoolSize; i++)
            {
                objectPool[i].SetActive(false);
            }

            progress.SetProgress(progress.GetProgress() + 500);
            yield return new WaitForEndOfFrame();

            // Load level
            // Initialize arrays
            level = new bool[levelData.height, levelData.width];
            hasDone = new bool[levelData.height, levelData.width];
            Color[] pixels = levelData.GetPixels();

            // Fill level array with data
            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < levelData.height; y++)
                {
                    Color pixel = pixels[x + y * 8];
                    bool putArrow = pixel.a == 1;

                    level[y,x] = putArrow;
                    hasDone[y, x] = false;

                    progress.SetProgress(progress.GetProgress() + 1);
                }
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForEndOfFrame();
            Destroy(progress.gameObject);
            yield return new WaitForEndOfFrame();
            started = true;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector3(leftPos.x, leftPos.y, 0), 0.3f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector3(upPos.x, upPos.y, 0), 0.3f);
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(new Vector3(downPos.x, downPos.y, 0), 0.3f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new Vector3(rightPos.x, rightPos.y, 0), 0.3f);
        }

    }


}



using DentedPixel;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
    [Header("Animation Configuration")]
    [SerializeField] LeanTweenType transitionCurve;
    [SerializeField] float transitionDuration = 0.3f;

    [Header("Fields")]
    [SerializeField] GameObject background;
    [SerializeField] Image levelText;
    [SerializeField] Sprite[] texts;

    int currentLevel = 0;
    int levelCount = 1;
    Vector2 levelTextPosition;
    RectTransform[] backgroundObjects;

    void Start()
    {
        levelTextPosition = levelText.rectTransform.anchoredPosition;
        levelCount = background.transform.childCount;

        // Fill backgroundImages array
        backgroundObjects = new RectTransform[levelCount];
        for (int i = 0; i < levelCount; i++)
        {
            backgroundObjects[i] = background.transform.GetChild(i).GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeMenu(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeMenu(-1);
        }
    }

    public void ChangeMenu(int input)
    {
        int _lastLevel = currentLevel;
        currentLevel = Mathf.RoundToInt(Mathf.Repeat(currentLevel + input, levelCount));

        // Cancel all other animations to prevent glitches
        for (int i = 0; i < levelCount; i++)
        {
            LeanTween.cancel(backgroundObjects[i]);
        }
        LeanTween.cancel(levelText.gameObject);

        // The actual animations
        LeanTween.alpha(backgroundObjects[_lastLevel], 0.0f, transitionDuration)
            .setEase(transitionCurve);
        LeanTween.alpha(backgroundObjects[currentLevel], 1.0f, transitionDuration)
            .setEase(transitionCurve);
        LeanTween.moveX(backgroundObjects[_lastLevel], input * -200, transitionDuration)
            .setFrom(Vector3.zero)
            .setEase(transitionCurve);
        LeanTween.moveX(backgroundObjects[currentLevel], 0, transitionDuration)
            .setFrom(200 * input * Vector3.right)
            .setEase(transitionCurve);

        LeanTween.moveLocalY(levelText.gameObject, levelTextPosition.y + 100, transitionDuration * 0.1f)
            .setEaseOutQuint()
            .setOnComplete(() => LeanTween.moveLocalY(levelText.gameObject, levelTextPosition.y, transitionDuration - 0.1f)
                .setEaseOutCirc());

        levelText.sprite = texts[currentLevel];
    }
}

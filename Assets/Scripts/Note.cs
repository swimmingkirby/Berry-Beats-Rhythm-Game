using UnityEngine;

namespace BerryBeats.BattleSystem
{
    public class Note : MonoBehaviour
    {
        SpriteRenderer srenderer;

        void Start()
        {
            srenderer = GetComponent<SpriteRenderer>();
        }

        void OnEnable()
        {
            switch (Mathf.RoundToInt(transform.localPosition.x))
            {
                case -3:
                    srenderer.sprite = Battle.instance.leftArrow;
                    break;
                case -1:
                    srenderer.sprite = Battle.instance.upArrow;
                    break;
                case 1:
                    srenderer.sprite = Battle.instance.downArrow;
                    break;
                case 3:
                    srenderer.sprite = Battle.instance.rightArrow;
                    break;
            }
        }

        void Update()
        {
            transform.position = transform.position - new Vector3(0, Time.deltaTime, 0) * ((Battle.instance.BPM/60)*8);
            if (transform.position.y < -12)
                gameObject.SetActive(false);
        }
    }
}


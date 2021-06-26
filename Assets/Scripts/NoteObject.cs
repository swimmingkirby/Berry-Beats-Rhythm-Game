using UnityEngine;
using BerryBeats;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    // Start is called before the first frame update
    public KeyCode keyToPress;

    [SerializeField] private GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                Destroy(gameObject);

                if (Mathf.Abs(transform.position.y) > 0.25)
                {
                    GameManager.instance.Hit(HitTypes.Regular);
                    Instantiate(hitEffect, hitEffect.transform.position, hitEffect.transform.rotation);

                    Debug.Log("Hit!");
                }
                else if ((Mathf.Abs(transform.position.y) > 0.05f))
                {
                    GameManager.instance.Hit(HitTypes.Good);
                    Instantiate(goodEffect, goodEffect.transform.position, goodEffect.transform.rotation);

                    Debug.Log("Good!");
                }
                else if ((Mathf.Abs(transform.position.y) > 0.001f))
                {
                    GameManager.instance.Hit(HitTypes.Perfect);
                    Instantiate(perfectEffect, perfectEffect.transform.position, perfectEffect.transform.rotation);

                    Debug.Log("Perfect!");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;

            GameManager.instance.NoteMissed();
            Instantiate(missEffect, missEffect.transform.position, missEffect.transform.rotation);
        }
    }
}
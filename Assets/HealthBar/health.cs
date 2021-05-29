using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class health : MonoBehaviour
{
    public Image bar; //GreenBar image (on top of red image)
    public Image Berry;
    public Image chilli;

    public Transform rightside;
    public Transform leftside;
    [Range(0, 1)] //slider range for now to edit green health
    public float GreenHealth; 
    public float RedHealth;

    void Start()
    {
        GreenHealth = 0.5f;
    }
    void Update()
    {
        RedHealth = 1 - GreenHealth; //red health is whatever is left/depends on green health since no one is actually playing
        Berry.transform.position = Vector3.Lerp(rightside.position, leftside.position, bar.fillAmount);//moves the berry from left side game object to right based on fill amount
        chilli.transform.position = Vector3.Lerp(rightside.position, leftside.position, bar.fillAmount);
        bar.fillAmount = RedHealth / (RedHealth + GreenHealth);
    }
}

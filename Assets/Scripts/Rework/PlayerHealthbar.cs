using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthbar : MonoBehaviour
{

    public float health = 1f; 

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.localScale = new Vector3(health, transform.localScale.y, transform.localScale.z);
        if (health > 2)
            health = 2;
        if (health < 0)
            health = 0;
    }

    public void ModifyHealth(float change)
    {
        health += change;
    }

}

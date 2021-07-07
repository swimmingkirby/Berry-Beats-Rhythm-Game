using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthbar : MonoBehaviour
{

    public float health = 1f;

    [SerializeField] Transform bar_end_pos;
    [SerializeField] GameObject berries;

    void Start()
    {
        health = 1f;

        berries.transform.position = bar_end_pos.position;
    }

    
    void Update()
    {
        transform.localScale = new Vector3(health, transform.localScale.y, transform.localScale.z);
        if (health > 2)
            health = 2;
        if (health < 0)
            health = 0;

        berries.transform.position = bar_end_pos.position;
    }

    public void ModifyHealth(float change)
    {
        health += change;
    }

}

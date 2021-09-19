using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{

    [SerializeField] float speed = 100000f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}

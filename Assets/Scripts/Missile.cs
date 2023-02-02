using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public int currentDamage = 1;
    public bool startFading = false;
    private float time = 0;

    private void Update()
    {
        time += 0.5f * Time.deltaTime;
        if (startFading)
        {
            var color = GetComponent<SpriteRenderer>().color; 
            color.a = Mathf.Lerp(1, 0, time);
            GetComponent<SpriteRenderer>().color = color; //mozno zbytocne
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        GetComponent<CircleCollider2D>().enabled = false;
        startFading = true;
        Destroy(gameObject, 10);
    }
}

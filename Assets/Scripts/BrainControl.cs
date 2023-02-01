using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Your brain remained inside the box, in it's comfort zone. You never let it achieve anything special. 

[RequireComponent(typeof(Rigidbody2D))]
public class BrainControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public float initialSpeed = 1;

    public float speedRampUp = 1;

    public float currentSpeed;

    public float spawnRadius;

    public Transform pivot;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed;

        float directionAngle = (Random.Range(1, 5) + 0.5f) * Mathf.PI /2;
        // rb.velocity = Random.insideUnitCircle.normalized * currentSpeed;
        rb.velocity = new Vector2(Mathf.Cos(directionAngle)*spawnRadius, Mathf.Sin(directionAngle)*spawnRadius ).normalized * currentSpeed;

        // nastavenie zaciatocnej pozicie
        float angle = Random.Range(0.0f, 1.0f) * Mathf.PI * 2f;
        Debug.Log(angle);
        Vector2 newPos = new Vector2(Mathf.Cos(angle)*spawnRadius, Mathf.Sin(angle)*spawnRadius ) + (Vector2) pivot.position;
        transform.position = newPos;
    }

    void Update()
    {
        currentSpeed += speedRampUp * Time.deltaTime;
        rb.velocity = rb.velocity.normalized * currentSpeed;
    }
}

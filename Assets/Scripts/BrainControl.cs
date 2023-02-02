using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// Your brain remained inside the box, in it's comfort zone. You never let it achieve anything special. 

[RequireComponent(typeof(Rigidbody2D))]
public class BrainControl : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private float cooldownTimeCntr = 0;

    public float currentSpeed;

    private List<Vector2> possibleDirections = new List<Vector2>();
    private bool dontResetToMiddle = false;

    public float startingBrainCooldown = 1;
    public float cooldownReductionRate = 0.2f;
    private float currentBrainCooldown;


    public Transform originPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = originPosition.position;
        currentBrainCooldown = startingBrainCooldown;
        cooldownTimeCntr = currentBrainCooldown;
        
        // naplnenie moznych smerov
        possibleDirections.Add(Vector2.up);
        possibleDirections.Add(Vector2.right);
        possibleDirections.Add(Vector2.down);
        possibleDirections.Add(Vector2.left);
        
        // nastavenie zaciatocneho smeru
        decideDirection();
    }

    void Update()
    {
        if (currentBrainCooldown < 0.1f) currentBrainCooldown = 0.1f;

        decideDirection();


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == transform)
            {
                PlayerPrefs.SetInt("resources", PlayerPrefs.GetInt("resources") + PlayerPrefs.GetInt("U_Click"));
            }
        }
    }

    private void decideDirection()
    {
        cooldownTimeCntr += Time.deltaTime;
        
        if (!dontResetToMiddle && Vector2.Distance(originPosition.position, transform.position) < 0.1)
        {
            transform.position = originPosition.position;
            rb.velocity = Vector2.zero;
            if (cooldownTimeCntr > currentBrainCooldown)
            {
                var newDirection = possibleDirections[Random.Range(0, 4)];
                rb.velocity = newDirection * currentSpeed;
                dontResetToMiddle = true;
                cooldownTimeCntr = 0;
                currentBrainCooldown -= cooldownReductionRate;
            }
        }

        if (Vector2.Distance(originPosition.position, transform.position) > 0.1)
        {
            dontResetToMiddle = false;
        }
    }
}

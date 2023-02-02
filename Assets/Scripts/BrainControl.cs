using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// Your brain remained inside the box, in it's comfort zone. You never let it achieve anything special. 

// TODO: upgrade walls, upgrade repair, nejaka nelinearna funkcia na znizovanie cooldownu
// TODO: timer na zmenu modu, modulo na check breathera, efekty postupne, zvuky a grafika

[RequireComponent(typeof(Rigidbody2D))]
public class BrainControl : MonoBehaviour
{
    private Rigidbody2D rb;

    public GameObject fog;

    private float cooldownTimeCntr = 0;

    public float currentSpeed;

    private List<Vector2> possibleDirections = new List<Vector2>();
    private bool dontResetToMiddle = false;

    public float startingBrainCooldown = 1;
    public float cooldownReductionRate = 0.2f;
    private float currentBrainCooldown;

    public Color brainNormal = Color.white;
    public Color brainGold = Color.yellow;
    public Color brainFrozen = Color.cyan;

    private int currentResourceIncome;

    public TMP_Text phaseTimer;
    public GameObject btnpanel;
    [Header("Phases")]
    public float timePerPhase = 30;
    public int normalResourceIncome = 1;
    public float normalSpeed = 10;
    public int currentDamage = 1;
    public int normalDamage = 2;
    public int brainFogDamage = 1;
    private float timeCntr = 0;
    private int phaseCntr = 0;

    [Header("Gold rush")]
    public float timePerGoldRush = 15;
    public int goldRushEveryNthPhase = 3;
    public float goldRushSpeed = 5;
    public int goldRushResourceIncome = 3;
    public int goldRushDamage = 1;

    [Header("Brain freeze")] public float frozenTime = 3;
    
    public Transform originPosition;

    const int BRAIN_FOG_PHASE = 4;
    const int BRAIN_WAVE_PHASE = 2;
    const int BRAIN_FREEZE_PHASE = 1;
    private const int WIN_PHASE = 5;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = originPosition.position;
        currentBrainCooldown = startingBrainCooldown;
        cooldownTimeCntr = currentBrainCooldown;
        currentResourceIncome = normalResourceIncome;
        
        // naplnenie moznych smerov
        possibleDirections.Add(Vector2.up);
        possibleDirections.Add(Vector2.right);
        possibleDirections.Add(Vector2.down);
        possibleDirections.Add(Vector2.left);

        // nastavenie zaciatocneho smeru
        decideDirection();
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                btnpanel.GetComponent<RawImage>().color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                btnpanel.GetComponent<RawImage>().color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

        void Update()
    {
        if (phaseCntr == WIN_PHASE)
        {
            btnpanel.SetActive(true);
            StartCoroutine(FadeImage(true));
            Time.timeScale = 0f;

            // win game
            // lerp time, return
        }
        
        if (currentBrainCooldown < 0.1f) currentBrainCooldown = 0.1f;
        timeCntr += Time.deltaTime;

        if (timeCntr > timePerPhase || (phaseCntr % goldRushEveryNthPhase == 0 && timeCntr > timePerGoldRush))
        {
            timeCntr = 0; // TODO: pridat vypis do UI
            phaseCntr++;
        }
        

        if (phaseCntr % goldRushEveryNthPhase == 0)
        {
            if(phaseCntr != 0)
                initGoldRush();
            phaseTimer.SetText("" + (int) (timePerGoldRush - timeCntr));
        }
        else
        {
            phaseTimer.SetText("" + (int) (timePerPhase - timeCntr));
        }

        if (phaseCntr == BRAIN_FOG_PHASE)
        {
            initBrainFog();
        }
        
        if (phaseCntr == BRAIN_WAVE_PHASE)
        {
            initBrainWave();
            // animacia vlny + vystrel do vsetkych stran projektil s tagom brain a malym colliderom, co uberie HP
            // zastav brain ked je v strede
        }
        
        if (phaseCntr == BRAIN_FREEZE_PHASE)
        {
            initBrainFreeze();
            // zmen tint na nieco modro biele, zablokuj repair a spust korutinu co na konci nastavi ze sa moze repairovat wall
        }
        

        // rozhodnutie pre novy smer
        decideDirection();

        // kliknutie prida resources
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == transform)
            {
                PlayerPrefs.SetInt("resources", PlayerPrefs.GetInt("resources") + currentResourceIncome);
            }
        }
    }

    private void initGoldRush()
    {
        gameObject.GetComponent<SpriteRenderer>().color = brainGold;
        currentSpeed = goldRushSpeed;
        fog.SetActive(false);
        currentResourceIncome = goldRushResourceIncome;
        currentDamage = goldRushDamage;
        GetComponent<SpriteRenderer>().enabled = true;
    }
    private void initBrainFog()
    {
        gameObject.GetComponent<SpriteRenderer>().color = brainNormal;
        AudioManager.instance.PlaySound("fog");
        fog.SetActive(true);
        currentSpeed = normalSpeed;
        currentResourceIncome = normalResourceIncome;
        currentDamage = brainFogDamage;
        GetComponent<SpriteRenderer>().enabled = false;
    }
    
    private void initBrainWave()
    {
        gameObject.GetComponent<SpriteRenderer>().color = brainNormal;
        AudioManager.instance.PlaySound("wave");
        fog.SetActive(false);
        currentSpeed = normalSpeed;
        currentResourceIncome = normalResourceIncome;
        currentDamage = normalDamage;
        GetComponent<SpriteRenderer>().enabled = true;

    }

    private void initBrainFreeze()
    {
        gameObject.GetComponent<SpriteRenderer>().color = brainFrozen;
        AudioManager.instance.PlaySound("ice-block-hit");
        fog.SetActive(false);
        currentSpeed = normalSpeed;
        currentResourceIncome = normalResourceIncome;
        currentDamage = normalDamage;
        GetComponent<SpriteRenderer>().enabled = true;

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (phaseCntr == BRAIN_FREEZE_PHASE)
        {
            FreezWall(col.gameObject);
        }
    }

    private void FreezWall(GameObject wall)
    {
        // nastav stenu ktoru sme trafili na nerozbitnu, spusti timer, ktory ju nastavi spat na rozbitnu po frozenTime
        float invulnerabilityPeriod = frozenTime;
        wall.GetComponent<WallControl>().PreventRepair(invulnerabilityPeriod);
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

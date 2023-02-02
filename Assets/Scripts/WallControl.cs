﻿using TMPro;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    [SerializeField]
    private int currentHp;

    [SerializeField]
    public int hpPerLevel;


    // Start is called before the first frame update
    void Start()
    {
        hpPerLevel = PlayerPrefs.GetInt("U_Walls");
        currentHp = (50 + PlayerPrefs.GetInt("U_Repair") * hpPerLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentHp <= 0)
        {
            gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == transform)
            {
                resolveWallEffect();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentHp--;
        PlayerPrefs.SetInt(gameObject.name, currentHp);
    }

    public void resolveWallEffect()
    {

        //TODO: Osetrit repair, tlacidlo je zapnute stale
        PlayerPrefs.SetInt("repair", 1);
        if (PlayerPrefs.GetInt("repair") == 1)
        {
            int maxWallHp = 10 + PlayerPrefs.GetInt("U_Repair") * hpPerLevel;

            if (currentHp >= maxWallHp)
            {
                currentHp = maxWallHp;
            }
            else
            {
                currentHp++;
                PlayerPrefs.SetInt(gameObject.name, currentHp);
                PlayerPrefs.SetInt("resources", PlayerPrefs.GetInt("resources") - PlayerPrefs.GetInt("repairCost"));
            }
            PlayerPrefs.SetInt("repair", 0);
        }
        else if (PlayerPrefs.GetInt("upgrade") == 1)
        {
            Debug.Log("upgrade");
        }
        
    }
}


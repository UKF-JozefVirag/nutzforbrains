using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class WallControl : MonoBehaviour
{
    private int currentHp;
    private bool preventRepair = false;
    public Material originalMat;
    public Material frozenMat;

    public int hpPerLevel;

    // Start is called before the first frame update
    void Start()
    {
        hpPerLevel = PlayerPrefs.GetInt("U_Walls");
        currentHp = PlayerPrefs.GetInt(gameObject.name);
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
        if (collision.gameObject.tag == "Brain")
        {
            AudioManager.instance.PlaySound("splat-effect");
            PlayerPrefs.SetInt("splash", 1);
            currentHp = Mathf.Clamp(currentHp - collision.gameObject.GetComponent<BrainControl>().currentDamage, 0, int.MaxValue);
        }

        PlayerPrefs.SetInt(gameObject.name, currentHp);
    }

    public void resolveWallEffect()
    {
        // PlayerPrefs.SetInt("repair", 1);
        if (!preventRepair && PlayerPrefs.GetInt("repair") == 1 && PlayerPrefs.GetInt("resources") >= PlayerPrefs.GetInt("repairCost"))
        {

            int maxWallHp = 10 + PlayerPrefs.GetInt("U_Repair") * hpPerLevel;
            
            currentHp += PlayerPrefs.GetInt("repairCost");
            if (currentHp >= maxWallHp)
                currentHp = maxWallHp;
            PlayerPrefs.SetInt(gameObject.name, currentHp);
            AudioManager.instance.PlaySound("upgrade");
            PlayerPrefs.SetInt("resources", PlayerPrefs.GetInt("resources") - PlayerPrefs.GetInt("repairCost"));

            PlayerPrefs.SetInt("repair", 0);
        }
        else if (PlayerPrefs.GetInt("upgrade") == 1)
        {
            Debug.Log("upgrade");
        }
    }

    public void PreventRepair(float invulnerabilityPeriod)
    {
        preventRepair = true;
        GetComponent<SpriteRenderer>().material = frozenMat;
        Invoke("ResetRepairBlock", invulnerabilityPeriod);
    }

    private void ResetRepairBlock()
    {
        preventRepair = false;
        GetComponent<SpriteRenderer>().material = originalMat;
    }
}
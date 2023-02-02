using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TMP_Text textHp1, textHp2, textHp3, textHp4, textResources, repairCostText, repairUpgradeCostx,repairUpgradeCost, wallUpgradeCost, incomeCost;

    public int resourceGainAmount;
    public float resourceGainFrequency;
    private bool resourceGainInvoked = false;

    public Animator imageAnim;
    public Animator deathTextAnim;

    private void Awake()
    {
        PlayerPrefs.SetInt("resources", 200);

        PlayerPrefs.SetInt("Wall1", 10); //hp stien
        PlayerPrefs.SetInt("Wall2", 10);
        PlayerPrefs.SetInt("Wall3", 10);
        PlayerPrefs.SetInt("Wall4", 10);

        PlayerPrefs.SetInt("Wall1_i", 10); //init hp stien
        PlayerPrefs.SetInt("Wall2_i", 10);
        PlayerPrefs.SetInt("Wall3_i", 10);
        PlayerPrefs.SetInt("Wall4_i", 10);

        PlayerPrefs.SetInt("U_Repair", 0); // upgrade repairu
        PlayerPrefs.SetInt("U_Walls", 0); // upgrade walls
        PlayerPrefs.SetInt("U_Income", 0); // upgrade income

        PlayerPrefs.SetInt("repairCost", 1);
        PlayerPrefs.SetInt("repairUpgradeCost", 20); // cena repairu
        PlayerPrefs.SetInt("wallCost", 50); // cena walls
        PlayerPrefs.SetInt("idleCost", 100); // cena idlu
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(PlayerPrefs.GetInt("incomeCost"));

        if (PlayerPrefs.GetInt("resources") < 0)
        {
            PlayerPrefs.SetInt("resources", 0);
        }

        if (PlayerPrefs.GetInt("Wall1") == 0) textHp1.text = "";
        else textHp1.text = PlayerPrefs.GetInt("Wall1") + "";
        
        if (PlayerPrefs.GetInt("Wall2") == 0) textHp2.text = "";
        else textHp2.text = PlayerPrefs.GetInt("Wall2") + "";
        
        if (PlayerPrefs.GetInt("Wall3") == 0) textHp3.text = "";
        else textHp3.text = PlayerPrefs.GetInt("Wall3") + "";
        
        if (PlayerPrefs.GetInt("Wall4") == 0) textHp4.text = "";
        else textHp4.text = PlayerPrefs.GetInt("Wall4") + "";

        if (!resourceGainInvoked && PlayerPrefs.GetInt("U_Income") == 1)
        {
            resourceGainInvoked = true;
            InvokeRepeating("TickResourceGain", resourceGainFrequency, resourceGainFrequency);
        }

        textResources.text = "Resources: " + PlayerPrefs.GetInt("resources");
    }

    private void SetTextOfCost()
    {
        repairCostText.text = PlayerPrefs.GetInt("repairCost") + "";
        repairUpgradeCostx.text = PlayerPrefs.GetInt("repairUpgradeCost") + "";
        wallUpgradeCost.text = PlayerPrefs.GetInt("wallCost") + "";
        incomeCost.text = PlayerPrefs.GetInt("idleCost") + "";
    }

    private void TickResourceGain()
    {
        PlayerPrefs.SetInt("resources", PlayerPrefs.GetInt("resources") + 
                                        resourceGainAmount * PlayerPrefs.GetInt("U_Income") );
    }

    public void clickRepair()
    {
        PlayerPrefs.SetInt("U_Repair", 1);
    }

    public void upgradeRepair()
    {
        if (PlayerPrefs.GetInt("resources") >= PlayerPrefs.GetInt("repairUpgradeCost"))
        {
            PlayerPrefs.SetInt("repairCost", PlayerPrefs.GetInt("U_Repair") + 1);
            PlayerPrefs.SetInt("resources", PlayerPrefs.GetInt("resources") - PlayerPrefs.GetInt("repairUpgradeCost"));
            PlayerPrefs.SetInt("U_Repair", PlayerPrefs.GetInt("U_Repair") + 1);
            PlayerPrefs.SetInt("repairUpgradeCost", (PlayerPrefs.GetInt("repairUpgradeCost") + 10) * 2);
            SetTextOfCost();
        }
    }
    
    public void upgradeWalls()
    {
        if (PlayerPrefs.GetInt("resources") >= PlayerPrefs.GetInt("wallCost"))
        {
            PlayerPrefs.SetInt("Wall1", PlayerPrefs.GetInt("Wall1_i") * 3);
            PlayerPrefs.SetInt("Wall2", PlayerPrefs.GetInt("Wall2_i") * 3);
            PlayerPrefs.SetInt("Wall3", PlayerPrefs.GetInt("Wall3_i") * 3);
            PlayerPrefs.SetInt("Wall4", PlayerPrefs.GetInt("Wall4_i") * 3);

            PlayerPrefs.SetInt("Wall1_i", PlayerPrefs.GetInt("Wall1_i") * 3);
            PlayerPrefs.SetInt("Wall2_i", PlayerPrefs.GetInt("Wall2_i") * 3);
            PlayerPrefs.SetInt("Wall3_i", PlayerPrefs.GetInt("Wall3_i") * 3);
            PlayerPrefs.SetInt("Wall4_i", PlayerPrefs.GetInt("Wall4_i") * 3);

            PlayerPrefs.SetInt("resources", PlayerPrefs.GetInt("resources") - PlayerPrefs.GetInt("wallCost"));
            PlayerPrefs.SetInt("U_Walls", PlayerPrefs.GetInt("U_Walls") + 1);
            PlayerPrefs.SetInt("wallCost", (PlayerPrefs.GetInt("wallCost") + 10) * 2);
            SetTextOfCost();

            textHp1.text = PlayerPrefs.GetInt("Wall1") + "";
            textHp2.text = PlayerPrefs.GetInt("Wall2") + "";
            textHp3.text = PlayerPrefs.GetInt("Wall3") + "";
            textHp4.text = PlayerPrefs.GetInt("Wall4") + "";
        }
    }
    public void upgradeIncome()
    {
        if (PlayerPrefs.GetInt("resources") >= PlayerPrefs.GetInt("idleCost"))
        {
            PlayerPrefs.SetInt("resources", PlayerPrefs.GetInt("resources") - PlayerPrefs.GetInt("idleCost"));
            PlayerPrefs.SetInt("U_Income", PlayerPrefs.GetInt("U_Income") + 1);
            PlayerPrefs.SetInt("idleCost", PlayerPrefs.GetInt("idleCost") * 2);
            SetTextOfCost();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Brain")
        {
            imageAnim.gameObject.SetActive(true);
            deathTextAnim.gameObject.SetActive(true);
        }
    }
}

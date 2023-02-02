using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TMP_Text textHp1, textHp2, textHp3, textHp4, textResources,
        repairCost, repairUpgradeCost, wallUpgradeCost, incomeCost;

    private void Awake()
    {
        PlayerPrefs.SetInt("resources", 20);

        PlayerPrefs.SetInt("Wall1", 50); //hp stien
        PlayerPrefs.SetInt("Wall2", 50);
        PlayerPrefs.SetInt("Wall3", 50);
        PlayerPrefs.SetInt("Wall4", 50);

        PlayerPrefs.SetInt("U_Repair", 0); // upgrade repairu
        PlayerPrefs.SetInt("U_Walls", 0); // upgrade walls
        PlayerPrefs.SetInt("U_Income", 0); // upgrade income

        PlayerPrefs.SetInt("repairCost", 1); // cena repairu
        PlayerPrefs.SetInt("wallCost", 50); // cena walls
        PlayerPrefs.SetInt("idleCost", 100); // cena idlu
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Wall1") == 0) textHp1.text = "";
        else textHp1.text = PlayerPrefs.GetInt("Wall1") + "";
        
        if (PlayerPrefs.GetInt("Wall2") == 0) textHp2.text = "";
        else textHp2.text = PlayerPrefs.GetInt("Wall2") + "";
        
        if (PlayerPrefs.GetInt("Wall3") == 0) textHp3.text = "";
        else textHp3.text = PlayerPrefs.GetInt("Wall3") + "";
        
        if (PlayerPrefs.GetInt("Wall4") == 0) textHp4.text = "";
        else textHp4.text = PlayerPrefs.GetInt("Wall4") + "";

        textResources.text = "Resources: " + PlayerPrefs.GetInt("resources");
    }

    public void clickRepair()
    {
        PlayerPrefs.SetInt("U_Repair", 1);
    }

    public void upgradeRepair()
    {
        if (PlayerPrefs.GetInt("resources") >= PlayerPrefs.GetInt("repairCost"))
        {
            PlayerPrefs.SetInt("U_Repair", PlayerPrefs.GetInt("U_Repair") +1);
            PlayerPrefs.SetInt("repairCost", (PlayerPrefs.GetInt("repairCost")+10)*2);
        }
    }
    
    public void upgradeWalls()
    {
        if (PlayerPrefs.GetInt("resources") >= PlayerPrefs.GetInt("wallCost"))
        {
            PlayerPrefs.SetInt("U_Walls", PlayerPrefs.GetInt("U_Walls") +1);
            PlayerPrefs.SetInt("wallCost", (PlayerPrefs.GetInt("wallCost")+10)*2);
        }
    }
    public void upgradeIncome()
    {
        if (PlayerPrefs.GetInt("resources") >= PlayerPrefs.GetInt("upgradeCost"))
        {
            PlayerPrefs.SetInt("U_Upgrade", PlayerPrefs.GetInt("U_Upgrade") +1);
            PlayerPrefs.SetInt("upgradeCost", (PlayerPrefs.GetInt("upgradeCost")+10)*2);
        }
    }

}

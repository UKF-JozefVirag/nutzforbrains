using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TMP_Text textHp1, textHp2, textHp3, textHp4;

    private void Awake()
    {
        PlayerPrefs.SetInt("Wall1", 10);
        PlayerPrefs.SetInt("Wall2", 10);
        PlayerPrefs.SetInt("Wall3", 10);
        PlayerPrefs.SetInt("Wall4", 10);
        PlayerPrefs.SetInt("U_Repair", 0);
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

    }

    public void clickRepair()
    {
        PlayerPrefs.SetInt("U_Repair", 1);
    }

}

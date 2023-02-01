using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    public TMP_Text textHp1, textHp2, textHp3, textHp4;

    private void Awake()
    {
        PlayerPrefs.SetInt("U_Repair", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickRepair()
    {
        PlayerPrefs.SetInt("U_Repair", 1);
    }

}

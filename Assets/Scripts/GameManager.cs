using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

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

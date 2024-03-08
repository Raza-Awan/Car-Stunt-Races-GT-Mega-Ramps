using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUiManager : MonoBehaviour
{
    public GameObject statsUI, engUpgHolder, suspensionUpgHolder, tireBrakeUpgHolder;
    public GameObject engUpgStatsUI, spUpgStatsUI, tiresUpgStatsUI;
    public GameObject upgradeMenu;

    [HideInInspector] public GameObject[] engUpgrades; //ref to all eng upgs, under eng upg holder obj.
    [HideInInspector] public GameObject[] SPUpgrades;  //ref to all suspension upgs, under suspension upg holder obj.
    [HideInInspector] public GameObject[] TBUpgrades;  //ref to all tire brakes upgs, under tires and brakes upg holder obj.

    GarageCarSelection carselection;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        carselection = GetComponent<GarageCarSelection>();
        anim = Camera.main.GetComponent<Animator>();

        // specifying each arrays length
        engUpgrades = new GameObject[engUpgHolder.transform.childCount];
        SPUpgrades = new GameObject[suspensionUpgHolder.transform.childCount];
        TBUpgrades = new GameObject[tireBrakeUpgHolder.transform.childCount];

        // filling in the arrays
        for (int i = 0; i < engUpgrades.Length; i++)
        {
            engUpgrades[i] = engUpgHolder.transform.GetChild(i).gameObject;
            SPUpgrades[i] = suspensionUpgHolder.transform.GetChild(i).gameObject;
            TBUpgrades[i] = tireBrakeUpgHolder.transform.GetChild(i).gameObject;
        }

    }

    public void OnEngineUIClick() // For Engine Upgrade UI Display managemnet
    {
        //ON
        anim.Play("UPGRADE ENGINE");
        engUpgHolder.SetActive(true);
        engUpgStatsUI.SetActive(true);

        int currentSelectedCar = PlayerPrefs.GetInt("selectedcar");
        foreach (GameObject upg in engUpgrades)
        {
            upg.SetActive(false);
        }
        engUpgrades[currentSelectedCar].SetActive(true);

        //OFF
        upgradeMenu.SetActive(false);
        statsUI.SetActive(false);
        suspensionUpgHolder.SetActive(false);
        tireBrakeUpgHolder.SetActive(false);
    }

    public void EngBackBtn()
    {
        anim.Play("UPGRADE ENGINE back");
        statsUI.SetActive(true);
        upgradeMenu.SetActive(true);
        engUpgStatsUI.SetActive(false);
        engUpgHolder.SetActive(false);
    }

    public void OnSuspensionUIClick() // For Suspension Upgrade UI Display managemnet
    {
        //ON
        anim.Play("UPGRADE SUSPENSION");
        suspensionUpgHolder.SetActive(true);
        spUpgStatsUI.SetActive(true);

        int currentSelectedCar = PlayerPrefs.GetInt("selectedcar");
        foreach (GameObject upg in SPUpgrades)
        {
            upg.SetActive(false);
        }
        SPUpgrades[currentSelectedCar].SetActive(true);

        //OFF
        upgradeMenu.SetActive(false);
        statsUI.SetActive(false);
        engUpgHolder.SetActive(false);
        tireBrakeUpgHolder.SetActive(false);
    }

    public void SPBackBtn()
    {
        anim.Play("UPGRADE SUSPENSION back");
        statsUI.SetActive(true);
        upgradeMenu.SetActive(true);
        spUpgStatsUI.SetActive(false);
        suspensionUpgHolder.SetActive(false);
    }

    public void OnTyreBrakeUIClick() // For Tire brakes Upgrade UI Display managemnet
    {
        //ON
        anim.Play("UPGRADE WHEEL");
        tireBrakeUpgHolder.SetActive(true);
        tiresUpgStatsUI.SetActive(true);

        int currentSelectedCar = PlayerPrefs.GetInt("selectedcar");
        foreach (GameObject upg in TBUpgrades)
        {
            upg.SetActive(false);
        }
        TBUpgrades[currentSelectedCar].SetActive(true);

        //OFF
        upgradeMenu.SetActive(false);
        statsUI.SetActive(false);
        engUpgHolder.SetActive(false);
        suspensionUpgHolder.SetActive(false);
    }

    public void TiresBackBtn()
    {
        anim.Play("UPGRADE WHEEL back");
        statsUI.SetActive(true);
        upgradeMenu.SetActive(true);
        tiresUpgStatsUI.SetActive(false);
        tireBrakeUpgHolder.SetActive(false);
    }

    public void BackToUpgMenu()
    {
        //ON
        statsUI.SetActive(true);
        //OFF
        engUpgHolder.SetActive(false);
        suspensionUpgHolder.SetActive(false);
        tireBrakeUpgHolder.SetActive(false);
        upgradeMenu.SetActive(false);
    }
}

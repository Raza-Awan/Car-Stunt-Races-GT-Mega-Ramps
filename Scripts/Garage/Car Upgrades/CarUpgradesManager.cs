using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NWH.WheelController3D;
using System.Linq;

// This script is managing all the cars upgrades logic

public class CarUpgradesManager : MonoBehaviour
{
    private UpgradesHolder upgradesHolder;
    private GarageCarSelection carSelection;

    //[CustomHeader("UI References")]
    public GameObject upgLowFundsUI;
    // For Engine
    private int[] engBtnIndex;
    // For Suspension
    private int[] SPBtnIndex;
    // For TireBrakes
    private int[] TBBtnIndex;

    // Start is called before the first frame update
    void Start()
    {
        upgradesHolder = GetComponent<UpgradesHolder>();
        carSelection = GetComponent<GarageCarSelection>();

        PriceAjustments();

        // For Engine 
        EngineInitialization();

        // For Suspension
        SPInitialization();

        // For Tire Brakes
        TBInitialization();

        UpdateCarStats();
    }

    private void PriceAjustments()  // This method is for dynamically adjusting prices for each car upgrade 
    {
        // Engine Prices
        // First Initial starting price for upg will be setted from inspector
        upgradesHolder.engineUpgradesStats[0].price[1] = upgradesHolder.engineUpgradesStats[0].price[0] * 1.7f;
        upgradesHolder.engineUpgradesStats[0].price[2] = upgradesHolder.engineUpgradesStats[0].price[1] * 1.7f;

        // Suspension Prices
        // First Initial starting price for upg will be setted from inspector
        upgradesHolder.SPUpgStats[0].price[1] = upgradesHolder.SPUpgStats[0].price[0] * 1.4f;
        upgradesHolder.SPUpgStats[0].price[2] = upgradesHolder.SPUpgStats[0].price[1] * 1.4f;

        // Tire Brakes Prices
        // First Initial starting price for upg will be setted from inspector
        upgradesHolder.TBUpgStats[0].price[1] = upgradesHolder.TBUpgStats[0].price[0] * 1.4f;
        upgradesHolder.TBUpgStats[0].price[2] = upgradesHolder.TBUpgStats[0].price[1] * 1.4f;
    }

    private void UpdateCarStats()
    {
        int currentCarIndex = PlayerPrefs.GetInt("selectedcar");

        //ENGINE FILL IMAGE AMOUNTS INITIALIZATION
        //Top Speed:
        upgradesHolder.engineUpgradesUI[currentCarIndex].tsFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[0];
        upgradesHolder.engineUpgradesUI[currentCarIndex].tsBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[0];
        upgradesHolder.engineUpgradesUI[currentCarIndex].currentTS.text = carSelection.carUpg_SO[currentCarIndex].TS_CurrentVal[engBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.engineUpgradesUI[currentCarIndex].newTS.text = carSelection.carUpg_SO[currentCarIndex].TS_NewVal[engBtnIndex[currentCarIndex]].ToString();
        //Acceleration:
        upgradesHolder.engineUpgradesUI[currentCarIndex].acFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[1];
        upgradesHolder.engineUpgradesUI[currentCarIndex].acBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[1];
        upgradesHolder.engineUpgradesUI[currentCarIndex].currentXL.text = carSelection.carUpg_SO[currentCarIndex].XL_CurrentVal[SPBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.engineUpgradesUI[currentCarIndex].newXL.text = carSelection.carUpg_SO[currentCarIndex].XL_NewVal[SPBtnIndex[currentCarIndex]].ToString();
        //Power:
        upgradesHolder.engineUpgradesUI[currentCarIndex].pwFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[2];
        upgradesHolder.engineUpgradesUI[currentCarIndex].pwBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[2];
        upgradesHolder.engineUpgradesUI[currentCarIndex].currentPW.text = carSelection.carUpg_SO[currentCarIndex].PW_CurrentVal[engBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.engineUpgradesUI[currentCarIndex].newPW.text = carSelection.carUpg_SO[currentCarIndex].PW_NewVal[engBtnIndex[currentCarIndex]].ToString();
        //Torque/Handling:
        upgradesHolder.engineUpgradesUI[currentCarIndex].tqFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[3];
        upgradesHolder.engineUpgradesUI[currentCarIndex].tqBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[3];
        upgradesHolder.engineUpgradesUI[currentCarIndex].currentTQ.text = carSelection.carUpg_SO[currentCarIndex].TQ_CurrentVal[TBBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.engineUpgradesUI[currentCarIndex].newTQ.text = carSelection.carUpg_SO[currentCarIndex].TQ_NewVal[TBBtnIndex[currentCarIndex]].ToString();

        //Suspension FILL IMAGE AMOUNTS INITIALIZATION
        //Top Speed:
        upgradesHolder.SPUpgUI[currentCarIndex].tsFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[0];
        upgradesHolder.SPUpgUI[currentCarIndex].tsBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[0];
        upgradesHolder.SPUpgUI[currentCarIndex].currentTS.text = carSelection.carUpg_SO[currentCarIndex].TS_CurrentVal[engBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.SPUpgUI[currentCarIndex].newTS.text = carSelection.carUpg_SO[currentCarIndex].TS_NewVal[engBtnIndex[currentCarIndex]].ToString();
        //Acceleration
        upgradesHolder.SPUpgUI[currentCarIndex].acFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[1];
        upgradesHolder.SPUpgUI[currentCarIndex].acBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[1];
        upgradesHolder.SPUpgUI[currentCarIndex].currentXL.text = carSelection.carUpg_SO[currentCarIndex].XL_CurrentVal[SPBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.SPUpgUI[currentCarIndex].newXL.text = carSelection.carUpg_SO[currentCarIndex].XL_NewVal[SPBtnIndex[currentCarIndex]].ToString();
        //Power
        upgradesHolder.SPUpgUI[currentCarIndex].pwFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[2];
        upgradesHolder.SPUpgUI[currentCarIndex].pwBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[2];
        upgradesHolder.SPUpgUI[currentCarIndex].currentPW.text = carSelection.carUpg_SO[currentCarIndex].PW_CurrentVal[engBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.SPUpgUI[currentCarIndex].newPW.text = carSelection.carUpg_SO[currentCarIndex].PW_NewVal[engBtnIndex[currentCarIndex]].ToString();
        //Torque/Handling
        upgradesHolder.SPUpgUI[currentCarIndex].tqFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[3];
        upgradesHolder.SPUpgUI[currentCarIndex].tqBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[3];
        upgradesHolder.SPUpgUI[currentCarIndex].currentTQ.text = carSelection.carUpg_SO[currentCarIndex].TQ_CurrentVal[TBBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.SPUpgUI[currentCarIndex].newTQ.text = carSelection.carUpg_SO[currentCarIndex].TQ_NewVal[TBBtnIndex[currentCarIndex]].ToString();

        //Tires FILL IMAGE AMOUNTS INITIALIZATION
        //Top Speed:
        upgradesHolder.TBUpgUI[currentCarIndex].tsFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[0];
        upgradesHolder.TBUpgUI[currentCarIndex].tsBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[0];
        upgradesHolder.TBUpgUI[currentCarIndex].currentTS.text = carSelection.carUpg_SO[currentCarIndex].TS_CurrentVal[engBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.TBUpgUI[currentCarIndex].newTS.text = carSelection.carUpg_SO[currentCarIndex].TS_NewVal[engBtnIndex[currentCarIndex]].ToString();
        //Acceleration            
        upgradesHolder.TBUpgUI[currentCarIndex].acFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[1];
        upgradesHolder.TBUpgUI[currentCarIndex].acBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[1];
        upgradesHolder.TBUpgUI[currentCarIndex].currentXL.text = carSelection.carUpg_SO[currentCarIndex].XL_CurrentVal[SPBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.TBUpgUI[currentCarIndex].newXL.text = carSelection.carUpg_SO[currentCarIndex].XL_NewVal[SPBtnIndex[currentCarIndex]].ToString();
        //Power             
        upgradesHolder.TBUpgUI[currentCarIndex].pwFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[2];
        upgradesHolder.TBUpgUI[currentCarIndex].pwBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[2];
        upgradesHolder.TBUpgUI[currentCarIndex].currentPW.text = carSelection.carUpg_SO[currentCarIndex].PW_CurrentVal[engBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.TBUpgUI[currentCarIndex].newPW.text = carSelection.carUpg_SO[currentCarIndex].PW_NewVal[engBtnIndex[currentCarIndex]].ToString();
        //Torque/Handling           
        upgradesHolder.TBUpgUI[currentCarIndex].tqFrontImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[3];
        upgradesHolder.TBUpgUI[currentCarIndex].tqBackImage.fillAmount = carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[3];
        upgradesHolder.TBUpgUI[currentCarIndex].currentTQ.text = carSelection.carUpg_SO[currentCarIndex].TQ_CurrentVal[TBBtnIndex[currentCarIndex]].ToString();
        upgradesHolder.TBUpgUI[currentCarIndex].newTQ.text = carSelection.carUpg_SO[currentCarIndex].TQ_NewVal[TBBtnIndex[currentCarIndex]].ToString();
    }

    private void EngineInitialization()
    {
        engBtnIndex = new int[upgradesHolder.carsPrefabs.Length];
        int currentCarIndex = PlayerPrefs.GetInt("selectedcar");

        // Setting btn index at start from Player prefs stored value for each btn index.
        for (int i = 0; i < engBtnIndex.Length; i++)
        {
            engBtnIndex[i] = PlayerPrefs.GetInt("EngUpgBtn" + i);

            // Manually adjusting fill image amounts, according to btn index
            if (engBtnIndex[i] == 3)
            {
                upgradesHolder.carsPrefabs[i].powertrain.engine.maxPower = carSelection.carUpg_SO[i].maxPowerVal[3];
            }
            if (engBtnIndex[i] == 2)
            {
                upgradesHolder.carsPrefabs[i].powertrain.engine.maxPower = carSelection.carUpg_SO[i].maxPowerVal[2];
            }
            if (engBtnIndex[i] == 1)
            {
                upgradesHolder.carsPrefabs[i].powertrain.engine.maxPower = carSelection.carUpg_SO[i].maxPowerVal[1];
            }
            if (engBtnIndex[i] == 0)
            {
                upgradesHolder.carsPrefabs[i].powertrain.engine.maxPower = carSelection.carUpg_SO[i].maxPowerVal[0];
            }
        }

        // Filling the engineUpgradeUI scripts references dnamically, so not do that every time manually in inspector for every new car.
        for (int i = 0; i < upgradesHolder.engineUpgradesUI.Length; i++)
        {
            upgradesHolder.engineUpgradesUI[i].engUpgBtns = upgradesHolder.engUpgHolder[i].GetComponentsInChildren<Button>();

            if (i > 0)
            {
                upgradesHolder.engineUpgradesStats[i].price[0] = upgradesHolder.engineUpgradesStats[i - 1].price[0] + 200;
                upgradesHolder.engineUpgradesStats[i].price[1] = upgradesHolder.engineUpgradesStats[i].price[0] * 1.7f;
                upgradesHolder.engineUpgradesStats[i].price[2] = upgradesHolder.engineUpgradesStats[i].price[1] * 1.7f;
            }

            for (int j = 0; j < 3; j++)
            {
                upgradesHolder.engineUpgradesUI[i].engPriceText[j] = upgradesHolder.engineUpgradesUI[i].engUpgBtns[j].GetComponentInChildren<TextMeshProUGUI>();
                upgradesHolder.engineUpgradesUI[i].engPriceText[j].text = "$" + upgradesHolder.engineUpgradesStats[i].price[j];

                upgradesHolder.engineUpgradesUI[i].engLockImages[j] = upgradesHolder.engineUpgradesUI[i].engUpgBtns[j].transform.GetChild(1).GetComponent<Image>();
                // 1 means that image is at child index of 1 under btn obj, so make sure it is at index of 1 in editor as well.
            }
        }

        //Checking if all 3 eng upragdes are applied on any car, then off all the upgrades for that car
        for (int i = 0; i < engBtnIndex.Length; i++)
        {
            if (engBtnIndex[i] == 3)
            {
                foreach (Button btn in upgradesHolder.engineUpgradesUI[i].engUpgBtns)
                {
                    btn.gameObject.SetActive(false);
                }
            }
            if (engBtnIndex[i] == 2)
            {
                upgradesHolder.engineUpgradesUI[i].engUpgBtns[engBtnIndex[i] - 1].gameObject.SetActive(false);
                upgradesHolder.engineUpgradesUI[i].engUpgBtns[engBtnIndex[i] - 2].gameObject.SetActive(false);
                // For Lock Images
                upgradesHolder.engineUpgradesUI[i].engLockImages[engBtnIndex[i]].gameObject.SetActive(false);
            }
            if (engBtnIndex[i] == 1)
            {
                upgradesHolder.engineUpgradesUI[i].engUpgBtns[engBtnIndex[i] - 1].gameObject.SetActive(false);
                // For Lock Images
                upgradesHolder.engineUpgradesUI[i].engLockImages[engBtnIndex[i]].gameObject.SetActive(false);
            }
            if (engBtnIndex[i] == 0)
            {
                // For Lock Images
                upgradesHolder.engineUpgradesUI[i].engLockImages[engBtnIndex[i]].gameObject.SetActive(false);
            }
        }

        // Making all eng upg btns interactables to false, leaving only the one to true which will be the current upg btn
        for (int i = 0; i < upgradesHolder.engineUpgradesUI.Length; i++)
        {
            foreach (Button btn in upgradesHolder.engineUpgradesUI[i].engUpgBtns)
            {
                btn.interactable = false;
            }

            if (engBtnIndex[i] < 3)
            {
                upgradesHolder.engineUpgradesUI[i].engUpgBtns[engBtnIndex[i]].interactable = true;
            }
        }
    }

    public void UpgradeEngine() //Attach this method on every engine upgrade button
    {
        int currentCarIndex = PlayerPrefs.GetInt("selectedcar");

        // Firstly, check whether player have enough cash for upgrade, if not then don't proceed further, return
        string cash = carSelection.currencyItem.ValueTitle.text;
        string extractedNumbers = new string(cash.Where(char.IsDigit).ToArray());

        float result;
        if (float.TryParse(extractedNumbers, out result))
        {
            // Use the result as needed
            Debug.Log("Current Cash = " + result);
        }

        float carUpgPrice = upgradesHolder.engineUpgradesStats[currentCarIndex].price[engBtnIndex[currentCarIndex]];
        if (result >= carUpgPrice)
        {
            CBS_CurrencyTest.INSTANCE.SubtractCurrency((int)carUpgPrice);
        }
        else
        {
            StartCoroutine(UpgLowFunds());
            return;
        }

        //Play audio clip when upgrade is applied
        carSelection.upgradeAudio.Play();

        carSelection.carUpg_SO[currentCarIndex].applyEngUpg[engBtnIndex[currentCarIndex]] = true;
        carSelection.carUpg_SO[currentCarIndex].carPower += 5;
        if (engBtnIndex[currentCarIndex] == 0)
        {
            carSelection.carUpg_SO[currentCarIndex].boxesToFill += 1;
        }

        // for increasing top speed & power fill image amount
        carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[0] += 0.033f;    //for top speed
        carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[2] += 0.033f;    //for power
        if (engBtnIndex[currentCarIndex] < 2)
        {
            carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[0] += 0.033f;    //for top speed
            carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[2] += 0.033f;    //for power
        }

        //if we have enough cash then after using this upgrade, off this button and set interactable for next upg btn to true

        engBtnIndex[currentCarIndex] = PlayerPrefs.GetInt("EngUpgBtn" + currentCarIndex);
        engBtnIndex[currentCarIndex]++;

        if (engBtnIndex[currentCarIndex] >= 3)
        {
            engBtnIndex[currentCarIndex] = 3;
        }
        PlayerPrefs.SetInt("EngUpgBtn" + currentCarIndex, engBtnIndex[currentCarIndex]);

        // for increasing car prefab's engine max power amount.
        upgradesHolder.carsPrefabs[currentCarIndex].powertrain.engine.maxPower = carSelection.carUpg_SO[currentCarIndex].maxPowerVal[engBtnIndex[currentCarIndex]];

        //for increasing max torque, top speed values
        //For max torque:
        carSelection.carUpg_SO[currentCarIndex].div2_Amount2 = carSelection.carUpg_SO[currentCarIndex].PW_CurrentVal[engBtnIndex[currentCarIndex]];
        //For Top speed:
        carSelection.carUpg_SO[currentCarIndex].div2_Amount3 = carSelection.carUpg_SO[currentCarIndex].TS_CurrentVal[engBtnIndex[currentCarIndex]];

        if (engBtnIndex[currentCarIndex] < 3) //because there are only 3 upgrades for engine, so index less than 3 
        {
            upgradesHolder.engineUpgradesUI[currentCarIndex].engUpgBtns[engBtnIndex[currentCarIndex]].interactable = true;
            upgradesHolder.engineUpgradesUI[currentCarIndex].engLockImages[engBtnIndex[currentCarIndex]].gameObject.SetActive(false);
        }
    }

    private void SPInitialization()
    {
        SPBtnIndex = new int[upgradesHolder.carsPrefabs.Length];

        // Filling the engineUpgradeUI scripts references dnamically, so not do that every time manually in inspector for every new car.
        for (int i = 0; i < upgradesHolder.SPUpgUI.Length; i++)
        {
            upgradesHolder.SPUpgUI[i].SPUpgBtns = upgradesHolder.SPUpgHolder[i].GetComponentsInChildren<Button>();

            upgradesHolder.SPUpgUI[i].wheelControllers = upgradesHolder.carsPrefabs[i].GetComponentsInChildren<WheelController>();

            if (i > 0)
            {
                upgradesHolder.SPUpgStats[i].price[0] = upgradesHolder.SPUpgStats[i - 1].price[0] + 200;
                upgradesHolder.SPUpgStats[i].price[1] = upgradesHolder.SPUpgStats[i].price[0] * 1.4f;
                upgradesHolder.SPUpgStats[i].price[2] = upgradesHolder.SPUpgStats[i].price[1] * 1.4f;
            }

            for (int j = 0; j < 3; j++)
            {
                upgradesHolder.SPUpgUI[i].SPPriceText[j] = upgradesHolder.SPUpgUI[i].SPUpgBtns[j].GetComponentInChildren<TextMeshProUGUI>();
                upgradesHolder.SPUpgUI[i].SPPriceText[j].text = "$" + upgradesHolder.SPUpgStats[i].price[j];

                upgradesHolder.SPUpgUI[i].SPLockImages[j] = upgradesHolder.SPUpgUI[i].SPUpgBtns[j].transform.GetChild(1).GetComponent<Image>();
                // 1 means that image is at child index of 1 under btn obj, so make sure it is at index of 1 in editor as well.
            }
        }

        // Setting btn index at start from Player prefs stored value for each btn index.
        for (int i = 0; i < SPBtnIndex.Length; i++)
        {
            SPBtnIndex[i] = PlayerPrefs.GetInt("SPUpgBtn" + i);

            // Manually adjusting fill image amounts, according to btn index
            if (SPBtnIndex[i] == 3)
            {
                upgradesHolder.carsPrefabs[i].powertrain.engine.inertia = carSelection.carUpg_SO[i].inertias[3];
                foreach (WheelController wheel in upgradesHolder.SPUpgUI[i].wheelControllers)
                {
                    //wheel.spring.maxLength *= 1.1f;
                    wheel.spring.maxLength = carSelection.carUpg_SO[i].maxSpringLengths[3];
                }
            }
            if (SPBtnIndex[i] == 2)
            {
                upgradesHolder.carsPrefabs[i].powertrain.engine.inertia = carSelection.carUpg_SO[i].inertias[2];
                foreach (WheelController wheel in upgradesHolder.SPUpgUI[i].wheelControllers)
                {
                    //wheel.spring.maxLength *= 1.1f;
                    wheel.spring.maxLength = carSelection.carUpg_SO[i].maxSpringLengths[2];
                }
            }
            if (SPBtnIndex[i] == 1)
            {
                upgradesHolder.carsPrefabs[i].powertrain.engine.inertia = carSelection.carUpg_SO[i].inertias[1];
                foreach (WheelController wheel in upgradesHolder.SPUpgUI[i].wheelControllers)
                {
                    //wheel.spring.maxLength *= 1.1f;
                    wheel.spring.maxLength = carSelection.carUpg_SO[i].maxSpringLengths[1];
                }
            }
            if (SPBtnIndex[i] == 0)
            {
                upgradesHolder.carsPrefabs[i].powertrain.engine.inertia = carSelection.carUpg_SO[i].inertias[0];
                foreach (WheelController wheel in upgradesHolder.SPUpgUI[i].wheelControllers)
                {
                    //wheel.spring.maxLength *= 1.1f;
                    wheel.spring.maxLength = carSelection.carUpg_SO[i].maxSpringLengths[0];
                }
            }
        }

        //Checking if all 3 eng upragdes are applied on any car, then off all the upgrades for that car
        for (int i = 0; i < SPBtnIndex.Length; i++)
        {
            if (SPBtnIndex[i] == 3)
            {
                foreach (Button btn in upgradesHolder.SPUpgUI[i].SPUpgBtns)
                {
                    btn.gameObject.SetActive(false);
                }
            }
            if (SPBtnIndex[i] == 2)
            {
                upgradesHolder.SPUpgUI[i].SPUpgBtns[SPBtnIndex[i] - 1].gameObject.SetActive(false);
                upgradesHolder.SPUpgUI[i].SPUpgBtns[SPBtnIndex[i] - 2].gameObject.SetActive(false);
                // For Lock Images
                upgradesHolder.SPUpgUI[i].SPLockImages[SPBtnIndex[i]].gameObject.SetActive(false);
            }
            if (SPBtnIndex[i] == 1)
            {
                upgradesHolder.SPUpgUI[i].SPUpgBtns[SPBtnIndex[i] - 1].gameObject.SetActive(false);
                // For Lock Images
                upgradesHolder.SPUpgUI[i].SPLockImages[SPBtnIndex[i]].gameObject.SetActive(false);
            }
            if (SPBtnIndex[i] == 0)
            {
                // For Lock Images
                upgradesHolder.SPUpgUI[i].SPLockImages[SPBtnIndex[i]].gameObject.SetActive(false);
            }
        }

        // Making all eng upg btns interactables to false, leaving only the one to true which will be the current upg btn
        for (int i = 0; i < upgradesHolder.SPUpgUI.Length; i++)
        {
            foreach (Button btn in upgradesHolder.SPUpgUI[i].SPUpgBtns)
            {
                btn.interactable = false;
            }

            if (SPBtnIndex[i] < 3)
            {
                upgradesHolder.SPUpgUI[i].SPUpgBtns[SPBtnIndex[i]].interactable = true;
            }
        }
    }

    public void UpgradeSuspension() //Attach this method on every suspension upgrade button
    {
        int currentCarIndex = PlayerPrefs.GetInt("selectedcar");

        // Firstly, check whether player have enough cash for upgrade, if not then don't proceed further, return
        string cash = carSelection.currencyItem.ValueTitle.text;
        string extractedNumbers = new string(cash.Where(char.IsDigit).ToArray());

        float result;
        if (float.TryParse(extractedNumbers, out result))
        {
            // Use the result as needed
            Debug.Log("Current Cash = " + result);
        }

        float carUpgPrice = upgradesHolder.engineUpgradesStats[currentCarIndex].price[engBtnIndex[currentCarIndex]];
        if (result >= carUpgPrice)
        {
            CBS_CurrencyTest.INSTANCE.SubtractCurrency((int)carUpgPrice);
        }
        else
        {
            StartCoroutine(UpgLowFunds());
            return;
        }

        //Play audio clip when upgrade is applied
        carSelection.upgradeAudio.Play();

        carSelection.carUpg_SO[currentCarIndex].applySpUpg[SPBtnIndex[currentCarIndex]] = true;
        carSelection.carUpg_SO[currentCarIndex].carPower += 2;
        if (SPBtnIndex[currentCarIndex] == 0)
        {
            carSelection.carUpg_SO[currentCarIndex].boxesToFill += 1;
        }

        // for increasing acceleration fill image amount
        carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[1] += 0.033f;    //for acceleration
        if (SPBtnIndex[currentCarIndex] < 2)
        {
            carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[1] += 0.033f;
        }

        //if we have enough cash then after using this upgrade, off this button and set interactable for next upg btn to true

        SPBtnIndex[currentCarIndex] = PlayerPrefs.GetInt("SPUpgBtn" + currentCarIndex);
        SPBtnIndex[currentCarIndex]++;

        if (SPBtnIndex[currentCarIndex] >= 3)
        {
            SPBtnIndex[currentCarIndex] = 3;
        }
        PlayerPrefs.SetInt("SPUpgBtn" + currentCarIndex, SPBtnIndex[currentCarIndex]);

        // for increasing car prefab's engine inertia amount and cars wheels suspension.
        upgradesHolder.carsPrefabs[currentCarIndex].powertrain.engine.inertia = carSelection.carUpg_SO[currentCarIndex].inertias[SPBtnIndex[currentCarIndex]];
        foreach (WheelController wheel in upgradesHolder.SPUpgUI[currentCarIndex].wheelControllers)
        {
            //wheel.spring.maxLength *= 1.1f;
            wheel.spring.maxLength = carSelection.carUpg_SO[currentCarIndex].maxSpringLengths[SPBtnIndex[currentCarIndex]];
        }

        //for increasing 0-60 mph(s) values
        //For 0-60 mph(s):
        carSelection.carUpg_SO[currentCarIndex].div2_Amount1 = carSelection.carUpg_SO[currentCarIndex].XL_CurrentVal[engBtnIndex[currentCarIndex]];

        if (SPBtnIndex[currentCarIndex] < 3) //because there are only 3 upgrades for engine, so index less than 3 
        {
            upgradesHolder.SPUpgUI[currentCarIndex].SPUpgBtns[SPBtnIndex[currentCarIndex]].interactable = true;
            upgradesHolder.SPUpgUI[currentCarIndex].SPLockImages[SPBtnIndex[currentCarIndex]].gameObject.SetActive(false);
        }
    }

    public void TBInitialization()
    {
        TBBtnIndex = new int[upgradesHolder.carsPrefabs.Length];

        // Setting btn index at start from Player prefs stored value for each btn index.
        for (int i = 0; i < TBBtnIndex.Length; i++)
        {
            TBBtnIndex[i] = PlayerPrefs.GetInt("TBUpgBtn" + i);

            // Manually adjusting fill image amounts, according to btn index
            if (TBBtnIndex[i] == 3)
            {
                upgradesHolder.carsPrefabs[i].brakes.maxTorque = carSelection.carUpg_SO[i].maxBrakeTorques[3];
            }
            if (TBBtnIndex[i] == 2)
            {
                upgradesHolder.carsPrefabs[i].brakes.maxTorque = carSelection.carUpg_SO[i].maxBrakeTorques[2];
            }
            if (TBBtnIndex[i] == 1)
            {
                upgradesHolder.carsPrefabs[i].brakes.maxTorque = carSelection.carUpg_SO[i].maxBrakeTorques[1];
            }
            if (TBBtnIndex[i] == 0)
            {
                upgradesHolder.carsPrefabs[i].brakes.maxTorque = carSelection.carUpg_SO[i].maxBrakeTorques[0];
            }
        }

        // Filling the TBUpgradeUI scripts references dnamically, so not do that every time manually in inspector for every new car.
        for (int i = 0; i < upgradesHolder.TBUpgUI.Length; i++)
        {
            upgradesHolder.TBUpgUI[i].TBUpgBtns = upgradesHolder.TBUpgHolder[i].GetComponentsInChildren<Button>();

            if (i > 0)
            {
                upgradesHolder.TBUpgStats[i].price[0] = upgradesHolder.TBUpgStats[i - 1].price[0] + 200;
                upgradesHolder.TBUpgStats[i].price[1] = upgradesHolder.TBUpgStats[i].price[0] * 1.4f;
                upgradesHolder.TBUpgStats[i].price[2] = upgradesHolder.TBUpgStats[i].price[1] * 1.4f;
            }

            for (int j = 0; j < 3; j++)
            {
                upgradesHolder.TBUpgUI[i].TBPriceText[j] = upgradesHolder.TBUpgUI[i].TBUpgBtns[j].GetComponentInChildren<TextMeshProUGUI>();
                upgradesHolder.TBUpgUI[i].TBPriceText[j].text = "$" + upgradesHolder.TBUpgStats[i].price[j];

                upgradesHolder.TBUpgUI[i].TBLockImages[j] = upgradesHolder.TBUpgUI[i].TBUpgBtns[j].transform.GetChild(1).GetComponent<Image>();
                // 1 means that image is at child index of 1 under btn obj, so make sure it is at index of 1 in editor as well.
            }
        }

        //Checking if all 3 TB upragdes are applied on any car, then off all the upgrades for that car
        for (int i = 0; i < TBBtnIndex.Length; i++)
        {
            if (TBBtnIndex[i] == 3)
            {
                foreach (Button btn in upgradesHolder.TBUpgUI[i].TBUpgBtns)
                {
                    btn.gameObject.SetActive(false);
                }
            }
            if (TBBtnIndex[i] == 2)
            {
                upgradesHolder.TBUpgUI[i].TBUpgBtns[TBBtnIndex[i] - 1].gameObject.SetActive(false);
                upgradesHolder.TBUpgUI[i].TBUpgBtns[TBBtnIndex[i] - 2].gameObject.SetActive(false);
                // For Lock Images
                upgradesHolder.TBUpgUI[i].TBLockImages[TBBtnIndex[i]].gameObject.SetActive(false);
            }
            if (TBBtnIndex[i] == 1)
            {
                upgradesHolder.TBUpgUI[i].TBUpgBtns[TBBtnIndex[i] - 1].gameObject.SetActive(false);
                // For Lock Images
                upgradesHolder.TBUpgUI[i].TBLockImages[TBBtnIndex[i]].gameObject.SetActive(false);
            }
            if (TBBtnIndex[i] == 0)
            {
                // For Lock Images
                upgradesHolder.TBUpgUI[i].TBLockImages[TBBtnIndex[i]].gameObject.SetActive(false);
            }
        }

        // Making all TB upg btns interactables to false, leaving only the one to true which will be the current upg btn
        for (int i = 0; i < upgradesHolder.TBUpgUI.Length; i++)
        {
            foreach (Button btn in upgradesHolder.TBUpgUI[i].TBUpgBtns)
            {
                btn.interactable = false;
            }

            if (TBBtnIndex[i] < 3)
            {
                upgradesHolder.TBUpgUI[i].TBUpgBtns[TBBtnIndex[i]].interactable = true;
            }
        }
    }

    public void UpgradeTireBrakes() //Attach this method on every tire brakes upgrade button
    {
        int currentCarIndex = PlayerPrefs.GetInt("selectedcar");

        // Firstly, check whether player have enough cash for upgrade, if not then don't proceed further, return
        string cash = carSelection.currencyItem.ValueTitle.text;
        string extractedNumbers = new string(cash.Where(char.IsDigit).ToArray());

        float result;
        if (float.TryParse(extractedNumbers, out result))
        {
            // Use the result as needed
            Debug.Log("Current Cash = " + result);
        }

        float carUpgPrice = upgradesHolder.engineUpgradesStats[currentCarIndex].price[engBtnIndex[currentCarIndex]];
        if (result >= carUpgPrice)
        {
            CBS_CurrencyTest.INSTANCE.SubtractCurrency((int)carUpgPrice);
        }
        else
        {
            StartCoroutine(UpgLowFunds());
            return;
        }

        //Play audio clip when upgrade is applied
        carSelection.upgradeAudio.Play();

        carSelection.carUpg_SO[currentCarIndex].applyTireUpg[TBBtnIndex[currentCarIndex]] = true;
        carSelection.carUpg_SO[currentCarIndex].carPower += 3;
        if (TBBtnIndex[currentCarIndex] == 0)
        {
            carSelection.carUpg_SO[currentCarIndex].boxesToFill += 1;
        }

        // for increasing top speed fill image amount
        carSelection.carUpg_SO[currentCarIndex].fillAmountsFront[3] += 0.033f;    //for handling
        if (TBBtnIndex[currentCarIndex] < 2)
        {
            carSelection.carUpg_SO[currentCarIndex].fillAmountsBack[3] += 0.033f;
        }

        //if we have enough cash then after using this upgrade, off this button and set interactable for next upg btn to true

        TBBtnIndex[currentCarIndex] = PlayerPrefs.GetInt("TBUpgBtn" + currentCarIndex);
        TBBtnIndex[currentCarIndex]++;

        if (TBBtnIndex[currentCarIndex] >= 3)
        {
            TBBtnIndex[currentCarIndex] = 3;
        }
        PlayerPrefs.SetInt("TBUpgBtn" + currentCarIndex, TBBtnIndex[currentCarIndex]);

        // for increasing car prefab's wheels tire brakes.
        upgradesHolder.carsPrefabs[currentCarIndex].brakes.maxTorque = carSelection.carUpg_SO[currentCarIndex].maxBrakeTorques[TBBtnIndex[currentCarIndex]];

        if (TBBtnIndex[currentCarIndex] < 3) //because there are only 3 upgrades for engine, so index less than 3 
        {
            upgradesHolder.TBUpgUI[currentCarIndex].TBUpgBtns[TBBtnIndex[currentCarIndex]].interactable = true;
            upgradesHolder.TBUpgUI[currentCarIndex].TBLockImages[TBBtnIndex[currentCarIndex]].gameObject.SetActive(false);
        }
    }

    IEnumerator UpgLowFunds()
    {
        upgLowFundsUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        upgLowFundsUI.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < engBtnIndex.Length; i++)
        {
            if (Input.GetKey(KeyCode.R))
            {
                PlayerPrefs.DeleteKey("EngUpgBtn" + i);
                PlayerPrefs.DeleteKey("SPUpgBtn" + i);
                PlayerPrefs.DeleteKey("TBUpgBtn" + i);
            }
        }

        UpdateCarStats();

        //for (int i = 0; i < engBtnIndex.Length; i++)
        //{
        //    if (upgradesHolder.engineUpgradesUI[i].engUpgBtns[2].gameObject.activeSelf == false) // means if last eng upg btn is also false
        //    {
        //        upgradesHolder.engineUpgradesUI[i].engBannerText.text = "ENGINE FULLY UPGRADED";
        //    }
        //    if (upgradesHolder.SPUpgUI[i].SPUpgBtns[2].gameObject.activeSelf == false)
        //    {
        //        upgradesHolder.SPUpgUI[i].SPBannerText.text = "SUSPENSION FULLY UPGRADED";
        //    }
        //    if (upgradesHolder.TBUpgUI[i].TBUpgBtns[2].gameObject.activeSelf == false)
        //    {
        //        upgradesHolder.TBUpgUI[i].TBBannerText.text = "TIRE BRAKES FULLY UPGRADED";
        //    }
        //}
    }
}

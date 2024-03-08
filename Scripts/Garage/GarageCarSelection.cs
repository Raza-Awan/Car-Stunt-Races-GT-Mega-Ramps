using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using CBS;
using CBS.UI;
using CBS.Models;
using System.Linq;

public class GarageCarSelection : MonoBehaviour
{
    public CBSCurrencyModule cbsCurrencyModule;
    public CBSCurrency cbsCurrency;
    public CurrencyItem currencyItem;
    SelectionBtnHolder selectionBtnHolder;
    UpgradesHolder upgsHolder;

    [Header("Car References")]
    public GameObject[] cars;
    private Vector3[] carsInitialPos;
    public int currentCarIndex;

    [Space]
    [Header("Car Stats")]
    public CarUpgrades_SO[] carUpg_SO;
    public CarsAvailabiltyStatus[] carsAvailabiltyStatus;

    [Space]
    [Header("UI References")]
    public Button camSwitch_Btn;
    public GameObject divider_1;
    public GameObject divider_2;
    public GameObject divider_3;
    private TextMeshProUGUI[] carPrice_text;

    //DIVIDER 1 REFERENCES
    private TextMeshProUGUI carNameBig_text;
    private TextMeshProUGUI carNameSmall_text;
    private TextMeshProUGUI carPower_text;
    //Engine
    private TextMeshProUGUI engineName_text;
    private TextMeshProUGUI engineType_text;
    //Tire
    private TextMeshProUGUI tireName_text;
    private TextMeshProUGUI tireType_text;
    //Suspension
    private TextMeshProUGUI SPName_text;
    private TextMeshProUGUI SPType_text;

    //DIVIDER 2 REFERENCES
    public TextMeshProUGUI accel_text;
    public TextMeshProUGUI maxTorque_text;
    public TextMeshProUGUI topSpeed_text;

    //DIVIDER 3 REFERENCES
    public Image[] ratingBoxes;
    private Sprite emptyBoxSprite;

    public bool restoreCarUpg_SO;

    public Button buyButton;
    public Button insufficientFundsButton;
    public Button selectButton;
    public Button upgradeButton;
    public Button backButton;    // normal back button for changing scene to main menu
    public Button backUpgButton; // this button is the one that gets on after clicking on upgrade btn
    public Button backToMainMenuBtn; // this button is the one that gets on after clicking on upgrade btn
    public GameObject lowFundsUI;

    public AudioSource upgradeAudio;

    [Space]
    [Header("Engine Upgrades References")]
    private int selectedCar; // for inspecting which car player has selected among all the unlocked/bought cars

    private void Awake()
    {
        selectionBtnHolder = GetComponent<SelectionBtnHolder>();
        upgsHolder = GetComponent<UpgradesHolder>();

        // Filling in the private References
        carNameBig_text = divider_1.transform.Find("CarNameBig_Text (TMP)").GetComponent<TextMeshProUGUI>();
        carNameSmall_text = divider_1.transform.Find("CarNameSmall_Text (TMP)").GetComponent<TextMeshProUGUI>();
        carPower_text = divider_1.transform.Find("CarPower_Text (TMP)").GetComponent<TextMeshProUGUI>();
        engineName_text = divider_1.transform.Find("EngineName_Text (TMP)").GetComponent<TextMeshProUGUI>();
        engineType_text = divider_1.transform.Find("EngineTypeText (TMP)").GetComponent<TextMeshProUGUI>();
        tireName_text = divider_1.transform.Find("TireName_Text (TMP)").GetComponent<TextMeshProUGUI>();
        tireType_text = divider_1.transform.Find("TireType_Text (TMP)").GetComponent<TextMeshProUGUI>();
        SPName_text = divider_1.transform.Find("SuspensionName_Text (TMP)").GetComponent<TextMeshProUGUI>();
        SPType_text = divider_1.transform.Find("SuspensionType_Text (TMP)").GetComponent<TextMeshProUGUI>();

        // Getting ref for cars price texts
        carPrice_text = new TextMeshProUGUI[cars.Length];   //initializing array length for storing elements
        for (int i = 0; i < cars.Length; i++)
        {
            carPrice_text[i] = selectionBtnHolder.carSelectionBtns[i].GetComponentInChildren<TextMeshProUGUI>();
            carPrice_text[i].text = carsAvailabiltyStatus[i].price.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cbsCurrencyModule = CBSModule.Get<CBSCurrencyModule>();
        currencyItem = FindObjectOfType<CurrencyItem>();
        selectedCar = PlayerPrefs.GetInt("PlayerSelectedCar");
        currentCarIndex = selectedCar; // for always starting garage scene with player current selected car
        PlayerPrefs.SetInt("selectedcar", currentCarIndex);

        //Storing cars initial pos
        carsInitialPos = new Vector3[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            carsInitialPos[i] = cars[i].transform.position;
        }

        // For cars 
        foreach (GameObject car in cars)
        {
            car.SetActive(false);
        }
        cars[currentCarIndex].SetActive(true);

        // For cars Stats
        foreach (CarsAvailabiltyStatus car in carsAvailabiltyStatus)
        {
            if (car.price == 0)
            {
                car.isUnlocked = true;
            }
            else
            {
                // means if car name exists in Playerprefs then returns 1(true) else 0(false), and name will only 
                //exists if car price is 0. This value will be setted from BuyCar function below.
                car.isUnlocked = PlayerPrefs.GetInt(car.carNameBig, 0) == 0 ? false : true;
            }
        }

        if (restoreCarUpg_SO == true)
        {
            for (int i = 0; i < carsAvailabiltyStatus.Length; i++)
            {
                carUpg_SO[i].carPower = carsAvailabiltyStatus[i].carPower;
                carUpg_SO[i].div2_Amount1 = carUpg_SO[i].XL_CurrentVal[0];          //accel
                carUpg_SO[i].div2_Amount2 = carUpg_SO[i].PW_CurrentVal[0];          //power/max torque
                carUpg_SO[i].div2_Amount3 = carUpg_SO[i].TS_CurrentVal[0];          //top speed
                carUpg_SO[i].boxesToFill = carsAvailabiltyStatus[i].boxesToFill;

                for (int j = 0; j < 4; j++)
                {
                    //For filling fill amount back values
                    carUpg_SO[i].fillAmountsBack[j] = (carUpg_SO[i].fillAmountsFront[j] + 0.033f) / carUpg_SO[i].fillAmountsFront[j];
                    carUpg_SO[i].fillAmountsBack[j] = carUpg_SO[i].fillAmountsFront[j] * carUpg_SO[i].fillAmountsBack[j];
                }

                for (int k = 0; k < 3; k++)
                {
                    //for top speed new values
                    carUpg_SO[i].TS_NewVal[k] = carUpg_SO[i].TS_CurrentVal[k + 1];
                    //for acceleration new values
                    carUpg_SO[i].XL_NewVal[k] = carUpg_SO[i].XL_CurrentVal[k + 1];
                    //for power new values
                    carUpg_SO[i].PW_NewVal[k] = carUpg_SO[i].PW_CurrentVal[k + 1];
                    //for torque/handling new values
                    carUpg_SO[i].TQ_NewVal[k] = carUpg_SO[i].TQ_CurrentVal[k + 1];
                }

                //for top speed new values
                carUpg_SO[i].TS_NewVal[3] = carUpg_SO[i].TS_CurrentVal[3];
                //for acceleration new values
                carUpg_SO[i].XL_NewVal[3] = carUpg_SO[i].XL_CurrentVal[3];
                //for power new values
                carUpg_SO[i].PW_NewVal[3] = carUpg_SO[i].PW_CurrentVal[3];
                //for torque/handling new values
                carUpg_SO[i].TQ_NewVal[3] = carUpg_SO[i].TQ_CurrentVal[3];
            }
        }

        emptyBoxSprite = ratingBoxes[0].sprite;
    }

    private void ManageCarStats()
    {
        carNameBig_text.text = carsAvailabiltyStatus[currentCarIndex].carNameBig;
        upgsHolder.engineUpgradesUI[currentCarIndex].carNameBig.text = carNameBig_text.text;
        carNameSmall_text.text = carsAvailabiltyStatus[currentCarIndex].carNameSmall;
        carPower_text.text = carUpg_SO[currentCarIndex].carPower.ToString();
        //eng upg ui
        upgsHolder.engineUpgradesUI[currentCarIndex].carNameBig.text = carNameBig_text.text;
        upgsHolder.engineUpgradesUI[currentCarIndex].carNameSmall.text = carNameSmall_text.text;
        upgsHolder.engineUpgradesUI[currentCarIndex].carPower.text = carPower_text.text;
        upgsHolder.engineUpgradesUI[currentCarIndex].engName.text = engineName_text.text;
        //sp upg ui
        upgsHolder.SPUpgUI[currentCarIndex].carNameBig.text = carNameBig_text.text;
        upgsHolder.SPUpgUI[currentCarIndex].carNameSmall.text = carNameSmall_text.text;
        upgsHolder.SPUpgUI[currentCarIndex].carPower.text = carPower_text.text;
        upgsHolder.SPUpgUI[currentCarIndex].spName.text = SPName_text.text;
        //tire upg ui
        upgsHolder.TBUpgUI[currentCarIndex].carNameBig.text = carNameBig_text.text;
        upgsHolder.TBUpgUI[currentCarIndex].carNameSmall.text = carNameSmall_text.text;
        upgsHolder.TBUpgUI[currentCarIndex].carPower.text = carPower_text.text;
        upgsHolder.TBUpgUI[currentCarIndex].tireName.text = tireName_text.text;

        accel_text.text = carUpg_SO[currentCarIndex].div2_Amount1.ToString();
        maxTorque_text.text = carUpg_SO[currentCarIndex].div2_Amount2.ToString();
        topSpeed_text.text = carUpg_SO[currentCarIndex].div2_Amount3.ToString();

        //for filling the rating boxes
        for (int i = 0; i < carUpg_SO[currentCarIndex].boxesToFill; i++)
        {
            ratingBoxes[i].sprite = null;
        }
        //for unfilling the rating boxes
        for (int i = carUpg_SO[currentCarIndex].boxesToFill; i < ratingBoxes.Length; i++)
        {
            ratingBoxes[i].sprite = emptyBoxSprite;
        }

        //Engine
        if (carUpg_SO[currentCarIndex].applyEngUpg[0] == true && carUpg_SO[currentCarIndex].applyEngUpg[1] == false && carUpg_SO[currentCarIndex].applyEngUpg[2] == false)
        {
            engineName_text.text = carUpg_SO[currentCarIndex].engName[0];
            engineType_text.text = carUpg_SO[currentCarIndex].engType[0];
        }
        else if (carUpg_SO[currentCarIndex].applyEngUpg[0] == true && carUpg_SO[currentCarIndex].applyEngUpg[1] == true && carUpg_SO[currentCarIndex].applyEngUpg[2] == false)
        {
            engineName_text.text = carUpg_SO[currentCarIndex].engName[1];
            engineType_text.text = carUpg_SO[currentCarIndex].engType[1];
        }
        else if (carUpg_SO[currentCarIndex].applyEngUpg[0] == true && carUpg_SO[currentCarIndex].applyEngUpg[1] == true && carUpg_SO[currentCarIndex].applyEngUpg[2] == true)
        {
            engineName_text.text = carUpg_SO[currentCarIndex].engName[2];
            engineType_text.text = carUpg_SO[currentCarIndex].engType[2];
        }
        else
        {
            engineName_text.text = carsAvailabiltyStatus[currentCarIndex].engName;
            engineType_text.text = carsAvailabiltyStatus[currentCarIndex].engType;
        }

        //Tire
        if (carUpg_SO[currentCarIndex].applyTireUpg[0] == true && carUpg_SO[currentCarIndex].applyTireUpg[1] == false && carUpg_SO[currentCarIndex].applyTireUpg[2] == false)
        {
            tireName_text.text = carUpg_SO[currentCarIndex].tireName[0];
            tireType_text.text = carUpg_SO[currentCarIndex].tireType[0];
        }
        else if (carUpg_SO[currentCarIndex].applyTireUpg[0] == true && carUpg_SO[currentCarIndex].applyTireUpg[1] == true && carUpg_SO[currentCarIndex].applyTireUpg[2] == false)
        {
            tireName_text.text = carUpg_SO[currentCarIndex].tireName[1];
            tireType_text.text = carUpg_SO[currentCarIndex].tireType[1];
        }
        else if (carUpg_SO[currentCarIndex].applyTireUpg[0] == true && carUpg_SO[currentCarIndex].applyTireUpg[1] == true && carUpg_SO[currentCarIndex].applyTireUpg[2] == true)
        {
            tireName_text.text = carUpg_SO[currentCarIndex].tireName[2];
            tireType_text.text = carUpg_SO[currentCarIndex].tireType[2];
        }
        else
        {
            tireName_text.text = carsAvailabiltyStatus[currentCarIndex].tireName;
            tireType_text.text = carsAvailabiltyStatus[currentCarIndex].tireType;
        }

        //Suspension
        if (carUpg_SO[currentCarIndex].applySpUpg[0] == true && carUpg_SO[currentCarIndex].applySpUpg[1] == false && carUpg_SO[currentCarIndex].applySpUpg[2] == false)
        {
            SPName_text.text = carUpg_SO[currentCarIndex].SpName[0];
            SPType_text.text = carUpg_SO[currentCarIndex].SpType[0];
        }
        else if (carUpg_SO[currentCarIndex].applySpUpg[0] == true && carUpg_SO[currentCarIndex].applySpUpg[1] == true && carUpg_SO[currentCarIndex].applySpUpg[2] == false)
        {
            SPName_text.text = carUpg_SO[currentCarIndex].SpName[1];
            SPType_text.text = carUpg_SO[currentCarIndex].SpType[1];
        }
        else if (carUpg_SO[currentCarIndex].applySpUpg[0] == true && carUpg_SO[currentCarIndex].applySpUpg[1] == true && carUpg_SO[currentCarIndex].applySpUpg[2] == true)
        {
            SPName_text.text = carUpg_SO[currentCarIndex].SpName[2];
            SPType_text.text = carUpg_SO[currentCarIndex].SpType[2];
        }
        else
        {
            SPName_text.text = carsAvailabiltyStatus[currentCarIndex].SpName;
            SPType_text.text = carsAvailabiltyStatus[currentCarIndex].SpType;
        }
    }

    private void Update()
    {
        ManageCarStats();
        UpdateUI();
    }

    public void SetCurrentCarIndex(int index)   //This method will be called automatically when btn will be pressed
    {
        cars[currentCarIndex].transform.position = carsInitialPos[currentCarIndex];
        cars[currentCarIndex].SetActive(false);

        currentCarIndex = index;    // This index is setted from selectionBtnHolder script

        cars[currentCarIndex].SetActive(true);

        //CarsAvailabiltyStatus currentCar = carsAvailabiltyStatus[currentCarIndex];
        //if (currentCar.isUnlocked == false)
        //{
        //    return; // don't change playerpref to this car if it is locked
        //}

        PlayerPrefs.SetInt("selectedcar", currentCarIndex);
    }

    public void InsufficientFunds() // Attach this function on InsufficientFunds Btn
    {
        StartCoroutine(LowFunds());
    }

    public void SelectCar() // Attach this function on Select Btn
    {
        selectedCar = PlayerPrefs.GetInt("selectedcar");
        PlayerPrefs.SetInt("PlayerSelectedCar", selectedCar);
    }

    public void BuyCar()    //Attach this function on buy car btn
    {
        CarsAvailabiltyStatus currentCar = carsAvailabiltyStatus[currentCarIndex]; //getting current selected car stats to display
        //unlock car
        currentCar.isUnlocked = true;
        PlayerPrefs.SetInt(currentCar.carNameBig, 1);
        //subtract currency on buying car with car price
        string cash = currencyItem.ValueTitle.text;
        string extractedNumbers = new string(cash.Where(char.IsDigit).ToArray());

        float playerCash;
        if (float.TryParse(extractedNumbers, out playerCash))
        {
            // Use the result as needed
        }
        Debug.Log("Current Cash = " + playerCash);

        float carPrice = currentCar.price;
        if (playerCash >= carPrice)
        {
            CBS_CurrencyTest.INSTANCE.SubtractCurrency((int)carPrice);
        }
    }

    private void UpdateUI()
    {
        CarsAvailabiltyStatus currentCar = carsAvailabiltyStatus[currentCarIndex]; //getting current selected car stats to display

        if (currentCar.isUnlocked == true) // car already bought, means car is unlocked
        {
            buyButton.gameObject.SetActive(false);
            insufficientFundsButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
        }
        else  // car not bought yet, means car is locked
        {
            string cash = currencyItem.ValueTitle.text;
            string extractedNumbers = new string(cash.Where(char.IsDigit).ToArray());

            float playerCash;
            if (float.TryParse(extractedNumbers, out playerCash))
            {
                // Use the result as needed
            }

            if (playerCash >= currentCar.price) // have enough cash to buy car
            {
                //Can Buy Car
                buyButton.gameObject.SetActive(true);
                insufficientFundsButton.gameObject.SetActive(false);
                selectButton.gameObject.SetActive(false);
            }
            else
            {
                // Low on cash, cannot buy car
                buyButton.gameObject.SetActive(false);
                insufficientFundsButton.gameObject.SetActive(true);
                selectButton.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator LowFunds()
    {
        lowFundsUI.SetActive(true);

        yield return new WaitForSeconds(1f);

        lowFundsUI.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
}

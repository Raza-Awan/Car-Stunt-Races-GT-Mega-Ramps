using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    public GameObject[] gameobject;
    public GameObject Banner;
    [SerializeField] public GameObject previousButton;
    [SerializeField] public GameObject nextButton;
    private int currentCar;
    public int Selected;

    int CashCount;
    public int[] Price;
    //public List<bool> islocked;
    public bool[] islocked;
    public bool check;
    public GameObject Buybtn, Selectedbtn, Upgradesbtn, LockBanner;
    private int count = 0;
    public Text PriceTxt;
    public GameObject PriceBanner, LowCashBanner;
    

    private void Awake()
    {
        check = true;
        SelectCar(0);
        //(START) TO KEEP THE CARS LOCKED WHEN THE USER FIRST TIME LAUNCHES THE GAME.
        if (PlayerPrefs.GetInt("GARAGESTARTEDFIRSTTIME") < 1)
        {
            for (int i = 1; i < islocked.Length; i++)// i starts from 1 because on 0 first car should always be unlocked.
            {
                islocked[i] = true;
                PlayerPrefsExtra.SetBool("isLocked" + i, islocked[i]);
            }
            count++;
            PlayerPrefs.SetInt("GARAGESTARTEDFIRSTTIME", count);
        }
        else
        {
            //islocked = PlayerPrefsExtra.GetList<bool>("isLocked");
            Debug.Log(islocked);
            for (int i = 0; i < islocked.Length; i++)
            {
                islocked[i] = (PlayerPrefsExtra.GetBool("isLocked" + i)); //Getting the state for which cars the user already bought in previous session.
            }
        }
        //(END) TO KEEP THE CARS LOCKED WHEN THE USER FIRST TIME LAUNCHES THE GAME.
    }
    public void Update()
    {
        //(START) CHECKING LOCKED OR UNLOCKED CARS PER FRAME
        if (check == true)
        {
            if (islocked[currentCar] == true)
            {
                Selectedbtn.SetActive(false);
                Buybtn.SetActive(true);
                LockBanner.SetActive(true);
                PriceBanner.SetActive(true);
                Upgradesbtn.SetActive(false);
                PriceTxt.text =  "$" + Price[currentCar].ToString();

            }
            if (islocked[currentCar] == false)
            {
                Selectedbtn.SetActive(true);
                Buybtn.SetActive(false);
                LockBanner.SetActive(false);
                PriceBanner.SetActive(false);
                Upgradesbtn.SetActive(true);
            }
        }
        //(END) CHECKING LOCKED OR UNLOCKED CARS PER FRAME
    }

    private void SelectCar(int _index)
    {
        for ( int i = 0; i < transform.childCount; i++)
        {
            gameobject[i].SetActive(i == _index);
            Debug.Log(_index);
            //PlayerPrefs.SetInt("selectedcar", _index);
            Selected = _index;
            Banner.SetActive(false);
        } 
    }
   
    public void ChangeCar(int _change)
    {
        currentCar += _change;

        if (currentCar > transform.childCount - 1)
            currentCar = 0;
        else if (currentCar < 0)
            currentCar = transform.childCount - 1;

        SelectCar(currentCar);

        int savecar = PlayerPrefs.GetInt("selectedcar");
        if (savecar == currentCar)
        {
            Banner.SetActive(true);
        }
        else
        {
            Banner.SetActive(false);
        }
    }
   
    public void SelectBtn()
    {
        PlayerPrefs.SetInt("selectedcar", Selected);
        Banner.SetActive(true);
    }

    public void BuyCar()
    {
        CashCount = PlayerPrefs.GetInt("Wallet");
        if (CashCount >= Price[currentCar])
        {
            islocked[currentCar] = false;
            for(int i = 0; i < islocked.Length; i++)
            {
                PlayerPrefsExtra.SetBool("isLocked" + i, islocked[i]); //Saving State if user bought the car.
            }
            CashCount -= Price[currentCar];
            PlayerPrefs.SetInt("Wallet", CashCount);
        }
        else
        {
           LowCashBanner.SetActive(true);
           StartCoroutine(Funds());
        }
    }
    public IEnumerator Funds()
    {
        yield return new WaitForSeconds(1.5f);
        LowCashBanner.SetActive(false);
    }
}

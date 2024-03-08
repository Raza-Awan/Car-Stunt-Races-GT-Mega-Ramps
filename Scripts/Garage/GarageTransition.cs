using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageTransition : MonoBehaviour
{
    GarageCarSelection carselection;

    public GameObject upgradeMenu;
    public GameObject statsUI;
    public GameObject carSelectionBtns;
    public GameObject rotationCam;
    public GameObject upgradeUICanvas;
    public GameObject mainCanvas;

    public GameObject[] garageLights;
    public GameObject[] garageLightBulbs;

    private Animator anim;

    public static bool isUpgrading = false;
    public bool toogleRotCam;

    private int currentCarIndex;
    private int lightIndex;

    // Start is called before the first frame update
    void Start()
    {
        carselection = GetComponent<GarageCarSelection>();

        anim = Camera.main.GetComponent<Animator>();
    }

    public void EnterUpgMenu() // Attach this on Upgrade button
    {
        isUpgrading = true;

        anim.Play("BASE ANI"); 
        StartCoroutine(ShowUpgMenu());

        // objects to ON
        carselection.backButton.gameObject.SetActive(true);

        // objects to OFF
        carSelectionBtns.SetActive(false);
        carselection.upgradeButton.gameObject.SetActive(false);
    }

    public void ExitUpgMenu() // Attach this on Back button that gets on after clicking on upgrade btn
    {
        StartCoroutine(ResetBool());

        anim.Play("BASE ANI back");
        upgradeMenu.SetActive(false);

        // objects to ON
        carselection.upgradeButton.gameObject.SetActive(true);

        // objects to OFF
        carselection.backButton.gameObject.SetActive(false);
    }

    IEnumerator ResetBool()
    {
        statsUI.SetActive(false);
        yield return new WaitForSeconds(1f);
        isUpgrading = false;
        statsUI.SetActive(true);
        carSelectionBtns.SetActive(true);
        carselection.backToMainMenuBtn.gameObject.SetActive(true);
    }

    public void BackToMainMenu() // Attach this function on simple back button 
    {
        SceneManager.LoadScene("mainmenu");
    }

    IEnumerator ShowUpgMenu()
    {
        statsUI.SetActive(false);
        carselection.backToMainMenuBtn.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        upgradeMenu.SetActive(true);
        statsUI.SetActive(true);
    }

    public void ManageGarageLights()
    {
        for (int i = 0; i < garageLights.Length; i++)
        {
            garageLights[i].SetActive(false);
            garageLightBulbs[i].SetActive(false);
            
        }
        garageLights[lightIndex].SetActive(true);
        garageLightBulbs[lightIndex].SetActive(true);
    }

    private void Update()
    {
        currentCarIndex = PlayerPrefs.GetInt("selectedcar");

        if (currentCarIndex >= 0 && currentCarIndex <= 3)
        {
            lightIndex = 0;
        }
        else if (currentCarIndex >= 4 && currentCarIndex <= 7)
        {
            lightIndex = 1;
        }
        else if (currentCarIndex >= 8 && currentCarIndex <= 11)
        {
            lightIndex = 2;
        }

        ManageGarageLights();

        if (toogleRotCam == true)
        {
            rotationCam.SetActive(true);
            upgradeUICanvas.SetActive(false);
            mainCanvas.SetActive(false);
        }
        if (toogleRotCam == false)
        {
            rotationCam.SetActive(false);
            upgradeUICanvas.SetActive(true);
            mainCanvas.SetActive(true);
        }
    }

    public void ToogleRotationCam()
    {
        toogleRotCam = !toogleRotCam;
    }
}

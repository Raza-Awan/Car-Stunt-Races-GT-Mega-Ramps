using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeightLevelSelectionHandler : MonoBehaviour
{

    public GameObject loadingScreen;
    public GameObject headBar;
    public GameObject[] Levels;
    public GameObject objectivesPopUp_Height;
    public GameObject[] tickBox;
    public GameObject[] emptyBox;
    public GameObject[] carImages;
    public TextMeshProUGUI[] missionStatements;
    public Button[] buttons;
    public Image[] lockImages;
    int Current_Level;
    int unlockedLevelsHeight;

    public bool canPlay;

    void Start()
    {
        UnlockLevelHeight();
        headBar.SetActive(true);
    }

    void Update()
    {
        if (TiresLeft.INSTANCE != null)
        {
            if (TiresLeft.INSTANCE.tiresLeft > 0)
            {
                canPlay = true;
            }
            if (TiresLeft.INSTANCE.tiresLeft <= 0)
            {
                canPlay = false;
            }

            /////////////////////////////////////////////////////////

            if (loadingScreen.activeSelf == true)
            {
                TiresLeft.isLoading = true;
            }
            else
            {
                TiresLeft.isLoading = false;
            }
        }
    }

    public void UnlockLevelHeight()
    {
        unlockedLevelsHeight = PlayerPrefs.GetInt("NextLevelHeight");

        for (int i = 0; i <= unlockedLevelsHeight; i++)
        {
            //Levels[i].gameObject.GetComponent<Button>().interactable = true;
            buttons[i].interactable = true;
            lockImages[i].enabled = false;
        }
        Current_Level = GameManager.Instance.HeightLevelNo;
        //OnClickLevelBtn();
    }
    public void OnClickLevelHeight(int Level)
    {
        GameManager.Instance.HeightLevelNo = Level;
        Current_Level = GameManager.Instance.HeightLevelNo;
        Debug.Log("level Index" + Current_Level);
    }

    public void OnClickNextHeight()
    {
        CheckForObjectiveStatus();

        for (int i = 0; i < 3; i++) // 3 because there are 3 objectives 
        {
            missionStatements[i].text = Objectives_Manager.INSTANCE.heightObjectives_SO[Current_Level].missionStatement[i];

            if (Objectives_Manager.INSTANCE.heightObjectives_SO[Current_Level].objectiveStatus[i] == true)
            {
                tickBox[i].SetActive(true);
                emptyBox[i].SetActive(false);
            }
            else
            {
                tickBox[i].SetActive(false);
                emptyBox[i].SetActive(true);
            }
        }

        int currentCarIndex = PlayerPrefs.GetInt("selectedcar");
        foreach (GameObject carImage in carImages)
        {
            carImage.SetActive(false);
        }
        carImages[currentCarIndex].SetActive(true);

        objectivesPopUp_Height.SetActive(true);
    }

    public void CheckForObjectiveStatus()
    {
        foreach (char digitChar in Objectives_Manager.INSTANCE.heightObjectives_SO[Current_Level].objsString)
        {
            int digit = int.Parse(digitChar.ToString());

            if (digit == Objectives_Manager.INSTANCE.heightObjectives_SO[Current_Level].objNumbers[0]) //for objective 1
            {
                Objectives_Manager.INSTANCE.heightObjectives_SO[Current_Level].objectiveStatus[0] = true;
            }
            if (digit == Objectives_Manager.INSTANCE.heightObjectives_SO[Current_Level].objNumbers[1]) //for objective 2
            {
                Objectives_Manager.INSTANCE.heightObjectives_SO[Current_Level].objectiveStatus[1] = true;
            }
            if (digit == Objectives_Manager.INSTANCE.heightObjectives_SO[Current_Level].objNumbers[2]) //for objective 3
            {
                Objectives_Manager.INSTANCE.heightObjectives_SO[Current_Level].objectiveStatus[2] = true;
            }
        }
    }

    public void OnClickStartHeight()
    {
        //StartCoroutine(LoadSelectedLevel());
        if (TiresLeft.INSTANCE.tiresLeft > 0 && canPlay == true)
        {
            StartCoroutine(LoadSelectedLevel());
        }
        if (TiresLeft.INSTANCE.tiresLeft <= 0 && canPlay == false)
        {
            Debug.Log("Not enough Tires!! Wait for tires to refill!!");
            StartCoroutine(TiresLeft.INSTANCE.ShowWaitForRefillText());
        }
    }

    IEnumerator LoadSelectedLevel()
    {
        // reduce 1 energy bar whenever player enters a level
        TiresLeft.INSTANCE.tiresLeft--;
        PlayerPrefs.SetInt("NumberOfTires", TiresLeft.INSTANCE.tiresLeft);
        TiresLeft.INSTANCE.tiresMinus_text.gameObject.SetActive(true);

        // enable load screen after a little delay, after playing energy reduction anim
        yield return new WaitForSeconds(2f);
        TiresLeft.INSTANCE.tiresMinus_text.gameObject.SetActive(false);
        objectivesPopUp_Height.SetActive(false);
        headBar.SetActive(false);
        loadingScreen.SetActive(true);

        // wait for 5 seconds on loading scene, then load selected level
        yield return new WaitForSeconds(5f);
        Change_SceneHeight();
    }

    public void OnClickLevelHeightBtn()
    {
        for (int i = 0; i <= unlockedLevelsHeight; i++)
        {
            if (i == Current_Level)
            {
                Levels[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }

            else
            {
                Levels[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }

        }
    }

    public void Change_SceneHeight()
    {
        GameManager.Instance.Change_Scene("HeightMaps");
    }
    //public void Change_Scene_Garage()
    //{
    //    GameManager.Instance.Change_Scene("Garage2");
    //}
}

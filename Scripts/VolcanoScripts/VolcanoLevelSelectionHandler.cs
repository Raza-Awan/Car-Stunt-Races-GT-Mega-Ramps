using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolcanoLevelSelectionHandler : MonoBehaviour
{
    public GifsManager gifsManager_Volcano;

    public GameObject loadingScreen;
    public GameObject headBar;
    public GameObject objectivesPopUp_Volcano;
    public GameObject[] tickBox;
    public GameObject[] emptyBox;
    public TextMeshProUGUI[] missionStatements;
    public GameObject[] Levels;
    public Button[] buttons;
    public Button playButton;
    public Image[] lockImages;
    int Current_Level;
    int unlockedLevelsVolcano;

    private bool canPlay;

    void Start()
    {
        UnlockLevelVolcano();
        headBar.SetActive(true);


        foreach (var btn in buttons)
        {
            btn.interactable = true;
        }
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

    public void UnlockLevelVolcano()
    {
        unlockedLevelsVolcano = PlayerPrefs.GetInt("NextLevelVolcano");

        for (int i = 0; i <= unlockedLevelsVolcano; i++)
        {
            //buttons[i].interactable = true;
            lockImages[i].enabled = false;
        }
        Current_Level = GameManager.Instance.VolcanoLevelNo;
    }
    public void OnClickLevelVolcano(int Level)
    {
        GameManager.Instance.VolcanoLevelNo = Level;
        Current_Level = GameManager.Instance.VolcanoLevelNo;

        gifsManager_Volcano.LoadSelectedLevelGif(Current_Level);

        if (Current_Level > unlockedLevelsVolcano)
        {
            playButton.gameObject.SetActive(false);
        }
        else
        {
            playButton.gameObject.SetActive(true);
        }
    }

    public void OnClickNextVolcano()
    {
        CheckForObjectiveStatus();

        for (int i = 0; i < 3; i++) // 3 because there are 3 objectives 
        {
            missionStatements[i].text = Objectives_Manager.INSTANCE.volcanoObjectives_SO[Current_Level].missionStatement[i];

            if (Objectives_Manager.INSTANCE.volcanoObjectives_SO[Current_Level].objectiveStatus[i] == true)
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

        objectivesPopUp_Volcano.SetActive(true);
    }

    public void CheckForObjectiveStatus()
    {
        foreach (char digitChar in Objectives_Manager.INSTANCE.volcanoObjectives_SO[Current_Level].objsString)
        {
            int digit = int.Parse(digitChar.ToString());

            if (digit == Objectives_Manager.INSTANCE.volcanoObjectives_SO[Current_Level].objNumbers[0]) //for objective 1
            {
                Objectives_Manager.INSTANCE.volcanoObjectives_SO[Current_Level].objectiveStatus[0] = true;
            }
            if (digit == Objectives_Manager.INSTANCE.volcanoObjectives_SO[Current_Level].objNumbers[1]) //for objective 2
            {
                Objectives_Manager.INSTANCE.volcanoObjectives_SO[Current_Level].objectiveStatus[1] = true;
            }
            if (digit == Objectives_Manager.INSTANCE.volcanoObjectives_SO[Current_Level].objNumbers[2]) //for objective 3
            {
                Objectives_Manager.INSTANCE.volcanoObjectives_SO[Current_Level].objectiveStatus[2] = true;
            }
        }
    }

    public void OnClickStartVolcano()
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
        yield return new WaitForSeconds(1.2f);
        TiresLeft.INSTANCE.tiresMinus_text.gameObject.SetActive(false);
        objectivesPopUp_Volcano.SetActive(false);
        headBar.SetActive(false);
        loadingScreen.SetActive(true);

        // wait for 5 seconds on loading scene, then load selected level
        yield return new WaitForSeconds(5f);
        Change_SceneVolcano();
    }

    public void OnClickLevelVolcanoBtn()
    {
        for (int i = 0; i <= unlockedLevelsVolcano; i++)
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

    public void Change_SceneVolcano()
    {
        GameManager.Instance.Change_Scene("volcano");
    }
}

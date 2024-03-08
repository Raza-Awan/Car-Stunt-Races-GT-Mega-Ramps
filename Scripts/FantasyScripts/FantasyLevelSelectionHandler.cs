using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FantasyLevelSelectionHandler : MonoBehaviour
{
    public GifsManager gifsManager_Fantasy;

    public GameObject loadingScreen;
    public GameObject headBar;
    public GameObject objectivesPopUp_Fantasy;
    public GameObject[] tickBox;
    public GameObject[] emptyBox;
    public TextMeshProUGUI[] missionStatements;
    public GameObject[] Levels;
    public Button[] buttons;
    public Button playButton;
    public Image[] lockImages;
    int Current_Level;
    int unlockedLevelsFantasy;

    private bool canPlay;

    void Start()
    {
        //audioSource.Play();
        UnlockLevelFantasy();
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

    public void UnlockLevelFantasy()
    {
        unlockedLevelsFantasy = PlayerPrefs.GetInt("NextLevelFantasy");

        for (int i = 0; i <= unlockedLevelsFantasy; i++)
        {
            //buttons[i].interactable = true;
            lockImages[i].enabled = false;
        }
        Current_Level = GameManager.Instance.FantasyLevelNo;
    }
    public void OnClickLevelFantasy(int Level)
    {
        GameManager.Instance.FantasyLevelNo = Level;
        Current_Level = GameManager.Instance.FantasyLevelNo;

        //gifsManager_Fantasy.LoadSelectedLevelGif(Current_Level);

        if (Current_Level > unlockedLevelsFantasy)
        {
            playButton.gameObject.SetActive(false);
        }
        else
        {
            playButton.gameObject.SetActive(true);
        }
    }

    public void OnClickNextFantasy()
    {
        CheckForObjectiveStatus();

        for (int i = 0; i < 3; i++) // 3 because there are 3 objectives 
        {
            missionStatements[i].text = Objectives_Manager.INSTANCE.fantasyObjectives_SO[Current_Level].missionStatement[i];

            if (Objectives_Manager.INSTANCE.fantasyObjectives_SO[Current_Level].objectiveStatus[i] == true)
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

        objectivesPopUp_Fantasy.SetActive(true);
    }

    public void CheckForObjectiveStatus()
    {
        foreach (char digitChar in Objectives_Manager.INSTANCE.fantasyObjectives_SO[Current_Level].objsString)
        {
            int digit = int.Parse(digitChar.ToString());

            if (digit == Objectives_Manager.INSTANCE.fantasyObjectives_SO[Current_Level].objNumbers[0]) //for objective 1
            {
                Objectives_Manager.INSTANCE.fantasyObjectives_SO[Current_Level].objectiveStatus[0] = true;
            }
            if (digit == Objectives_Manager.INSTANCE.fantasyObjectives_SO[Current_Level].objNumbers[1]) //for objective 2
            {
                Objectives_Manager.INSTANCE.fantasyObjectives_SO[Current_Level].objectiveStatus[1] = true;
            }
            if (digit == Objectives_Manager.INSTANCE.fantasyObjectives_SO[Current_Level].objNumbers[2]) //for objective 3
            {
                Objectives_Manager.INSTANCE.fantasyObjectives_SO[Current_Level].objectiveStatus[2] = true;
            }
        }
    }

    public void OnClickStartFantasy()
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
        objectivesPopUp_Fantasy.SetActive(false);
        headBar.SetActive(false);
        loadingScreen.SetActive(true);

        // wait for 5 seconds on loading scene, then load selected level
        yield return new WaitForSeconds(5f);
        Change_SceneFantasy();
    }

    public void OnClickLevelFantasyBtn()
    {
        for (int i = 0; i <= unlockedLevelsFantasy; i++)
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

    public void Change_SceneFantasy()
    {
        GameManager.Instance.Change_Scene("FantasyScene");
    }
}

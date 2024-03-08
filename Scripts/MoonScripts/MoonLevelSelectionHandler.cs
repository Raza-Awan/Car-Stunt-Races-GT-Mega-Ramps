using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoonLevelSelectionHandler : MonoBehaviour
{
    public GifsManager gifsManager_Moon;

    public GameObject loadingScreen;
    public GameObject headBar;
    public GameObject objectivesPopUp_Moon;
    public GameObject[] tickBox;
    public GameObject[] emptyBox;
    public TextMeshProUGUI[] missionStatements;
    public GameObject[] Levels;
    public Button[] buttons;
    public Button playButton;
    public Image[] lockImages;
    int Current_Level;
    int unlockedLevelsMoon;

    private bool canPlay;

    void Start()
    {
        //audioSource.Play();
        UnlockLevelMoon();
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

    public void UnlockLevelMoon()
    {
        unlockedLevelsMoon = PlayerPrefs.GetInt("NextLevelMoon");

        for (int i = 0; i <= unlockedLevelsMoon; i++)
        {
            //buttons[i].interactable = true;
            lockImages[i].enabled = false;
        }
        Current_Level = GameManager.Instance.MoonLevelNo;
    }
    public void OnClickLevelMoon(int Level)
    {
        GameManager.Instance.MoonLevelNo = Level;
        Current_Level = GameManager.Instance.MoonLevelNo;

        gifsManager_Moon.LoadSelectedLevelGif(Current_Level);

        if (Current_Level > unlockedLevelsMoon)
        {
            playButton.gameObject.SetActive(false);
        }
        else
        {
            playButton.gameObject.SetActive(true);
        }
    }

    public void OnClickNextMoon()
    {
        CheckForObjectiveStatus();

        for (int i = 0; i < 3; i++) // 3 because there are 3 objectives 
        {
            missionStatements[i].text = Objectives_Manager.INSTANCE.moonObjectives_SO[Current_Level].missionStatement[i];

            if (Objectives_Manager.INSTANCE.moonObjectives_SO[Current_Level].objectiveStatus[i] == true)
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

        objectivesPopUp_Moon.SetActive(true);
    }

    public void CheckForObjectiveStatus()
    {
        foreach (char digitChar in Objectives_Manager.INSTANCE.moonObjectives_SO[Current_Level].objsString)
        {
            int digit = int.Parse(digitChar.ToString());

            if (digit == Objectives_Manager.INSTANCE.moonObjectives_SO[Current_Level].objNumbers[0]) //for objective 1
            {
                Objectives_Manager.INSTANCE.moonObjectives_SO[Current_Level].objectiveStatus[0] = true;
            }
            if (digit == Objectives_Manager.INSTANCE.moonObjectives_SO[Current_Level].objNumbers[1]) //for objective 2
            {
                Objectives_Manager.INSTANCE.moonObjectives_SO[Current_Level].objectiveStatus[1] = true;
            }
            if (digit == Objectives_Manager.INSTANCE.moonObjectives_SO[Current_Level].objNumbers[2]) //for objective 3
            {
                Objectives_Manager.INSTANCE.moonObjectives_SO[Current_Level].objectiveStatus[2] = true;
            }
        }
    }

    public void OnClickStartMoon()
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
        objectivesPopUp_Moon.SetActive(false);
        headBar.SetActive(false);
        loadingScreen.SetActive(true);

        // wait for 5 seconds on loading scene, then load selected level
        yield return new WaitForSeconds(5f);
        Change_SceneMoon();
    }

    public void OnClickLevelMoonBtn()
    {
        for (int i = 0; i <= unlockedLevelsMoon; i++)
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

    public void Change_SceneMoon()
    {
        GameManager.Instance.Change_Scene("MoonGameplay");
    }
}

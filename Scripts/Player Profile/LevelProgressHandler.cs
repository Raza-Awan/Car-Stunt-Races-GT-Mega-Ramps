using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressHandler : MonoBehaviour
{
    public static LevelProgressHandler INSTANCE;

    public Text levelNumberText;
    public Text levelProgressText;
    public int playerLevel;
    public int currentProgressValue;
    public int maxProgressValue;

    public Slider progressFill_Slider;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Max Progress level
        if (PlayerPrefs.HasKey("Max_Prog_Lvl"))
        {
            maxProgressValue = PlayerPrefs.GetInt("Max_Prog_Lvl");
        }
        else
        {
            maxProgressValue = 100;
        }

        // Current Player Progress level
        if (PlayerPrefs.HasKey("Curnt_Prog_Lvl"))
        {
            currentProgressValue = PlayerPrefs.GetInt("Curnt_Prog_Lvl");
        }
        else
        {
            currentProgressValue = 0;
        }

        // Player Current Level Number
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            playerLevel = PlayerPrefs.GetInt("PlayerLevel");
            levelNumberText.text = "LEVEL:" + playerLevel;
        }
        else
        {
            playerLevel = 1;
            levelNumberText.text = "LEVEL:" + playerLevel;
        }

        levelProgressText.text = currentProgressValue + " / " + maxProgressValue;
        SetProgressValue(maxProgressValue, currentProgressValue);
    }

    public void SetProgressValue(int maxValue, int currentValue)
    {
        progressFill_Slider.maxValue = maxValue;
        progressFill_Slider.value = currentValue;
        PlayerPrefs.SetInt("Max_Prog_Lvl", maxValue);
        PlayerPrefs.SetInt("Curnt_Prog_Lvl", currentValue);
    }

    public void SetProgressValueOnFirstFinish() //When clearing the level first time
    {
        currentProgressValue = PlayerPrefs.GetInt("Curnt_Prog_Lvl");
        currentProgressValue += 30;

        maxProgressValue = PlayerPrefs.GetInt("Max_Prog_Lvl");

        if (currentProgressValue >= maxProgressValue)
        {
            maxProgressValue += 60;
            currentProgressValue = 0;
            playerLevel++;
        }

        PlayerPrefs.SetInt("Max_Prog_Lvl", maxProgressValue);
        PlayerPrefs.SetInt("Curnt_Prog_Lvl", currentProgressValue);
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
    }

    public void SetProgressValueOnReplay()  //When clearing the level again/replay or on second time
    {
        currentProgressValue = PlayerPrefs.GetInt("Curnt_Prog_Lvl");
        currentProgressValue += 10;

        maxProgressValue = PlayerPrefs.GetInt("Max_Prog_Lvl");

        if (currentProgressValue >= maxProgressValue)
        {
            maxProgressValue += 60;
            currentProgressValue = 0;
            playerLevel++;
        }

        PlayerPrefs.SetInt("Max_Prog_Lvl", maxProgressValue);
        PlayerPrefs.SetInt("Curnt_Prog_Lvl", currentProgressValue);
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
    }
}

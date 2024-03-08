using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeMaster : MonoBehaviour
{
    DateTime currentDate;
    DateTime oldDate;
    public static TimeMaster instance;

    private bool isRewardReady = false;
    private void Awake()
    {
        instance = this;
    }

    public float checkDate(string saveName)
    {
        currentDate = System.DateTime.Now;
        string tempString = PlayerPrefs.GetString(saveName);
        Debug.Log(tempString);
        if (tempString != "")
        {
            oldDate = DateTime.Parse(tempString);
            Debug.Log("Old Date" + oldDate);

            TimeSpan difference = currentDate - oldDate;
            Debug.Log(difference.TotalSeconds);
            return (float)difference.TotalSeconds;
        }
        else
        {
            return 0;
        }
        //DateTime savedtime
        //long templong = (long)Convert.ToInt64(tempString);

    }

    public void SaveDate(string saveName)
    {
        PlayerPrefs.SetString(saveName, DateTime.Now.ToString());
        
    }

}

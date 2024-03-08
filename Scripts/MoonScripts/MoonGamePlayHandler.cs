using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase.Analytics;

public class MoonGamePlayHandler : MonoBehaviour
{
    public static MoonGamePlayHandler Instance;
    public Moon_LevelInfo[] Moon_Levels;
    public GameObject player;
    [HideInInspector] public int Moon_Levelno;
    //public int count;
    CountdownTimer Timer;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        //Timer = FindObjectOfType<CountdownTimer>();
        Moon_Levelno = GameManager.Instance.MoonLevelNo;
       // ActivePlayer();
        ActiveLevel();
        Debug.Log(Moon_Levelno + "=gameplayindex");
    }
    public void ActivePlayer()
    {
        player.transform.position = Moon_Levels[Moon_Levelno].PlayerPosition.transform.position;
        player.transform.rotation = Moon_Levels[Moon_Levelno].PlayerPosition.transform.rotation;
        //Gta_Levels[Gta_Levelno].endPoint.gameObject.SetActive(false);
    }
    
    public void ActiveLevel()
    {
        for(int i = 0; i <= Moon_Levels.Length-1; i++)
        {
            if(i == Moon_Levelno)
            {
                //Debug.Log("level value" + Gta_Levelno);
                Moon_Levels[i].MoonLevel.gameObject.SetActive(true);
                //count = i;
                //Debug.Log("Count Value Level: " + count);
                //FirebaseAnalytics.LogEvent("Level_Started_My_New", "Level_Number_My", count);
            }
            else
            {
                Moon_Levels[i].MoonLevel.gameObject.SetActive(false);
            }
        }
    }
  
}

[System.Serializable]
public class Moon_LevelInfo
{
    public GameObject MoonLevel;
    public Transform PlayerPosition;
}
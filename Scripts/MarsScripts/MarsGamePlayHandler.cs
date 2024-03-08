using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase.Analytics;

public class MarsGamePlayHandler : MonoBehaviour
{
    public static MarsGamePlayHandler Instance;
    public Mars_LevelInfo[] Mars_Levels;
    public GameObject player;
    [HideInInspector] public int Mars_Levelno;
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
        Mars_Levelno = GameManager.Instance.MarsLevelNo;
       // ActivePlayer();
        ActiveLevel();
        //Debug.Log(Gta_Levelno + "=gameplayindex");
    }
    public void ActivePlayer()
    {
        player.transform.position = Mars_Levels[Mars_Levelno].PlayerPosition.transform.position;
        player.transform.rotation = Mars_Levels[Mars_Levelno].PlayerPosition.transform.rotation;
        //Gta_Levels[Gta_Levelno].endPoint.gameObject.SetActive(false);
    }
    
    public void ActiveLevel()
    {
        for(int i = 0; i <= Mars_Levels.Length-1; i++)
        {
            if(i == Mars_Levelno)
            {
                //Debug.Log("level value" + Gta_Levelno);
                Mars_Levels[i].MarsLevel.gameObject.SetActive(true);
                //count = i;
                //Debug.Log("Count Value Level: " + count);
                //FirebaseAnalytics.LogEvent("Level_Started_My_New", "Level_Number_My", count);
            }
            else
            {
                Mars_Levels[i].MarsLevel.gameObject.SetActive(false);
            }
        }
    }
  
}

[System.Serializable]
public class Mars_LevelInfo
{
    public GameObject MarsLevel;
    public Transform PlayerPosition;
}
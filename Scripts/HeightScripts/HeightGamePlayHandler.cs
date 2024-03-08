using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase.Analytics;

public class HeightGamePlayHandler : MonoBehaviour
{
    public static HeightGamePlayHandler Instance;
    public Height_LevelInfo[] Height_Levels;
    public GameObject player;
    [HideInInspector] public int Height_Levelno;
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
        Height_Levelno = GameManager.Instance.HeightLevelNo;
       // ActivePlayer();
        ActiveLevel();
        Debug.Log(Height_Levelno + "=gameplayindex");
    }
    public void ActivePlayer()
    {
        player.transform.position = Height_Levels[Height_Levelno].PlayerPosition.transform.position;
        player.transform.rotation = Height_Levels[Height_Levelno].PlayerPosition.transform.rotation;
        //Gta_Levels[Gta_Levelno].endPoint.gameObject.SetActive(false);
    }
    
    public void ActiveLevel()
    {
        for(int i = 0; i <= Height_Levels.Length-1; i++)
        {
            if(i == Height_Levelno)
            {
                //Debug.Log("level value" + Gta_Levelno);
                Height_Levels[i].HeightLevel.gameObject.SetActive(true);
                //count = i;
                //Debug.Log("Count Value Level: " + count);
                //FirebaseAnalytics.LogEvent("Level_Started_My_New", "Level_Number_My", count);
            }
            else
            {
                Height_Levels[i].HeightLevel.gameObject.SetActive(false);
            }
        }
    }
  
}

[System.Serializable]
public class Height_LevelInfo
{
    public GameObject HeightLevel;
    public Transform PlayerPosition;
}
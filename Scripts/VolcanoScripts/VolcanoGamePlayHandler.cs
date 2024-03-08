using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase.Analytics;

public class VolcanoGamePlayHandler : MonoBehaviour
{
    public static VolcanoGamePlayHandler Instance;
    public Volcano_LevelInfo[] Volcano_Levels;
    public GameObject player;
    [HideInInspector] public int Volcano_Levelno;
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
        Volcano_Levelno = GameManager.Instance.VolcanoLevelNo;
       // ActivePlayer();
        ActiveLevel();
        Debug.Log(Volcano_Levelno + "=gameplayindex");
    }
    public void ActivePlayer()
    {
        player.transform.position = Volcano_Levels[Volcano_Levelno].PlayerPosition.transform.position;
        player.transform.rotation = Volcano_Levels[Volcano_Levelno].PlayerPosition.transform.rotation;
        //Gta_Levels[Gta_Levelno].endPoint.gameObject.SetActive(false);
    }
    
    public void ActiveLevel()
    {
        for(int i = 0; i <= Volcano_Levels.Length-1; i++)
        {
            if(i == Volcano_Levelno)
            {
                //Debug.Log("level value" + Gta_Levelno);
                Volcano_Levels[i].VolcanoLevel.gameObject.SetActive(true);
                //count = i;
                //Debug.Log("Count Value Level: " + count);
                //FirebaseAnalytics.LogEvent("Level_Started_My_New", "Level_Number_My", count);
            }
            else
            {
                Volcano_Levels[i].VolcanoLevel.gameObject.SetActive(false);
            }
        }
    }
  
}

[System.Serializable]
public class Volcano_LevelInfo
{
    public GameObject VolcanoLevel;
    public Transform PlayerPosition;
}
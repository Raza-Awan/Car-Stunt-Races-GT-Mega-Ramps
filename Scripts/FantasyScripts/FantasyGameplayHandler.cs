using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasyGameplayHandler : MonoBehaviour
{
    public static FantasyGameplayHandler Instance;

    public Fantasy_LevelInfo[] fantasy_Levels;
    [HideInInspector] public int fantasy_Levelno;
    public int count;
    CountdownTimer Timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Timer = FindObjectOfType<CountdownTimer>();
        fantasy_Levelno = GameManager.Instance.FantasyLevelNo;
        ActiveLevel();
        Debug.Log(fantasy_Levelno + " = gameplayindex");
    }

    public void ActiveLevel()
    {
        for (int i = 0; i <= fantasy_Levels.Length - 1; i++)
        {
            if (i == fantasy_Levelno)
            {
                Debug.Log("level value = " + fantasy_Levelno);
                fantasy_Levels[i].Level.gameObject.SetActive(true);
                count = i;
            }
            else
            {
                fantasy_Levels[i].Level.gameObject.SetActive(false);
            }
        }
    }

}

[System.Serializable]
public class Fantasy_LevelInfo
{
    public GameObject Level;
    public Transform PlayerPosition;
}

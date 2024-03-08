using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCamTriggerManager : MonoBehaviour
{
    public AirCamTrigger[] airCamTriggers;

    public static int airCamTrigCount;

    public int check;

    // Start is called before the first frame update
    void Start()
    {
        airCamTrigCount = 0;

        if (CanvasCamRef.gamePlay_Scene == true)
        {
            airCamTriggers = GamePlayHandler.Instance.Gta_Levels[GameManager.Instance.LevelNo].
                             Level.GetComponentsInChildren<AirCamTrigger>();
        }

        if (CanvasCamRef.marsGamePlay_Scene == true)
        {
            airCamTriggers = MarsGamePlayHandler.Instance.Mars_Levels[GameManager.Instance.MarsLevelNo].
                             MarsLevel.GetComponentsInChildren<AirCamTrigger>();
        }

        if (CanvasCamRef.volcanoGamePlay_Scene == true)
        {
            airCamTriggers = VolcanoGamePlayHandler.Instance.Volcano_Levels[GameManager.Instance.VolcanoLevelNo].
                             VolcanoLevel.GetComponentsInChildren<AirCamTrigger>();
        }

        if (CanvasCamRef.moonGamePlay_Scene == true)
        {
            airCamTriggers = MoonGamePlayHandler.Instance.Moon_Levels[GameManager.Instance.MoonLevelNo].
                             MoonLevel.GetComponentsInChildren<AirCamTrigger>();
        }

        if (CanvasCamRef.fantasyGamePlay_Scene == true)
        {
            airCamTriggers = FantasyGameplayHandler.Instance.fantasy_Levels[GameManager.Instance.FantasyLevelNo].
                             Level.GetComponentsInChildren<AirCamTrigger>();
        }

        foreach (var airCamTrig in airCamTriggers)
        {
            airCamTrig.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        check = airCamTrigCount;
        airCamTrigCount = Mathf.Clamp(airCamTrigCount, 0, airCamTriggers.Length - 1);
        airCamTriggers[airCamTrigCount].enabled = true;
    }
}

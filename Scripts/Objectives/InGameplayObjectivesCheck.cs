using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameplayObjectivesCheck : MonoBehaviour
{
    public GameObject[] tickBox;
    public GameObject[] emptyBox;
    public TextMeshProUGUI[] missionStatements;

    int Current_Level;

    // Start is called before the first frame update
    void Update()
    {
        EarthGameplayMissions();
        MarsGameplayMissions();
        VolcanoGameplayMissions();
        MoonGameplayMissions();
        FantasyGameplayMissions();
    }

    private void EarthGameplayMissions()
    {
        if (CanvasCamRef.gamePlay_Scene == true)
        {
            Current_Level = GameManager.Instance.LevelNo;
            //Debug.Log("Current Earth Level Number = " + Current_Level);

            for (int i = 0; i < 3; i++) // 3 because there are 3 objectives 
            {
                missionStatements[i].text = Objectives_Manager.INSTANCE.earthObjectives_SO[Current_Level].missionStatement[i];

                if (Objectives_Manager.INSTANCE.earthObjectives_SO[Current_Level].objectiveStatus[i] == true)
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
        }
    }

    private void MarsGameplayMissions()
    {
        if (CanvasCamRef.marsGamePlay_Scene == true)
        {
            Current_Level = GameManager.Instance.MarsLevelNo;
            //Debug.Log("Current Mars Level Number = " + Current_Level);

            for (int i = 0; i < 3; i++) // 3 because there are 3 objectives 
            {
                missionStatements[i].text = Objectives_Manager.INSTANCE.marsObjectives_SO[Current_Level].missionStatement[i];

                if (Objectives_Manager.INSTANCE.marsObjectives_SO[Current_Level].objectiveStatus[i] == true)
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
        }
    }

    private void VolcanoGameplayMissions()
    {
        if (CanvasCamRef.volcanoGamePlay_Scene == true)
        {
            Current_Level = GameManager.Instance.VolcanoLevelNo;
            //Debug.Log("Current volcano Level Number = " + Current_Level);

            for (int i = 0; i < 3; i++) // 3 because there are 3 objectives 
            {
                missionStatements[i].text = Objectives_Manager.INSTANCE.volcanoObjectives_SO[Current_Level].missionStatement[i];

                if (Objectives_Manager.INSTANCE.volcanoObjectives_SO[Current_Level].objectiveStatus[i] == true)
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
        }
    }

    private void MoonGameplayMissions()
    {
        if (CanvasCamRef.moonGamePlay_Scene == true)
        {
            Current_Level = GameManager.Instance.MoonLevelNo;
            //Debug.Log("Current Moon Level Number = " + Current_Level);

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
        }
    }

    private void FantasyGameplayMissions()
    {
        if (CanvasCamRef.fantasyGamePlay_Scene == true)
        {
            Current_Level = GameManager.Instance.FantasyLevelNo;
            //Debug.Log("Current Fantasy Level Number = " + Current_Level);

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
        }
    }
}

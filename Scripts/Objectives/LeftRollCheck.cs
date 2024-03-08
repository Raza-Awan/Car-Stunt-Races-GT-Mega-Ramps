using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRollCheck : MonoBehaviour
{
    public int levelNo;
    public int objectiveNo;

    public int numberOfRollsToCheck;

    public bool objectiveComplete;

    bool check;

    // Start is called before the first frame update
    void Start()
    {
        CheckForScene();
        check = true;
    }

    private void CheckForScene()
    {
        // Earth Gameplay
        if (CanvasCamRef.gamePlay_Scene == true)
        {
            if (Objectives_Manager.INSTANCE.UpdateEarthObjectives
                (Objectives_Manager.INSTANCE.earthObjectives_SO[levelNo - 1].objsString, objectiveNo) == true)
            {
                Objectives_Manager.INSTANCE.earthObjectives_SO[levelNo - 1].objectiveStatus[objectiveNo - 1] = true;
                objectiveComplete = true;
            }
        }

        // Mars Gameplay
        if (CanvasCamRef.marsGamePlay_Scene == true)
        {
            if (Objectives_Manager.INSTANCE.UpdateMarsObjectives
                 (Objectives_Manager.INSTANCE.marsObjectives_SO[levelNo - 1].objsString, objectiveNo) == true)
            {
                Objectives_Manager.INSTANCE.marsObjectives_SO[levelNo - 1].objectiveStatus[objectiveNo - 1] = true;
                objectiveComplete = true;
            }
        }

        // Volcano Gameplay
        if (CanvasCamRef.volcanoGamePlay_Scene == true)
        {
            if (Objectives_Manager.INSTANCE.UpdateVolcanoObjectives
                (Objectives_Manager.INSTANCE.volcanoObjectives_SO[levelNo - 1].objsString, objectiveNo) == true)
            {
                Objectives_Manager.INSTANCE.volcanoObjectives_SO[levelNo - 1].objectiveStatus[objectiveNo - 1] = true;
                objectiveComplete = true;
            }
        }

        // Moon Gameplay
        if (CanvasCamRef.moonGamePlay_Scene == true)
        {
            if (Objectives_Manager.INSTANCE.UpdateMoonObjectives
                (Objectives_Manager.INSTANCE.moonObjectives_SO[levelNo - 1].objsString, objectiveNo) == true)
            {
                Objectives_Manager.INSTANCE.moonObjectives_SO[levelNo - 1].objectiveStatus[objectiveNo - 1] = true;
                objectiveComplete = true;
            }
        }

        // Fantasy Gameplay
        if (CanvasCamRef.fantasyGamePlay_Scene == true)
        {
            if (Objectives_Manager.INSTANCE.UpdateFantasyObjectives
                (Objectives_Manager.INSTANCE.fantasyObjectives_SO[levelNo - 1].objsString, objectiveNo) == true)
            {
                Objectives_Manager.INSTANCE.fantasyObjectives_SO[levelNo - 1].objectiveStatus[objectiveNo - 1] = true;
                objectiveComplete = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SaveObjectiveStatus();

        if (objectiveComplete == true)
        {
            return;
        }

        if (numberOfRollsToCheck == AirCarControl.INSTANCE.leftRollCount && objectiveComplete == false)
        {
            AirCarControl.INSTANCE.objectiveBonus += 20;
            objectiveComplete = true;
        }
    }

    void SaveObjectiveStatus()
    {
        //Earth Scene
        if (CanvasCamRef.gamePlay_Scene == true && objectiveComplete == true && Ghost_Recorder.levelFinished == true)
        {
            if (check == true)
            {
                string objString = PlayerPrefs.GetString("EarthObjs" + (levelNo - 1));
                if (objString.Length < 4)
                {
                    objString += objectiveNo;
                    Debug.Log("String Length = " + objString.Length);
                    PlayerPrefs.SetString("EarthObjs" + (levelNo - 1), objString);
                    Objectives_Manager.INSTANCE.earthObjectives_SO[levelNo - 1].objsString += objectiveNo;
                }

                Objectives_Manager.INSTANCE.earthObjectives_SO[levelNo - 1].objectiveStatus[objectiveNo - 1] = true;

                check = false;
            }
        }
        // Mars Scene
        if (CanvasCamRef.marsGamePlay_Scene == true && objectiveComplete == true && Ghost_Recorder.levelFinished == true)
        {
            if (check == true)
            {
                string objString = PlayerPrefs.GetString("MarsObjs" + (levelNo - 1));
                if (objString.Length < 4)
                {
                    objString += objectiveNo;
                    PlayerPrefs.SetString("MarsObjs" + (levelNo - 1), objString);
                    Objectives_Manager.INSTANCE.marsObjectives_SO[levelNo - 1].objsString += objectiveNo;
                }

                Objectives_Manager.INSTANCE.marsObjectives_SO[levelNo - 1].objectiveStatus[objectiveNo - 1] = true;

                check = false;
            }
        }
        // Volcano Scene
        if (CanvasCamRef.volcanoGamePlay_Scene == true && objectiveComplete == true && Ghost_Recorder.levelFinished == true)
        {
            if (check == true)
            {
                string objString = PlayerPrefs.GetString("VolcanoObjs" + (levelNo - 1));
                if (objString.Length < 4)
                {
                    objString += objectiveNo;
                    PlayerPrefs.SetString("VolcanoObjs" + (levelNo - 1), objString);
                    Objectives_Manager.INSTANCE.volcanoObjectives_SO[levelNo - 1].objsString += objectiveNo;
                }

                Objectives_Manager.INSTANCE.volcanoObjectives_SO[levelNo - 1].objectiveStatus[objectiveNo - 1] = true;

                check = false;
            }
        }
        // Moon Scene
        if (CanvasCamRef.moonGamePlay_Scene == true && objectiveComplete == true && Ghost_Recorder.levelFinished == true)
        {
            if (check == true)
            {
                string objString = PlayerPrefs.GetString("MoonObjs" + (levelNo - 1));
                if (objString.Length < 4)
                {
                    objString += objectiveNo;
                    PlayerPrefs.SetString("MoonObjs" + (levelNo - 1), objString);
                    Objectives_Manager.INSTANCE.moonObjectives_SO[levelNo - 1].objsString += objectiveNo;
                }

                Objectives_Manager.INSTANCE.moonObjectives_SO[levelNo - 1].objectiveStatus[objectiveNo - 1] = true;

                check = false;
            }
        }
        // Fantasy Scene
        if (CanvasCamRef.fantasyGamePlay_Scene == true && objectiveComplete == true && Ghost_Recorder.levelFinished == true)
        {
            if (check == true)
            {
                string objString = PlayerPrefs.GetString("FantasyObjs" + (levelNo - 1));
                if (objString.Length < 4)
                {
                    objString += objectiveNo;
                    PlayerPrefs.SetString("FantasyObjs" + (levelNo - 1), objString);
                    Objectives_Manager.INSTANCE.fantasyObjectives_SO[levelNo - 1].objsString += objectiveNo;
                }

                Objectives_Manager.INSTANCE.fantasyObjectives_SO[levelNo - 1].objectiveStatus[objectiveNo - 1] = true;

                check = false;
            }
        }
    }
}

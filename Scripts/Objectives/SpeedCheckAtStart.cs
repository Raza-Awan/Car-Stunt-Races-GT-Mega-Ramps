using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCheckAtStart : MonoBehaviour
{
    public int levelNo;
    public int objectiveNo;

    private float SPEED;
    private float speedMultiplier = 3.6182f;    // Multiply speed with this value to make it same as nwh vehicles meter values.

    public float targetSpeed;
    public float targetTimeValue;
    private float timer;

    public bool startTimer;
    public bool objectiveComplete;

    public bool check;


    // Start is called before the first frame update
    void Start()
    {
        startTimer = false;
        check = true;
        timer = targetTimeValue;
        CheckForScene();
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

        //Earth Scene
        if (CanvasCamRef.gamePlay_Scene == true)
        {
            SPEED = GameplayCar.INSTANCE.playerVehicleController.Speed * speedMultiplier;
        }
        // Mars Scene
        if (CanvasCamRef.marsGamePlay_Scene == true)
        {
            SPEED = MarsGameplayCar.INSTANCE.playerVehicleController.Speed * speedMultiplier;
        }
        // Volcano Scene
        if (CanvasCamRef.volcanoGamePlay_Scene == true)
        {
            SPEED = VolcanoGameplayCar.INSTANCE.playerVehicleController.Speed * speedMultiplier;
        }
        // Moon Scene
        if (CanvasCamRef.moonGamePlay_Scene == true)
        {
            SPEED = MoonGameplayCar.INSTANCE.playerVehicleController.Speed * speedMultiplier;
        }
        // Fantasy Scene
        if (CanvasCamRef.fantasyGamePlay_Scene == true)
        {
            SPEED = FantasyGameplayCar.INSTANCE.playerVehicleController.Speed * speedMultiplier;
        }


        if (CountdownTimer.INSTANCE.TimerOn == false && objectiveComplete == false)
        {
            startTimer = true;
        }
        else
        {
            startTimer = false;
        }

        if (startTimer == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer >= 0 && objectiveComplete == false && SPEED >= targetSpeed)
        {
            AirCarControl.INSTANCE.objectiveBonus += 20;
            Debug.Log("SPEED CHECK bonus = " + AirCarControl.INSTANCE.objectiveBonus);
            objectiveComplete = true;
        }

        if (timer <= 0)
        {
            timer = -1;
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

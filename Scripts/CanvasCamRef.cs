using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasCamRef : MonoBehaviour
{
    public static bool gamePlay_Scene;
    public static bool moonGamePlay_Scene;
    public static bool volcanoGamePlay_Scene;
    public static bool marsGamePlay_Scene;
    public static bool heightGamePlay_Scene;
    public static bool fantasyGamePlay_Scene;

    Canvas canvas;

    private void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // For Earth GamePlay 
        if (currentScene == "Gameplay")
        {
            gamePlay_Scene = true;
            moonGamePlay_Scene = false;
            volcanoGamePlay_Scene = false;
            marsGamePlay_Scene = false;
            heightGamePlay_Scene = false;
            fantasyGamePlay_Scene = false;
        }
        // For Moon GamePlay 
        if (currentScene == "MoonGameplay")
        {
            gamePlay_Scene = false;
            moonGamePlay_Scene = true;
            volcanoGamePlay_Scene = false;
            marsGamePlay_Scene = false;
            heightGamePlay_Scene = false;
            fantasyGamePlay_Scene = false;
        }
        // For Volcano GamePlay 
        if (currentScene == "volcano")
        {
            gamePlay_Scene = false;
            moonGamePlay_Scene = false;
            volcanoGamePlay_Scene = true;
            marsGamePlay_Scene = false;
            heightGamePlay_Scene = false;
            fantasyGamePlay_Scene = false;
        }
        // For Mars GamePlay 
        if (currentScene == "GameplayMars")
        {
            gamePlay_Scene = false;
            moonGamePlay_Scene = false;
            volcanoGamePlay_Scene = false;
            marsGamePlay_Scene = true;
            heightGamePlay_Scene = false;
            fantasyGamePlay_Scene = false;
        }
        // For Height map GamePlay 
        if (currentScene == "HeightMaps")
        {
            gamePlay_Scene = false;
            moonGamePlay_Scene = false;
            volcanoGamePlay_Scene = false;
            marsGamePlay_Scene = false;
            heightGamePlay_Scene = true;
            fantasyGamePlay_Scene = false;
        }
        // For Fantasy map GamePlay 
        if (currentScene == "FantasyScene")
        {
            gamePlay_Scene = false;
            moonGamePlay_Scene = false;
            volcanoGamePlay_Scene = false;
            marsGamePlay_Scene = false;
            heightGamePlay_Scene = false;
            fantasyGamePlay_Scene = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
       
        // For Earth GamePlay 
        if (gamePlay_Scene == true)
        {
            canvas.worldCamera = GameplayCar.INSTANCE.vehicleCamera.transform.GetComponent<Camera>();
        }
        // For Moon GamePlay 
        if (moonGamePlay_Scene == true)
        {
            //canvas.worldCamera = MoonGameplayCar.INSTANCE.vehicleCamera.transform.GetComponent<Camera>();
            canvas.worldCamera = AirCarControl.INSTANCE.vehicleCam;
        }
        // For Volcano GamePlay 
        if (volcanoGamePlay_Scene == true)
        {
            canvas.worldCamera = VolcanoGameplayCar.INSTANCE.vehicleCamera.transform.GetComponent<Camera>();
        }
        // For Mars GamePlay 
        if (marsGamePlay_Scene == true)
        {
            canvas.worldCamera = MarsGameplayCar.INSTANCE.vehicleCamera.transform.GetComponent<Camera>();
        }
        //For Height map GamePlay
        if (heightGamePlay_Scene == true)
        {
            canvas.worldCamera = HeightGameplayCar.INSTANCE.vehicleCamera.GetComponent<Camera>();
        }
        //For Fantasy map GamePlay
        if (fantasyGamePlay_Scene == true)
        {
            canvas.worldCamera = FantasyGameplayCar.INSTANCE.vehicleCamera.GetComponent<Camera>();
        }
    }

    private void LateUpdate()
    {
        if (AirCamTrigger.inAir == false)
        {
            canvas.worldCamera = AirCarControl.INSTANCE.vehicleCam;
        }
        else
        {
            canvas.worldCamera = AirCarControl.INSTANCE.inAirCam;
        }
    }
}

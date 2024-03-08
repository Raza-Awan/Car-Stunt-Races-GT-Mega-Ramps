using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard INSTANCE;

    public Text[] namesText;

    public string[] vehicleNames;
    public List<GameObject> finishedVehiclesList;

    public bool playerFinishedFirst;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (var text in namesText)
        {
            text.text = string.Empty;
            text.gameObject.SetActive(false);
        }

        playerFinishedFirst = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // For managing names and positions in leaderboard
        for (int i = 0; i < finishedVehiclesList.Count; i++)
        {
            vehicleNames[i] = finishedVehiclesList[i].name;

            if (vehicleNames[i] == AirCarControl.INSTANCE.objToRotate.name || finishedVehiclesList[i].tag == "Vehicle")
            {
                vehicleNames[i] = PlayerPrefs.GetString("PlayerName");

                if (vehicleNames[i].Length <= 0)
                {
                    vehicleNames[i] = "PLAYER";
                }
            }

            namesText[i].text = vehicleNames[i];
        }

        // Check if player has finished first, then level complete
        if (Ghost_Recorder.levelFinished == true && finishedVehiclesList.Count > 0)
        {
            if (finishedVehiclesList[0].name == AirCarControl.INSTANCE.objToRotate.name)
            {
                playerFinishedFirst = true;
            }
        }
    }
}

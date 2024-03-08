using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NWH.VehiclePhysics2;
using NWH.Common.Cameras;

public class MoonGameplayCar : MonoBehaviour
{
    public static MoonGameplayCar INSTANCE;

    public GameObject[] gameobject;
    public Transform [] arr;

    public GameObject mobCtrls;
    public GameObject airCtrls;

    [Header("Not To Change Parameters")]
    public Transform spawnpos;
    public GameObject playerVehicle;
    public VehicleController playerVehicleController;
    public CameraMouseDrag vehicleCamera;

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

        int i = GameManager.Instance.MoonLevelNo;
        int index = PlayerPrefs.GetInt("selectedcar");

        Debug.Log(index);
        playerVehicle = Instantiate(gameobject[index], arr[i].position, arr[i].rotation);

        playerVehicleController = playerVehicle.GetComponent<VehicleController>();
        vehicleCamera = playerVehicle.GetComponentInChildren<CameraMouseDrag>();
    }
}

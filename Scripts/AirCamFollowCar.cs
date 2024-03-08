using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCamFollowCar : MonoBehaviour
{
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    public float rotSpeed = 50f;

    GameObject playerVehicle;

    // Start is called before the first frame update
    void Start()
    {
        if (CanvasCamRef.moonGamePlay_Scene == true)
        {
            playerVehicle = MoonGameplayCar.INSTANCE.playerVehicle;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (AirCarControl.INSTANCE.grounded == false)
        {
            Vector3 targetPos = new Vector3(playerVehicle.transform.position.x + offsetX,
                                       playerVehicle.transform.position.y + offsetY,
                                       playerVehicle.transform.position.z + offsetZ);
            transform.position = targetPos;
            Quaternion targetRot = Quaternion.Euler(playerVehicle.transform.rotation.x, transform.rotation.y, transform.rotation.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            //transform.localRotation = Quaternion.identity;
        }
        
    }
}

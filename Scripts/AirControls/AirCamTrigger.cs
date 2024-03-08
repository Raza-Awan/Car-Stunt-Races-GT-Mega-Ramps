using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCamTrigger : MonoBehaviour
{
    public static bool inAir;
    public bool check;

    public Transform camPos;

    private void Start()
    {
        inAir = false;
    }

    private void Update()
    {
        check = inAir;
        if (inAir == true)
        {
            AirCarControl.INSTANCE.inAirCam.gameObject.SetActive(true);
            AirCarControl.INSTANCE.inAirCam.transform.position = camPos.position;
            AirCarControl.INSTANCE.vehicleCam.gameObject.SetActive(false);
            Time.timeScale = 0.5f;

            // For rotating air cam towards the vehicle 
            Vector3 relativePos = AirCarControl.INSTANCE.objToRotate.transform.position - AirCarControl.INSTANCE.inAirCam.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            AirCarControl.INSTANCE.inAirCam.transform.rotation = rotation;
        }
        else
        {
            AirCarControl.INSTANCE.inAirCam.gameObject.SetActive(false);
            AirCarControl.INSTANCE.vehicleCam.gameObject.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            inAir = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            inAir = false;
            AirCamTriggerManager.airCamTrigCount++;
            Invoke(nameof(OffTrigger), 1f);
        }
    }

    private void OffTrigger()
    {
        this.gameObject.SetActive(false);
    }
}

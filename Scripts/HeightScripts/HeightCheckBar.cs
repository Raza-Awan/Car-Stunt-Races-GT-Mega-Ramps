using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NWH.VehiclePhysics2;

public class HeightCheckBar : MonoBehaviour
{
    public float totalDis;
    public float disLeft;
    public float clampedDis;

    public Transform startPoint;
    public Transform finishPoint;
    public Image heightBarImage;

    public bool grounded;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        totalDis = Vector3.Distance(startPoint.position, finishPoint.position);
        disLeft = totalDis;
    }

    // Update is called once per frame
    void Update()
    {
        VehicleController playerVehicleController = HeightGameplayCar.INSTANCE.playerVehicleController;

        if (disLeft >= 1500f)
        {
            grounded = Physics.CheckSphere(playerVehicleController.transform.position, 3f, groundLayer);
        }

        disLeft = Vector3.Distance(playerVehicleController.transform.position, finishPoint.position);

        if (grounded == true)
        {
            disLeft = totalDis;
            clampedDis = 0f;
        }

        clampedDis = 1 - (disLeft / totalDis);
        clampedDis = Mathf.Clamp01(clampedDis);

        heightBarImage.fillAmount = clampedDis;

        if (clampedDis >= 0.94f)
        {
            clampedDis = 0.999f;
        }
    }
}

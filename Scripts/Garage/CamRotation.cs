using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    public Transform objToRotateAround;
    public float rotationSpeed;

    public bool allowManualRotation;
    public bool isRotating;
    private Vector3 lastTouchPosition;
    public float manualRotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(objToRotateAround.position, Vector3.up, rotationSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            float deltaX = Input.mousePosition.x - lastTouchPosition.x;
            deltaX /= 5;
            objToRotateAround.Rotate(0, deltaX, 0);
            lastTouchPosition = Input.mousePosition;
        }
        else
        {
            objToRotateAround.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }


        //if (allowManualRotation)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        isRotating = true;
        //        lastTouchPosition = Input.mousePosition;
        //    }
        //    else if (Input.GetMouseButtonUp(0))
        //    {
        //        isRotating = false;
        //    }

        //    if (isRotating)
        //    {
        //        float mouseX = (Input.mousePosition.x - lastTouchPosition.x) * 0.1f;
        //        mouseX = Mathf.Clamp(mouseX, 0f, float.MaxValue);
        //        transform.RotateAround(objToRotateAround.position, Vector3.up, mouseX * manualRotationSpeed * Time.deltaTime);
        //        lastTouchPosition = Input.mousePosition;
        //    }
        //}
    }
}

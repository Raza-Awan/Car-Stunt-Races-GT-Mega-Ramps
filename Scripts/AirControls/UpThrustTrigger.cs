using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpThrustTrigger : MonoBehaviour
{
    private Rigidbody rb;

    public float upThrustForce = 20f;

    private void Start()
    {
        rb = AirCarControl.INSTANCE.objToRotate.transform.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vehicle"))
        {
            rb.AddForce(transform.up * upThrustForce, ForceMode.VelocityChange);
        }
    }
}

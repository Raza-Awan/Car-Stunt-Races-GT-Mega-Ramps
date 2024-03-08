using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationAxis;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis * speed * Time.deltaTime);
    }
}

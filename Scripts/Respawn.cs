using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Respawn : MonoBehaviour
{
    public MyGhost RespawnAgent;
    public float groundCheckDistance;
    public bool grounded;
    public GameObject Car;
    public Rigidbody rb;
    public static Respawn Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        groundCheckDistance = 1.1f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance ))
        {
            grounded = true;
            RespawnAgent.isRecord = true;
            //Debug.Log(spvalue);
            //Debug.Log("HITTING GROUND");
        }
        else
        {
            RespawnAgent.isRecord = false;
            grounded = false;
        }
    }
    public void RespawnCar()
    {
        for(int i = 0; i<=50; i++)
        {
            RespawnAgent.timeStamp.RemoveAt(RespawnAgent.timeStamp.Count -1);
            RespawnAgent.position.RemoveAt(RespawnAgent.position.Count - 1);
            RespawnAgent.rotation.RemoveAt(RespawnAgent.rotation.Count - 1);
        }
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Car.transform.position = RespawnAgent.position.Last();
        Car.transform.rotation = RespawnAgent.rotation.Last();
    }
}

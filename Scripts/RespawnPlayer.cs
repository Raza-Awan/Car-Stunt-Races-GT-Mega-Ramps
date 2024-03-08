using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public static RespawnPlayer INSTANCE;

    public List<Vector3> positions;
    public List<Quaternion> rotations;

    public float storeDelay = 0.1f;
    float timer;

    Rigidbody rb;

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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(StorePlayerTransforms), 3f);
    }

    private void StorePlayerTransforms()
    {
        timer += Time.deltaTime;

        if (timer >= storeDelay && AirCarControl.INSTANCE.grounded == true && Ghost_Recorder.levelFinished == false)
        {
            timer = 0f;
            positions.Add(transform.position);
            rotations.Add(transform.rotation);
        }
    }

    public void Respawn_Player()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        for (int i = 0; i < 40; i++)
        {
            positions.RemoveAt(positions.Count - 1);
            rotations.RemoveAt(rotations.Count - 1);
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = positions[positions.Count - 1];
        transform.rotation = rotations[rotations.Count - 1];
    }
}

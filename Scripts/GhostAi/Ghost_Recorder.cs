using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Recorder : MonoBehaviour
{
    public Ghost_SO ghost_SO;

    public float delay;
    float timer;

    public static float speed;

    bool waitForRecord;
    public static bool levelFinished; //This bool is for checking if player has finished the level or not

    // Start is called before the first frame update
    void Start()
    {
        levelFinished = false;
        waitForRecord = false;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(StartRecord), 3f);

        if (ghost_SO.canRecord == true && waitForRecord == true && levelFinished == false)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                ghost_SO.positions.Add(transform.position);
                ghost_SO.rotations.Add(transform.rotation);
                timer = 0f;
            }
        }

        if (levelFinished == true && !LeaderBoard.INSTANCE.finishedVehiclesList.Contains(gameObject))
        {
            LeaderBoard.INSTANCE.finishedVehiclesList.Add(gameObject);
            speed = GetComponent<NWH.VehiclePhysics2.VehicleController>().Speed;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            foreach (var text in LeaderBoard.INSTANCE.namesText)
            {
                text.gameObject.SetActive(true);
            }
        }
    }

    private void StartRecord()
    {
        waitForRecord = true;
    }
}

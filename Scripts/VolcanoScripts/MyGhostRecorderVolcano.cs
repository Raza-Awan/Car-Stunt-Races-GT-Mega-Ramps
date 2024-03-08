using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//APPLY THIS SCRIPT ON THE PLAYER GAMEOBJECT WHICH THE USER IS CONTROLLING.

public class MyGhostRecorderVolcano : MonoBehaviour
{
    public MyGhost myghost;
    private float timer;
    private float timeValue;
    private float FastestTime = 0;
    VolcanoLapTime laptime;
    private void Awake()
    {
        laptime = FindObjectOfType<VolcanoLapTime>();

        if (myghost.isRecord)
        {
            myghost.ResetData();
            timeValue = 0;
            timer = 0;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        timeValue += Time.deltaTime;

        if(myghost.isRecord & timer >=1/myghost.recordFrequency)
        {
            if (laptime.startTimer == true)
            {
                myghost.timeStamp.Add(timeValue);
                myghost.position.Add(this.transform.position);
                myghost.rotation.Add(this.transform.rotation);
                timer = 0;
            }
           
        }
               
    }
}

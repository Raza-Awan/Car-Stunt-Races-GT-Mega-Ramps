
using UnityEngine;
using UnityEngine.UI;

public class VolcanoLapTime : MonoBehaviour
{
    public float laptimer;
    public float besttime;
    public bool startTimer = true;
    MyGhostRecorder recorderCheck;
    private bool checkpoint1 = false;
    public MyGhost ThirdParty;
    public MyGhost GhostPlayer;
    //public GameObject ghostObj;
    public Text Ltime;
    public Text Btime;
    public Text FLtime;
    public Text FBtime;
    public int levelno;
    RewardShow Bonus;
    CountdownTimer Strttime;
    [HideInInspector] public int BReward = 0;
    // Start is called before the first frame update
    void Start()
    {
        Bonus = FindObjectOfType<RewardShow>();
        recorderCheck = FindObjectOfType<MyGhostRecorder>();
        BReward = 0;
        Strttime = FindObjectOfType<CountdownTimer>();
        levelno = GameManager.Instance.VolcanoLevelNo;
        //if condition to disable ghost presence when no best time se present.
        if (PlayerPrefs.GetFloat("VolcanoBestTime" + levelno) == 0)
        {
            //ghostObj.SetActive(false);
        }

        Btime.text = "Best: " + PlayerPrefs.GetFloat("VolcanoBestTime" + levelno).ToString("F2");
        FBtime.text = PlayerPrefs.GetFloat("VolcanoBestTime" + levelno).ToString("F2");

        //Getting Ghost last saved ghost values on start
        GhostPlayer.position = PlayerPrefsExtra.GetList<Vector3>("VolcanoPosition"+ levelno);
        GhostPlayer.rotation = PlayerPrefsExtra.GetList<Quaternion>("VolcanoRotation"+ levelno);
        GhostPlayer.timeStamp = PlayerPrefsExtra.GetList<float>("Volcanotiem"+ levelno);
    }

    // Update is called once per frame
    void Update()
    {
        if (Strttime.TimeLeft == 0) //To start timer after countdown is over
        {
            if (startTimer == true)
            {
                //recorderCheck.checkbyLapTimes = true;
                laptimer = laptimer + Time.deltaTime;

                Ltime.text = "Current: " + laptimer.ToString("F2");
                Btime.text = "Best: " + PlayerPrefs.GetFloat("VolcanoBestTime" + levelno).ToString("F2");

            }
        }
    }
    public void GhostValue()
    {       
        GhostPlayer.position.Clear();  
        foreach (Vector3 pos in ThirdParty.position)
        {
            GhostPlayer.position.Add(pos);
            //PlayerPrefsExtra.SetList<Vector3>("Position" + levelno, GhostPlayer.position);
        }

        GhostPlayer.rotation.Clear();
        foreach (Quaternion rot in ThirdParty.rotation)
        {
            GhostPlayer.rotation.Add(rot);
            //PlayerPrefsExtra.SetList<Vector3>("Rotation" + levelno, GhostPlayer.rotation);
        }

        GhostPlayer.timeStamp.Clear();    
        foreach (float tiem in ThirdParty.timeStamp)
        {
            GhostPlayer.timeStamp.Add(tiem);
           // PlayerPrefsExtra.SetList<float>("tiem" + levelno, GhostPlayer.timeStamp);
        }
        VolcanoSaveData();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Vehicle")
        {
            startTimer = false;
            FLtime.text = laptimer.ToString("F2");
            if (PlayerPrefs.GetFloat("VolcanoBestTime"+ levelno) == 0)
            {
                Debug.Log("isfirsttime");
                besttime = laptimer;
                PlayerPrefs.SetFloat("VolcanoBestTime" + levelno, besttime);
                FBtime.text = PlayerPrefs.GetFloat("VolcanoBestTime" + levelno).ToString("F2");
                GhostValue();
            }
            if (laptimer < PlayerPrefs.GetFloat("VolcanoBestTime" + levelno))
            {
                //Bonus.BrewardCheck = true;
                BReward = 20;
                Debug.Log("issecond");
                besttime = laptimer;
                PlayerPrefs.SetFloat("VolcanoBestTime" + levelno, besttime);
                FBtime.text = PlayerPrefs.GetFloat("VolcanoBestTime" + levelno).ToString("F2");
                GhostValue();
            }

            //Btime.text = "Best: " + besttime.ToString("F2");
        }
    }
    public void VolcanoSaveData()
    {
        PlayerPrefsExtra.SetList<Vector3>("VolcanoPosition" + levelno, GhostPlayer.position);
        PlayerPrefsExtra.SetList<Quaternion>("VolcanoRotation" + levelno, GhostPlayer.rotation);
        PlayerPrefsExtra.SetList<float>("Volcanotiem" + levelno, GhostPlayer.timeStamp);
    }
}

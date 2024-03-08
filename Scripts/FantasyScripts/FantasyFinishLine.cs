using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

public class FantasyFinishLine : MonoBehaviour
{
    public GameObject finish; //MobInput;
    int count;
    public int FinCount;

    // Start is called before the first frame update
    void Start()
    {
        FinCount = GameManager.Instance.FantasyLevelNo;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vehicle" && Ghost_Recorder.levelFinished == false)
        {
            StartCoroutine(ExampleCoroutine());
            if (PlayerPrefs.GetInt("FinishCount") >= 2)
            {
                AdsManager.instance.ShowInterstitialAd();
                //AdsManager._INSTANCE.SHOW_INTERSTITIAL_AD();
                count = 0;
                PlayerPrefs.SetInt("FinishCount", count);
            }

            Ghost_Recorder.levelFinished = true;
        }

    }
    IEnumerator ExampleCoroutine()
    {
        if (count < 2)
        {
            count = PlayerPrefs.GetInt("FinishCount");
            count++;
            PlayerPrefs.SetInt("FinishCount", count);
        }
        FirebaseAnalytics.LogEvent("Level_Finish_Fantasy", "Level_Finish_Num", FinCount);
        yield return new WaitForSeconds(0.01f);
        AudioListener.pause = true;
        FantasyUIManager.Instance.ShowCompletePanel();
    }
}

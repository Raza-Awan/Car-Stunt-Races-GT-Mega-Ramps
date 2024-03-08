using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

public class HeightFinishLines : MonoBehaviour
{
    public GameObject finish; //MobInput;
    int count;
    public int FinCount; 
    
    // Start is called before the first frame update
    void Start()
    {
        FinCount = GameManager.Instance.HeightLevelNo;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vehicle")
        {     
            StartCoroutine(ExampleCoroutine());
            if (PlayerPrefs.GetInt("FinishCount") == 2)
            {
                //AdsManager._INSTANCE.SHOW_INTERSTITIAL_AD();
                count = 0;
                PlayerPrefs.SetInt("FinishCount", count);
            }
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
        FirebaseAnalytics.LogEvent("Level_Finish_Height", "Level_Finish_Num", FinCount);
        yield return new WaitForSeconds(0.01f);
        AudioListener.pause = true;
        HeightUIManager.Instance.ShowCompletePanel();
    }
    
}

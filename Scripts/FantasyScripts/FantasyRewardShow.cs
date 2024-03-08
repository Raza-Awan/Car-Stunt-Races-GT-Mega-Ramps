using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FantasyRewardShow : MonoBehaviour
{
    public Button addExpPointFirstTime_Btn;
    public Button addExpPointOnReplay_Btn;

    public Text RewardTxt;
    CheckPoints chkpnt;
    public Text Bonus;
    FantasyLapTime laptime;

    bool check;
    bool check2 = true;
    float cash;
    float addBonus = 0;

    public static FantasyRewardShow Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

        check = true;
        laptime = FindObjectOfType<FantasyLapTime>();
        //Mlaptime = FindObjectOfType<MarsLapTime>();
    }

    public void LevelFinishReward()
    {
        int reward = 50;
        RewardTxt.text = reward.ToString();

        Bonus.text = AirCarControl.INSTANCE.objectiveBonus.ToString();

        int totalReward = reward + AirCarControl.INSTANCE.objectiveBonus;

        CBS_CurrencyTest.INSTANCE.AddCurrency(totalReward);
        CBS_LeaderboardTest.INSTANCE.AddPointsToLeaderboard(30);
        CBS_LootboxTest.instance.GrantLootBox();
    }

    public void FinishedLevelReplayReward()
    {
        int reward = Random.Range(10, 20); ;
        RewardTxt.text = reward.ToString();

        Bonus.text = AirCarControl.INSTANCE.objectiveBonus.ToString();

        int totalReward = reward + AirCarControl.INSTANCE.objectiveBonus;

        CBS_CurrencyTest.INSTANCE.AddCurrency(totalReward);
        CBS_LeaderboardTest.INSTANCE.AddPointsToLeaderboard(10);
    }

    public void LevelFailedReward()
    {
        int reward = 0;

        RewardTxt.text = reward.ToString();
        Bonus.text = AirCarControl.INSTANCE.objectiveBonus.ToString();

        int totalReward = reward + AirCarControl.INSTANCE.objectiveBonus;

        CBS_CurrencyTest.INSTANCE.AddCurrency(totalReward);
    }

    public void GiveExpPoints_FirstTime()
    {
        addExpPointFirstTime_Btn.onClick.Invoke();
    }
    public void GiveExpPoints_OnReplay()
    {
        addExpPointOnReplay_Btn.onClick.Invoke();
    }

    public void OnClickDoubleReward()
    {
        if (check2 == true)
        {
            if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                //ADS_Manager.Instance.OnClickRewarded(Rewards.Coins);
                //AdsManager._INSTANCE.SHOW_INTERSTITIAL_AD();
            }
            else
            {
                Debug.Log("There is no internet connections...");
            }
        }
    }
    public void doubleReward()
    {
        float AdReward = chkpnt.Reward;
        AdReward += AdReward;
        chkpnt.Reward = AdReward;
        cash = PlayerPrefs.GetFloat("Wallet");
        cash += AdReward;
        PlayerPrefs.SetFloat("Wallet", cash);
        RewardTxt.text = "REWARD: " + AdReward.ToString("F0");
        check2 = false;
        StartCoroutine(adwait());
    }
    IEnumerator adwait()
    {
        yield return new WaitForSeconds(300f);
        check2 = true;
    }
}

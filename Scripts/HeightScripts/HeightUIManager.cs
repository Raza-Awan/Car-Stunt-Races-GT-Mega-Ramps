using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HeightUIManager : MonoBehaviour
{   
    public static HeightUIManager Instance;
    public GameObject PausePanel;
    public GameObject pauseBtn, lapbars;
    public GameObject[] lvlCompleteObjs;
    public GameObject[] lvlFailedObjs;
    public GameObject Level_Complete;
    public GameObject Level_Failed;
    public GameObject MobInput;
    private GameObject mobInputCanvas;
    public GameObject loading;
    public GameObject popupDrift;
    public GameObject jump, bigJump, hugeJump;
    public Animator jumpanim, bigjump, hugejump;
    public bool isCompleted;
    public Text backFlip_txt;
    public Text frontFlip_txt;
    public Text leftRoll_txt;
    public Text rightRoll_txt;
    public Text instructionText;
    public Text[] Stars;
    int levelIndex;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        isCompleted = false;
        
    }
    public void ShowPausePanel()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        PausePanel.gameObject.SetActive(true);
        MobInput.SetActive(false);
        pauseBtn.SetActive(false);
        lapbars.SetActive(false);
    }
    public void ShowCompletePanel()
    {
        Level_Complete.gameObject.SetActive(true);
        MobInput.SetActive(false);
        pauseBtn.SetActive(false);
        lapbars.SetActive(false);

        int levelNo = PlayerPrefs.GetInt("NextLevelHeight");

        if (LeaderBoard.INSTANCE.playerFinishedFirst == true)
        {
            foreach (var obj in lvlCompleteObjs)
            {
                obj.SetActive(true);
            }

            if (levelNo == GameManager.Instance.HeightLevelNo)
            {
                levelNo++;
                PlayerPrefs.SetInt("NextLevelHeight", levelNo);
                HeightRewardShow.Instance.LevelFinishReward();
                //LevelProgressHandler.INSTANCE.SetProgressValueOnFirstFinish();
                HeightRewardShow.Instance.GiveExpPoints_FirstTime();
            }
            else
            {
                HeightRewardShow.Instance.FinishedLevelReplayReward();
                //LevelProgressHandler.INSTANCE.SetProgressValueOnReplay();
                HeightRewardShow.Instance.GiveExpPoints_OnReplay();
            }

            isCompleted = true;
        }
        else
        {
            foreach (var obj in lvlFailedObjs)
            {
                obj.SetActive(true);
            }
            HeightRewardShow.Instance.LevelFailedReward();
        }
    }
    public void ShowLevelFailed()
    {
        Time.timeScale = 0;
        Level_Failed.gameObject.SetActive(true);
        MobInput.SetActive(false);
        pauseBtn.SetActive(false);
        lapbars.SetActive(false);
    }
    public void OnClickNextButton()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        Level_Complete.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        GameManager.Instance.HeightLevelNo += 1;
        Invoke("GamplayScene", 5f);
    }
    public void OnClickHome()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        Level_Complete.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        Invoke("MainMenuScene", 0f);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        Level_Complete.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        Invoke("GamplayScene", 3f);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        PausePanel.gameObject.SetActive(false);
        MobInput.SetActive(true);
        pauseBtn.SetActive(true);
        lapbars.SetActive(true);
    }
    public void Replay()
    {
        PausePanel.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        Invoke("GamplayScene", 0f);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void OnRespawnWatchBtn()
    {
        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            //ADS_Manager.Instance.OnClickRewarded(Rewards.Daimond);
            //AdsManager._INSTANCE.SHOW_REWARD_VIDEO_AD(t =>
            //{
            //    if (t)
            //    {
            //        if (MobInput != null && Level_Failed != null)
            //        {
            //            Debug.Log(MobInput);
            //            Debug.Log(Level_Failed);
            //            MobInput.SetActive(true);
            //            Level_Failed.SetActive(false);
            //            //Respawn.Instance.RespawnCar();
            //            mobInputCanvas.SetActive(true);
            //            RespawnPlayer.INSTANCE.Respawn_Player();
            //            AudioListener.pause = false;
            //        }
            //    }
            //    else
            //    {
            //        Debug.Log("Don't give reward");
            //    }
            //});
        }
        else
        {
            //InternetConnection.SetActive(true);
            Debug.Log("There is no internet connections...");
        }
    }

    public void MainMenuScene()
    {
        GameManager.Instance.Change_Scene("MainMenu");
    }
    public void GamplayScene()
    {
        GameManager.Instance.Change_Scene("HeightMaps");
    }
}

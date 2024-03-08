using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VolcanoUIManager : MonoBehaviour
{   
    public static VolcanoUIManager Instance;
    public GameObject PausePanel;
    public GameObject pauseBtn, lapbars, checkpointBG;
    public GameObject loseCanvas;
    public GameObject Level_Complete;
    public GameObject[] lvlCompleteObjs;
    public GameObject[] lvlFailedObjs;
    public GameObject Level_Failed;
    public GameObject MobInput;
    public GameObject mobInputCanvas;
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

        mobInputCanvas = MobInput.transform.parent.gameObject;
    }
    private void Start()
    {
        isCompleted = false;

        foreach (var obj in lvlCompleteObjs)
        {
            obj.SetActive(false);
        }
        foreach (var obj in lvlFailedObjs)
        {
            obj.SetActive(false);
        }

        //AdsManager.OnRespawnPlayer += RespawnAfterWatchingAd;
    }
    public void ShowPausePanel()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        PausePanel.gameObject.SetActive(true);
        MobInput.SetActive(false);
        pauseBtn.SetActive(false);
        lapbars.SetActive(false);
        checkpointBG.SetActive(false);
    }
    public void ShowCompletePanel()
    {
        //AdsManager._INSTANCE.SHOW_INTERSTITIAL_AD();

        Level_Complete.gameObject.SetActive(true);
        MobInput.SetActive(false);
        pauseBtn.SetActive(false);
        lapbars.SetActive(false);
        checkpointBG.SetActive(false);

        int levelNo = PlayerPrefs.GetInt("NextLevelVolcano");

        if (LeaderBoard.INSTANCE.playerFinishedFirst == true)
        {
            foreach (var obj in lvlCompleteObjs)
            {
                obj.SetActive(true);
            }

            if (levelNo == GameManager.Instance.VolcanoLevelNo)
            {
                levelNo++;
                PlayerPrefs.SetInt("NextLevelVolcano", levelNo);
                VolcanoRewardShow.Instance.LevelFinishReward();
                //LevelProgressHandler.INSTANCE.SetProgressValueOnFirstFinish();
                VolcanoRewardShow.Instance.GiveExpPoints_FirstTime();
            }
            else
            {
                VolcanoRewardShow.Instance.FinishedLevelReplayReward();
                //LevelProgressHandler.INSTANCE.SetProgressValueOnReplay();
                VolcanoRewardShow.Instance.GiveExpPoints_OnReplay();
            }

            isCompleted = true;
        }
        else
        {
            foreach (var obj in lvlFailedObjs)
            {
                obj.SetActive(true);
            }
            VolcanoRewardShow.Instance.LevelFailedReward();
        }
    }
    public void ShowLevelFailed()
    {
        Time.timeScale = 0;
        Level_Failed.gameObject.SetActive(true);
        MobInput.SetActive(false);
        pauseBtn.SetActive(false);
        lapbars.SetActive(false);
        checkpointBG.SetActive(false);
    }

    public void ShowLoseCanvas()
    {
        Time.timeScale = 0;
        loseCanvas.gameObject.SetActive(true);
        MobInput.SetActive(false);
        pauseBtn.SetActive(false);
        lapbars.SetActive(false);
        checkpointBG.SetActive(false);
        //Animator animator = loseCanvas.GetComponent<Animator>();
        //animator.Play("LoseCanvas_Anim");
    }

    public void OnClickNextButton()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        Level_Complete.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        GameManager.Instance.VolcanoLevelNo += 1;
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
        checkpointBG.SetActive(true);
    }
    public void Replay()
    {
        PausePanel.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        Invoke("GamplayScene",0f);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void OnRespawnWatchBtn()
    {
        //AdsManager.instance.respawn = true;
        AdsManager.instance.ShowRewardedAd();
    }

    public void RespawnAfterWatchingAd()
    {
        RespawnPlayer.INSTANCE.Respawn_Player();
        AudioListener.pause = false;
        Level_Failed.SetActive(false);
        mobInputCanvas.SetActive(true);
        mobInputCanvas.SetActive(true);
        //AdsManager.instance.respawn = false;
    }

    public void MainMenuScene()
    {
        GameManager.Instance.Change_Scene("MainMenu");
    }
    public void GamplayScene()
    {
        GameManager.Instance.Change_Scene("volcano");
    }
}

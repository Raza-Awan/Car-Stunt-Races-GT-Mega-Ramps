using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerProfileManager : MonoBehaviour
{
    public GameObject mainProfileBg;
    public GameObject infoBg;
    public GameObject changeAvatarsBg;

    // Start is called before the first frame update
    void Start()
    {
        mainProfileBg.SetActive(false);
        infoBg.SetActive(false);
        changeAvatarsBg.SetActive(false);
    }

    public void OnClickProfile()
    {
        mainProfileBg.SetActive(true);
        infoBg.SetActive(true);
        changeAvatarsBg.SetActive(false);
    }
}

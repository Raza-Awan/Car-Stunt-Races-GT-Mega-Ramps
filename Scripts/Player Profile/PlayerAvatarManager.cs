using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatarManager : MonoBehaviour
{
    public Image profileAvatar;     //Profile Avatar image ref
    public Image infoMainAvatar;    //Main Avatars ref from info bg
    public Image mainAvatar;        //Main Avatars ref from change avatar bg
    public Image[] avatars;
    private Button[] buttons;

    public Sprite[] avatarsSprites;

    public int currentAvatarIndex;

    private void Awake()
    {
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].sprite = avatarsSprites[i];
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("AvatarIndex"))
        {
            currentAvatarIndex = PlayerPrefs.GetInt("AvatarIndex");
        }
        else
        {
            currentAvatarIndex = 0;
        }

        //Setting player avatar image
        profileAvatar.sprite = avatarsSprites[currentAvatarIndex];
        infoMainAvatar.sprite = avatarsSprites[currentAvatarIndex];
        mainAvatar.sprite = avatarsSprites[currentAvatarIndex];

        // initializing array length
        buttons = new Button[avatars.Length];

        for (int i = 0; i < avatars.Length; i++)
        {
            buttons[i] = avatars[i].GetComponentInChildren<Button>();
        }

        // Attach click event handlers to each button
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Store the index in a local variable to avoid closure issues
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    public void OnButtonClick(int index)
    {
        currentAvatarIndex = index;

        //Setting player avatar image
        profileAvatar.sprite = avatarsSprites[currentAvatarIndex];
        infoMainAvatar.sprite = avatarsSprites[currentAvatarIndex];
        mainAvatar.sprite = avatarsSprites[currentAvatarIndex];

        PlayerPrefs.SetInt("AvatarIndex", currentAvatarIndex);
    }
}

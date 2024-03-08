using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    public Text playerDisplayName;
    //public InputField playerNameInput;
    private string playerName;

    private void Update()
    {
        playerName = playerDisplayName.text;
        PlayerPrefs.SetString("PlayerName", playerName);
    }

    //public void Start()
    //{
    //    if (PlayerPrefs.HasKey("PlayerName"))
    //    {
    //        playerName = PlayerPrefs.GetString("PlayerName");
    //        playerNameInput.text = playerName;
    //    }
    //    else
    //    {
    //        playerName = "PLAYER";
    //        playerNameInput.text = playerName;
    //        PlayerPrefs.SetString("PlayerName", playerName);
    //    }

    //    // Subscribe to the EndEdit event
    //    if (playerNameInput != null)
    //    {
    //        playerNameInput.onEndEdit.AddListener(OnEndEdit);
    //    }
    //}

    //void OnEndEdit(string input)
    //{
    //    // Check if the input is empty
    //    if (string.IsNullOrEmpty(input))
    //    {
    //        // If empty, set it to "PLAYER"
    //        playerNameInput.text = "PLAYER";
    //        Debug.Log("has entered!!");
    //    }

    //    // Save the player name to PlayerPrefs
    //    PlayerPrefs.SetString("PlayerName", playerNameInput.text);
    //    PlayerPrefs.Save();

    //    // Update the playerName variable
    //    playerName = playerNameInput.text;
    //}
}

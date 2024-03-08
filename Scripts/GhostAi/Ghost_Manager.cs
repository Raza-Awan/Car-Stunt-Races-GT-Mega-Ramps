using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Manager : MonoBehaviour
{
    public static Ghost_Manager INSTANCE;

    public GhostLevelManager[] ghostsInLevel;

    public GameObject[] ghosts;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Dynamically filling out the GhostLevelManager array data.
        // Note: Make sure arrays size are declared in editor before addition of any new ghost

        for (int i = 0; i < ghostsInLevel.Length; i++) // outer loop will run according to number of levels in each scene
        {
            for (int j = 0; j < 3; j++) // innner loop will run according to number of ghosts we want in our level
            {
                ghostsInLevel[i].ghosts[j] = ghosts[i].transform.GetChild(j).gameObject;
                ghostsInLevel[i].ghosts_SO[j] = ghosts[i].transform.GetChild(j).transform.GetComponent<Ghost_AI>().ghost_SO;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var ghost in ghosts)
        {
            ghost.SetActive(false);
        }

        // EARTH GAMEPLAY
        if (CanvasCamRef.gamePlay_Scene == true)
        {
            ghosts[GameManager.Instance.LevelNo].SetActive(true);

            for (int i = 0; i < 3; i++)
            {
                ghostsInLevel[GameManager.Instance.LevelNo].ghosts[i].transform.position = ghostsInLevel[GameManager.Instance.LevelNo].ghosts_SO[i].positions[0];
                ghostsInLevel[GameManager.Instance.LevelNo].ghosts[i].transform.rotation = ghostsInLevel[GameManager.Instance.LevelNo].ghosts_SO[i].rotations[0];
            }
        }
        // EARTH MARS GAMEPLAY
        if (CanvasCamRef.marsGamePlay_Scene == true)
        {
            ghosts[GameManager.Instance.MarsLevelNo].SetActive(true);

            for (int i = 0; i < 3; i++)
            {
                ghostsInLevel[GameManager.Instance.MarsLevelNo].ghosts[i].transform.position = ghostsInLevel[GameManager.Instance.MarsLevelNo].ghosts_SO[i].positions[0];
                ghostsInLevel[GameManager.Instance.MarsLevelNo].ghosts[i].transform.rotation = ghostsInLevel[GameManager.Instance.MarsLevelNo].ghosts_SO[i].rotations[0];
            }
        }
        // EARTH MOON GAMEPLAY
        if (CanvasCamRef.moonGamePlay_Scene == true)
        {
            ghosts[GameManager.Instance.MoonLevelNo].SetActive(true);

            for (int i = 0; i < 3; i++)
            {
                ghostsInLevel[GameManager.Instance.MoonLevelNo].ghosts[i].transform.position = ghostsInLevel[GameManager.Instance.MoonLevelNo].ghosts_SO[i].positions[0];
                ghostsInLevel[GameManager.Instance.MoonLevelNo].ghosts[i].transform.rotation = ghostsInLevel[GameManager.Instance.MoonLevelNo].ghosts_SO[i].rotations[0];
            }
        }
        // EARTH VOLCANO GAMEPLAY
        if (CanvasCamRef.volcanoGamePlay_Scene == true)
        {
            ghosts[GameManager.Instance.VolcanoLevelNo].SetActive(true);

            for (int i = 0; i < 3; i++)
            {
                ghostsInLevel[GameManager.Instance.VolcanoLevelNo].ghosts[i].transform.position = ghostsInLevel[GameManager.Instance.VolcanoLevelNo].ghosts_SO[i].positions[0];
                ghostsInLevel[GameManager.Instance.VolcanoLevelNo].ghosts[i].transform.rotation = ghostsInLevel[GameManager.Instance.VolcanoLevelNo].ghosts_SO[i].rotations[0];
            }
        }
        // EARTH FANTASY GAMEPLAY
        if (CanvasCamRef.fantasyGamePlay_Scene == true)
        {
            ghosts[GameManager.Instance.FantasyLevelNo].SetActive(true);

            for (int i = 0; i < 3; i++)
            {
                ghostsInLevel[GameManager.Instance.FantasyLevelNo].ghosts[i].transform.position = ghostsInLevel[GameManager.Instance.FantasyLevelNo].ghosts_SO[i].positions[0];
                ghostsInLevel[GameManager.Instance.FantasyLevelNo].ghosts[i].transform.rotation = ghostsInLevel[GameManager.Instance.FantasyLevelNo].ghosts_SO[i].rotations[0];
            }
        }
        //if (CanvasCamRef.heightGamePlay_Scene == true)
        //{
        //    ghosts[GameManager.Instance.HeightLevelNo].SetActive(true);
        //}
    }
}

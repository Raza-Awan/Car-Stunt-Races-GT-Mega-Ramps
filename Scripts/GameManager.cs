using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int LevelNo;
    public int MarsLevelNo;
    public int VolcanoLevelNo;
    public int HeightLevelNo;
    public int MoonLevelNo;
    public int FantasyLevelNo;
    public int VehicleIndex;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Change_Scene(string name)
    {
        SceneManager.LoadScene(name);
    }
}

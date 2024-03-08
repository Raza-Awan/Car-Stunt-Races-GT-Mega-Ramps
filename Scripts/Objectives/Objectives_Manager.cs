using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives_Manager : MonoBehaviour
{
    // OBJECTIVES S0 REFERENCES 
    public Objectives_SO[] earthObjectives_SO;
    public Objectives_SO[] marsObjectives_SO;
    public Objectives_SO[] volcanoObjectives_SO;
    public Objectives_SO[] moonObjectives_SO;
    public Objectives_SO[] fantasyObjectives_SO;
    public Objectives_SO[] heightObjectives_SO;


    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // Singleton Instance
    public static Objectives_Manager INSTANCE;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        InitializeEarthObjsPrefs();
        InitializeMarsObjsPrefs();
        InitializeVolcanoObjsPrefs();
        InitializeMoonObjsPrefs();
        InitializeFantasyObjsPrefs();
        InitializeHeightObjsPrefs();
    }

    public void InitializeEarthObjsPrefs()
    {

        for (int i = 0; i < earthObjectives_SO.Length; i++)
        {
            earthObjectives_SO[i].objsString = PlayerPrefs.GetString("EarthObjs" + i);
        }
    }

    public void InitializeMarsObjsPrefs()
    {
        for (int i = 0; i < marsObjectives_SO.Length; i++)
        {
            marsObjectives_SO[i].objsString = PlayerPrefs.GetString("MarsObjs" + i);
        }
    }

    public void InitializeVolcanoObjsPrefs()
    {
        for (int i = 0; i < volcanoObjectives_SO.Length; i++)
        {
            volcanoObjectives_SO[i].objsString = PlayerPrefs.GetString("VolcanoObjs" + i);
        }
    }

    public void InitializeMoonObjsPrefs()
    {
        for (int i = 0; i < moonObjectives_SO.Length; i++)
        {
            moonObjectives_SO[i].objsString = PlayerPrefs.GetString("MoonObjs" + i);
        }
    }

    public void InitializeFantasyObjsPrefs()
    {
        for (int i = 0; i < fantasyObjectives_SO.Length; i++)
        {
            fantasyObjectives_SO[i].objsString = PlayerPrefs.GetString("FantasyObjs" + i);
        }
    }

    public void InitializeHeightObjsPrefs()
    {
        for (int i = 0; i < heightObjectives_SO.Length; i++)
        {
            heightObjectives_SO[i].objsString = PlayerPrefs.GetString("HeightObjs" + i);
        }
    }

    public bool UpdateEarthObjectives(string objsString, int objNumber)
    {
        bool match = false;

        foreach (char digitChar in objsString)
        {
            int digit = int.Parse(digitChar.ToString());

            if (digit == objNumber)
            {
                match = true;
                break;
            }
        }

        return match;
    }

    public bool UpdateMarsObjectives(string objsString, int objNumber)
    {
        bool match = false;

        foreach (char digitChar in objsString)
        {
            int digit = int.Parse(digitChar.ToString());

            if (digit == objNumber)
            {
                match = true;
                break;
            }
        }

        return match;
    }

    public bool UpdateVolcanoObjectives(string objsString, int objNumber)
    {
        bool match = false;

        foreach (char digitChar in objsString)
        {
            int digit = int.Parse(digitChar.ToString());

            if (digit == objNumber)
            {
                match = true;
                break;
            }
        }

        return match;
    }

    public bool UpdateMoonObjectives(string objsString, int objNumber)
    {
        bool match = false;

        foreach (char digitChar in objsString)
        {
            int digit = int.Parse(digitChar.ToString());

            if (digit == objNumber)
            {
                match = true;
                break;
            }
        }

        return match;
    }

    public bool UpdateFantasyObjectives(string objsString, int objNumber)
    {
        bool match = false;

        foreach (char digitChar in objsString)
        {
            int digit = int.Parse(digitChar.ToString());

            if (digit == objNumber)
            {
                match = true;
                break;
            }
        }

        return match;
    }

    public bool UpdateHeightObjectives(string objsString, int objNumber)
    {
        bool match = false;

        foreach (char digitChar in objsString)
        {
            int digit = int.Parse(digitChar.ToString());

            if (digit == objNumber)
            {
                match = true;
                break;
            }
        }

        return match;
    }
}

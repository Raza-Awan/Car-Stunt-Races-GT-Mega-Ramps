using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NOS 
{
    public int LevelNo;
    public float Timer;
    public int Coins;
    public int Daimonds;
    public float NosTimer;
    public float Acceleration;
    public string Upgraded;
    public NOS(int levelNo)
    {
        LevelNo = levelNo;
    }

}

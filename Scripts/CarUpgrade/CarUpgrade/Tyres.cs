using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tyres 
{
    public int LevelNo;
    public float Timer;
    public int Coins;
    public int Daimonds;
    public float slipvalue;
    public float gripvalue;
    public float springlength;
    public float Handling;
    public string Upgraded;
    public Tyres(int levelNo)
    {
        LevelNo = levelNo;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Objectives_SO")]

public class Objectives_SO : ScriptableObject
{
    public string objsString;

    public int[] objNumbers;
    public bool[] objectiveStatus;
    public string[] missionStatement;
}

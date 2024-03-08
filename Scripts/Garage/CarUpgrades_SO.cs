using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSO_Car", menuName = "ScriptableObjects/Upgrade_SO")]
public class CarUpgrades_SO : ScriptableObject
{
    [HideInInspector] public int carPower;
    [HideInInspector] public int boxesToFill;

    public float[] fillAmountsFront;
    [HideInInspector] public float[] fillAmountsBack;

    public int[] TS_CurrentVal;                     //top speed
    [HideInInspector] public int[] TS_NewVal;
    public float[] XL_CurrentVal;                   //acceleration
    [HideInInspector] public float[] XL_NewVal;
    public int[] PW_CurrentVal;                     //power
    [HideInInspector] public int[] PW_NewVal;
    public int[] TQ_CurrentVal;                     //handling
    [HideInInspector] public int[] TQ_NewVal;

    [Space]
    [Header("[ENGINE]")]
    public string[] engName;
    public string[] engType;
    public bool[] applyEngUpg;
    public int[] maxPowerVal;       //This array will hold values of upgrades for car prefabs max power values Under Power and torque section 

    [Header("[TIRES]")]
    public string[] tireName;
    public string[] tireType;
    public bool[] applyTireUpg;
    public float[] maxSpringLengths;    //This array will hold values of upgrades for car wheel prefabs max spring length values 
    public float[] inertias;            //This array will hold values of upgrades for car prefabs inertia va;ues

    [Header("[SUSPENSION]")]
    public string[] SpName;
    public string[] SpType;
    public bool[] applySpUpg;
    public int[] maxBrakeTorques;   //This array will hold values of upgrades for car prefabs max break torque values

    [Header("[DIVIDER 2 REF'S]")]
    [HideInInspector] public float div2_Amount1;
    [HideInInspector] public float div2_Amount2;
    [HideInInspector] public float div2_Amount3;
}

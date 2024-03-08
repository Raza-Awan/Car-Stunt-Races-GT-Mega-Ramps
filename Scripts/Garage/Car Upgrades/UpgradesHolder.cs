using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NWH.VehiclePhysics2;

//This script will hold references for upgrades

public class UpgradesHolder : MonoBehaviour
{
    public VehicleController[] carsPrefabs; // make sure that sequence is right according to garageCarSelection's cars array.

    #region Engine Upgrade Parameters
    [Header("Engine Upgrade Parameters:")]
    public GameObject[] engUpgHolder;       // this is ref for each car engine upg ui parents, under which there buttons, text and lockImages are 

    public EngineUpgradesUI[] engineUpgradesUI; // store ref of each car engine upgrade UI here i.e; buttons, text & lock images.

    public EngineUpgradesStats[] engineUpgradesStats; // this stores data for each engine upg like how much car
                                                      // stats will be increased after applying this upg.Its size will be 3 as there 
                                                      // are max 3 upgrades available for each car engine.
    #endregion

    #region Suspension Upgrade Parameters
    [Header("Suspension Upgrade Parameters:")]
    public GameObject[] SPUpgHolder;

    public SuspensionUpgUI[] SPUpgUI;

    public SuspensionUpgStats[] SPUpgStats;
    #endregion

    #region Tire Brakes Upgrade Parameters
    [Header("Tire Brakes Upgrade Parameters:")]
    public GameObject[] TBUpgHolder;

    public TiresBrakesUpgUI[] TBUpgUI;

    public TiresBrakesUpgStats[] TBUpgStats;
    #endregion
}

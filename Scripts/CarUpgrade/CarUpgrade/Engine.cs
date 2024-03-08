using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NWH.Common.SceneManagement;
using NWH.NPhysics;
using NWH.VehiclePhysics2.Effects;
using NWH.VehiclePhysics2.Input;
using NWH.VehiclePhysics2.Modules;
using NWH.VehiclePhysics2.Powertrain;
using NWH.VehiclePhysics2.Powertrain.Wheel;
using NWH.VehiclePhysics2.Sound;
using NWH.WheelController3D;
using NWH.VehiclePhysics2.Sound.SoundComponents;

[System.Serializable]
public class Engine 
{
    public int LevelNo;
    public float Timer;
    public int Coins;
    public int Daimonds;
    public float TopSpeed;
    public float Acceleration;
    public string Upgraded;
    public Engine(int levelNo)
    {
        LevelNo = levelNo;
    }
}


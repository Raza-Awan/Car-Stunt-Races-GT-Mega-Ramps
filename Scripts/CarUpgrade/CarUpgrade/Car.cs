using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


namespace NWH.VehiclePhysics2
{
    [System.Serializable]
    public class Car
    {
        public VehicleController NWH_Car;
        public WheelController[] NWH_Wheels;
        public int carNumber;
        public int EngCount, SuspCount, TBCount;

        public Image[] EngImg;
        public Image[] SuspImg;
        public Image[] TBImg;
        public Button[] EngBtn;
        public Button[] SuspBtn;
        public Button[] TBBtn;
        public List<Suspension> suspension = new List<Suspension>();
        public List<Engine> engine = new List<Engine>();
        //public List<NOS> nos = new List<NOS>();
        public List<Tyres> tyre = new List<Tyres>();

        //Tiers tier1 = new Tiers(7);
        //Tiers tier2 = new Tiers(7);
        //Tiers tier3 = new Tiers(7);

        //private void Start()
        //{
        //    if (RCC_DemoVehicles.Instance.vehicles != null)
        //    {
        //        for(int i=0; i<tier1.Cars.Length; i++)
        //        {
        //            tier1.Cars[i] = RCC_DemoVehicles.Instance.vehicles[i];
        //        }
        //        for(int i=0; i<tier2.Cars.Length; i++)
        //        {
        //           tier2.Cars[i] = RCC_DemoVehicles.Instance.vehicles[i+7];
        //        }
        //        for(int i=0; i<tier3.Cars.Length; i++)
        //        {
        //           tier3.Cars[i] = RCC_DemoVehicles.Instance.vehicles[i+14];
        //        }  
        //    }
        //    settingBaseValuesofTier(tier1, 70, 25, 500, 250, 1);
        //    settingBaseValuesofTier(tier2, 120, 32, 1200, 550, 1.2f);
        //    settingBaseValuesofTier(tier3, 180, 40, 1800, 1000, 2);       
        //}
        //void settingBaseValuesofTier(Tiers tier, float speed, float sangle, float brake, float acceleration, float drftPower)
        //{
        //    for(int i=0; i< tier.Cars.Length; i++)
        //    {
        //        tier.Cars[i].maxspeed = speed + (i*10);
        //        tier.Cars[i].steerAngle = sangle + (i*2);
        //        tier.Cars[i].brakeTorque = brake + (i*200);
        //        tier.Cars[i].maxEngineTorque = acceleration + (i * 70);
        //        tier.Cars[i].DriftPower = drftPower;
        //    }
        //}
    }
}

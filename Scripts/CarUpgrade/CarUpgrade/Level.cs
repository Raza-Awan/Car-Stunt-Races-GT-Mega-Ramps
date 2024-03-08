using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
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
    public class Level : MonoBehaviour
    {
        CarSelection carselection;
        int firsttimecount = 0;
        int activeCarIndex;
        VehicleController ActiveCar;
        //WheelController[] ActiveWheels;
        float playerCash;
        int playerDaimonds;
        int indexcount;
        public string upgradingPart;
        //int EngCount, SuspCount, TBCount;

        [Header("Car Count integers for upgrades")]
        int BacEngCount = 0, BacSuspCount = 0, BacTBCount = 0;
        int BatEngCount = 0, BatSuspCount = 0, BatTBCount = 0;
        int SCD2EngCount = 0, SCD2SuspCount = 0, SCD2TBCount = 0;
        int RampEngCount = 0, RampSuspCount = 0, RampTBCount = 0;
        int SDCF1EngCount = 0, SDCF1SuspCount = 0, SDCF1TBCount = 0;
        int F1ConceptEngCount = 0, F1ConceptSuspCount = 0, F1ConceptTBCount = 0;


        [Header("UI CarSpecs")]
        public Image[] lockimg;
        public Button[] EngBtn;
        public Button[] SuspBtn;
        public Button[] TB;

        public Image Acceleration;
        public Image TopSpeed;
        public Image Break;
        //public Image NOS;
        public Image AddingAcceleration;
        public Image AddingTopSpeed;
        public Image AddingBreak;
        //public Image AddingNOS;
        public Text TimeLeftBanner;

        [Header("Car Stats")]
        public Image AccelerationStat;
        public Image TopspeedStat;
        public Image HandlingStat;

        [Header("UI PNGs")]
        public GameObject midPng;
        public GameObject nextPng;
        public GameObject previousPng;
        public Sprite[] partModels;

        [Header("UI Text")]
        public Text notEnoughCashText;
        public Text purchasedText;
        public Text timeToUpgrade;
        public Text prize;
        public Button upgradeButton;

        [Header("Tiers")]
        public Tiers tier1;
        //public Tiers tier2;
        //public Tiers tier3;
        Tiers tier;

        //[Header("Parts List")]
        //public List<Suspension> suspension = new List<Suspension>();
        //public List<Engine> engine = new List<Engine>();
        //public List<NOS> nos = new List<NOS>();
        //public List<Tyres> tyre = new List<Tyres>();

        public void Awake()
        {
            //playerCash = PlayerPrefs.GetInt(Login.instance.playerName + "SavedCash", 0);
            //playerDaimonds = PlayerPrefs.GetInt(Login.instance.playerName + "SavedDaimonds", 0);
            tier = GetActiveCar();
            PlayerPrefs.SetString("PartUpgrading", "false");
            playerCash = PlayerPrefs.GetFloat("Wallet");

        }
        public void OnEnable()
        {

            //PlayerPrefs.SetString("PartUpgrading", "false");
            //carselection = FindObjectOfType<CarSelection>();
            //tier = GetActiveCar();
            //playerCash = PlayerPrefs.GetInt(Login.instance.playerName + "SavedCash", 0);
            //playerDaimonds = PlayerPrefs.GetInt(Login.instance.playerName + "SavedDaimonds", 0);
            if (timeToUpgrade != null) timeToUpgrade.text = tier.car[activeCarIndex].suspension[indexcount].Timer.ToString();
            if (prize != null) prize.text = tier.car[activeCarIndex].suspension[indexcount].Coins.ToString() + "$";
            string displayName = PlayerPrefs.GetString("UpgradingPart");
            if (LevelCheck(displayName) == true)
            {
                SpecsValuesForDisplay(displayName);
                upgradeButton.gameObject.SetActive(false);
            }
            else
            {
                settingSpecsValuesForDisplay(displayName);
                //upgradeButton.gameObject.SetActive(true);
            }

            if (PlayerPrefs.GetInt("LevelScriptFirstTime2") < 1)
            {
                //BacMono Upgrades Buttons Initialization
                PlayerPrefsExtra.SetBool("BacMonoEngBtn", tier.car[0].EngBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("BacMonoEngImg", tier.car[0].EngImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("BacMonoEngBtn1", tier.car[0].EngBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("BacMonoEngImg1", tier.car[0].EngImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("BacMonoEngBtn2", tier.car[0].EngBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("BacMonoEngImg2", tier.car[0].EngImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("BacMonoSuspBtn", tier.car[0].SuspBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("BacMonoSuspImg", tier.car[0].SuspImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("BacMonoSuspBtn1", tier.car[0].SuspBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("BacMonoSuspImg1", tier.car[0].SuspImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("BacMonoSuspBtn2", tier.car[0].SuspBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("BacMonoSuspImg2", tier.car[0].SuspImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("BacMonoTBBtn", tier.car[0].TBBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("BacMonoTBImg", tier.car[0].TBImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("BacMonoTBBtn1", tier.car[0].TBBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("BacMonoTBImg1", tier.car[0].TBImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("BacMonoTBBtn2", tier.car[0].TBBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("BacMonoTBImg2", tier.car[0].TBImg[2].enabled = true);

                PlayerPrefs.SetFloat("AccStatBacMono", 0.3f);
                PlayerPrefs.SetFloat("TopSpeedStatBacMono", 0.3f);
                PlayerPrefs.SetFloat("HandlingStatBacMono", 0.4f);

                //BatMobile Upgrade Buttons Initialization
                PlayerPrefsExtra.SetBool("BatMobileEngBtn", tier.car[1].EngBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("BatMobileEngImg", tier.car[1].EngImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("BatMobileEngBtn1", tier.car[1].EngBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("BatMobileEngImg1", tier.car[1].EngImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("BatMobileEngBtn2", tier.car[1].EngBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("BatMobileEngImg2", tier.car[1].EngImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("BatMobileSuspBtn", tier.car[1].SuspBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("BatMobileSuspImg", tier.car[1].SuspImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("BatMobileSuspBtn1", tier.car[1].SuspBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("BatMobileSuspImg1", tier.car[1].SuspImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("BatMobileSuspBtn2", tier.car[1].SuspBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("BatMobileSuspImg2", tier.car[1].SuspImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("BatMobileTBBtn", tier.car[1].TBBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("BatMobileTBImg", tier.car[1].TBImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("BatMobileTBBtn1", tier.car[1].TBBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("BatMobileTBImg1", tier.car[1].TBImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("BatMobileTBBtn2", tier.car[1].TBBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("BatMobileTBImg2", tier.car[1].TBImg[2].enabled = true);

                PlayerPrefs.SetFloat("AccStatBatMobile", 0.52f);
                PlayerPrefs.SetFloat("TopSpeedStatBatMobile", 0.4f);
                PlayerPrefs.SetFloat("HandlingStatBatMobile", 0.25f);

                //SCD2
                PlayerPrefsExtra.SetBool("SCD2EngBtn", tier.car[2].EngBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("SCD2EngImg", tier.car[2].EngImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("SCD2EngBtn1", tier.car[2].EngBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("SCD2EngImg1", tier.car[2].EngImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("SCD2EngBtn2", tier.car[2].EngBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("SCD2EngImg2", tier.car[2].EngImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("SCD2SuspBtn", tier.car[2].SuspBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("SCD2SuspImg", tier.car[2].SuspImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("SCD2SuspBtn1", tier.car[2].SuspBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("SCD2SuspImg1", tier.car[2].SuspImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("SCD2SuspBtn2", tier.car[2].SuspBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("SCD2SuspImg2", tier.car[2].SuspImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("SCD2TBBtn", tier.car[2].TBBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("SCD2TBImg", tier.car[2].TBImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("SCD2TBBtn1", tier.car[2].TBBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("SCD2TBImg1", tier.car[2].TBImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("SCD2TBBtn2", tier.car[2].TBBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("SCD2TBImg2", tier.car[2].TBImg[2].enabled = true);

                PlayerPrefs.SetFloat("AccStatSCD2", 0.35f);
                PlayerPrefs.SetFloat("TopSpeedStatSCD2", 0.5f);
                PlayerPrefs.SetFloat("HandlingStatSCD2", 0.45f);

                //Ramp Car N1ew
                PlayerPrefsExtra.SetBool("RampEngBtn", tier.car[3].EngBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("RampEngImg", tier.car[3].EngImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("RampEngBtn1", tier.car[3].EngBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("RampEngImg1", tier.car[3].EngImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("RampEngBtn2", tier.car[3].EngBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("RampEngImg2", tier.car[3].EngImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("RampSuspBtn", tier.car[3].SuspBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("RampSuspImg", tier.car[3].SuspImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("RampSuspBtn1", tier.car[3].SuspBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("RampSuspImg1", tier.car[3].SuspImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("RampSuspBtn2", tier.car[3].SuspBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("RampSuspImg2", tier.car[3].SuspImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("RampTBBtn", tier.car[3].TBBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("RampTBImg", tier.car[3].TBImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("RampTBBtn1", tier.car[3].TBBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("RampTBImg1", tier.car[3].TBImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("RampTBBtn2", tier.car[3].TBBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("RampTBImg2", tier.car[3].TBImg[2].enabled = true);

                PlayerPrefs.SetFloat("AccStatRamp", 0.6f);
                PlayerPrefs.SetFloat("TopSpeedStatRamp", 0.5f);
                PlayerPrefs.SetFloat("HandlingStatRamp", 0.75f);

                //SDCF1
                PlayerPrefsExtra.SetBool("SDCF1EngBtn", tier.car[4].EngBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("SDCF1EngImg", tier.car[4].EngImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("SDCF1EngBtn1", tier.car[4].EngBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("SDCF1EngImg1", tier.car[4].EngImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("SDCF1EngBtn2", tier.car[4].EngBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("SDCF1EngImg2", tier.car[4].EngImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("SDCF1SuspBtn", tier.car[4].SuspBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("SDCF1SuspImg", tier.car[4].SuspImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("SDCF1SuspBtn1", tier.car[4].SuspBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("SDCF1SuspImg1", tier.car[4].SuspImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("SDCF1SuspBtn2", tier.car[4].SuspBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("SDCF1SuspImg2", tier.car[4].SuspImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("SDCF1TBBtn", tier.car[4].TBBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("SDCF1TBImg", tier.car[4].TBImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("SDCF1TBBtn1", tier.car[4].TBBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("SDCF1TBImg1", tier.car[4].TBImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("SDCF1TBBtn2", tier.car[4].TBBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("SDCF1TBImg2", tier.car[4].TBImg[2].enabled = true);

                PlayerPrefs.SetFloat("AccStatSDCF1", 0.7f);
                PlayerPrefs.SetFloat("TopSpeedStatSDCF1", 0.7f);
                PlayerPrefs.SetFloat("HandlingStatSDCF1", 0.8f);

                //F1Concept
                PlayerPrefsExtra.SetBool("F1ConceptEngBtn", tier.car[5].EngBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("F1ConceptEngImg", tier.car[5].EngImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("F1ConceptEngBtn1", tier.car[5].EngBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("F1ConceptEngImg1", tier.car[5].EngImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("F1ConceptEngBtn2", tier.car[5].EngBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("F1ConceptEngImg2", tier.car[5].EngImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("F1ConceptSuspBtn", tier.car[5].SuspBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("F1ConceptSuspImg", tier.car[5].SuspImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("F1ConceptSuspBtn1", tier.car[5].SuspBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("F1ConceptSuspImg1", tier.car[5].SuspImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("F1ConceptSuspBtn2", tier.car[5].SuspBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("F1ConceptSuspImg2", tier.car[5].SuspImg[2].enabled = true);
                PlayerPrefsExtra.SetBool("F1ConceptTBBtn", tier.car[5].TBBtn[0].interactable = true);
                PlayerPrefsExtra.SetBool("F1ConceptTBImg", tier.car[5].TBImg[0].enabled = false);
                PlayerPrefsExtra.SetBool("F1ConceptTBBtn1", tier.car[5].TBBtn[1].interactable = false);
                PlayerPrefsExtra.SetBool("F1ConceptTBImg1", tier.car[5].TBImg[1].enabled = true);
                PlayerPrefsExtra.SetBool("F1ConceptTBBtn2", tier.car[5].TBBtn[2].interactable = false);
                PlayerPrefsExtra.SetBool("F1ConceptTBImg2", tier.car[5].TBImg[2].enabled = true);

                PlayerPrefs.SetFloat("AccStatF1Concept", 0.3f);
                PlayerPrefs.SetFloat("TopSpeedStatF1Concept", 0.4f);
                PlayerPrefs.SetFloat("HandlingStatF1Concept", 0.4f);

                firsttimecount++;
                PlayerPrefs.SetInt("LevelScriptFirstTime2", firsttimecount);
            }
        }
        private void Update()
        {
            playerCash = PlayerPrefs.GetFloat("Wallet");
            //Debug.Log(activeCarIndex);
            tier = GetActiveCar();
            SuspensionTimeCheck();
            EngineCheckTime();
            //NOSCheckTime();
            TyreCheckTime();
            // Debug.Log("ActiveCarIndex: " + activeCarIndex);

            //BacMono
            if (activeCarIndex == 0) //THESE ACTIVECARINDEX CONDITIONS WILL UPDATE THE UPGRADE BUTTONS, UPGRADED STATS AND LOCK IMAGES FOR EACH CAR.
            {
                if (PlayerPrefs.GetInt("BacEngCount") > 0)
                {
                    ActiveCar.powertrain.transmission.finalGearRatio = PlayerPrefs.GetFloat("BacMonoEngGPower");
                    ActiveCar.powertrain.engine.maxPower = PlayerPrefs.GetFloat("BacMonoEngMaxPower");
                }
                if (PlayerPrefs.GetInt("BacSuspCount") > 0)
                {
                    ActiveCar.steering.maximumSteerAngle = PlayerPrefs.GetFloat("BacMonoSteer");
                    ActiveCar.brakes.maxTorque = PlayerPrefs.GetFloat("BacMonoBrake");
                }
                if (PlayerPrefs.GetInt("BacTBCount") > 0)
                {
                    for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                    {
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient = PlayerPrefs.GetFloat("BacMonoSideForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient = PlayerPrefs.GetFloat("BacMonoForwardForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient = PlayerPrefs.GetFloat("BacMonoSideSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient = PlayerPrefs.GetFloat("BacMonoForwardSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength = PlayerPrefs.GetFloat("BacMonoMaxLength" + i);
                    }
                }

                //Debug.Log("BACMONO SPECS SPECS");
                tier.car[activeCarIndex].EngBtn[0].interactable = PlayerPrefsExtra.GetBool("BacMonoEngBtn");
                tier.car[activeCarIndex].EngImg[0].enabled = PlayerPrefsExtra.GetBool("BacMonoEngImg");
                tier.car[activeCarIndex].EngBtn[1].interactable = PlayerPrefsExtra.GetBool("BacMonoEngBtn1");
                tier.car[activeCarIndex].EngImg[1].enabled = PlayerPrefsExtra.GetBool("BacMonoEngImg1");
                tier.car[activeCarIndex].EngBtn[2].interactable = PlayerPrefsExtra.GetBool("BacMonoEngBtn2");
                tier.car[activeCarIndex].EngImg[2].enabled = PlayerPrefsExtra.GetBool("BacMonoEngImg2");

                tier.car[activeCarIndex].SuspBtn[0].interactable = PlayerPrefsExtra.GetBool("BacMonoSuspBtn");
                tier.car[activeCarIndex].SuspImg[0].enabled = PlayerPrefsExtra.GetBool("BacMonoSuspImg");
                tier.car[activeCarIndex].SuspBtn[1].interactable = PlayerPrefsExtra.GetBool("BacMonoSuspBtn1");
                tier.car[activeCarIndex].SuspImg[1].enabled = PlayerPrefsExtra.GetBool("BacMonoSuspImg1");
                tier.car[activeCarIndex].SuspBtn[2].interactable = PlayerPrefsExtra.GetBool("BacMonoSuspBtn2");
                tier.car[activeCarIndex].SuspImg[2].enabled = PlayerPrefsExtra.GetBool("BacMonoSuspImg2");

                tier.car[activeCarIndex].TBBtn[0].interactable = PlayerPrefsExtra.GetBool("BacMonoTBBtn");
                tier.car[activeCarIndex].TBImg[0].enabled = PlayerPrefsExtra.GetBool("BacMonoTBImg");
                tier.car[activeCarIndex].TBBtn[1].interactable = PlayerPrefsExtra.GetBool("BacMonoTBBtn1");
                tier.car[activeCarIndex].TBImg[1].enabled = PlayerPrefsExtra.GetBool("BacMonoTBImg1");
                tier.car[activeCarIndex].TBBtn[2].interactable = PlayerPrefsExtra.GetBool("BacMonoTBBtn2");
                tier.car[activeCarIndex].TBImg[2].enabled = PlayerPrefsExtra.GetBool("BacMonoTBImg2");

                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBacMono");
                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatBacMono");
                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBacMono");
            }

            //Batmobile
            if (activeCarIndex == 1)
            {

                if (PlayerPrefs.GetInt("BatEngCount") > 0)
                {
                    ActiveCar.powertrain.transmission.finalGearRatio = PlayerPrefs.GetFloat("BatMobileEngGPower");
                    ActiveCar.powertrain.engine.maxPower = PlayerPrefs.GetFloat("BatMobileEngMaxPower");
                }
                if (PlayerPrefs.GetInt("BatSuspCount") > 0)
                {
                    ActiveCar.steering.maximumSteerAngle = PlayerPrefs.GetFloat("BatMobileSteer");
                    ActiveCar.brakes.maxTorque = PlayerPrefs.GetFloat("BatMobileBrake");
                }
                if (PlayerPrefs.GetInt("BatTBCount") > 0)
                {
                    for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                    {
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient = PlayerPrefs.GetFloat("BatMobileSideForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient = PlayerPrefs.GetFloat("BatMobileForwardForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient = PlayerPrefs.GetFloat("BatMobileSideSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient = PlayerPrefs.GetFloat("BatMobileForwardSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength = PlayerPrefs.GetFloat("BatMobileMaxLength" + i);
                    }
                }


                //Debug.Log("BATMOBILE SPECS");
                tier.car[activeCarIndex].EngBtn[0].interactable = PlayerPrefsExtra.GetBool("BatMobileEngBtn");
                tier.car[activeCarIndex].EngImg[0].enabled = PlayerPrefsExtra.GetBool("BatMobileEngImg");
                tier.car[activeCarIndex].EngBtn[1].interactable = PlayerPrefsExtra.GetBool("BatMobileEngBtn1");
                tier.car[activeCarIndex].EngImg[1].enabled = PlayerPrefsExtra.GetBool("BatMobileEngImg1");
                tier.car[activeCarIndex].EngBtn[2].interactable = PlayerPrefsExtra.GetBool("BatMobileEngBtn2");
                tier.car[activeCarIndex].EngImg[2].enabled = PlayerPrefsExtra.GetBool("BatMobileEngImg2");

                tier.car[activeCarIndex].SuspBtn[0].interactable = PlayerPrefsExtra.GetBool("BatMobileSuspBtn");
                tier.car[activeCarIndex].SuspImg[0].enabled = PlayerPrefsExtra.GetBool("BatMobileSuspImg");
                tier.car[activeCarIndex].SuspBtn[1].interactable = PlayerPrefsExtra.GetBool("BatMobileSuspBtn1");
                tier.car[activeCarIndex].SuspImg[1].enabled = PlayerPrefsExtra.GetBool("BatMobileSuspImg1");
                tier.car[activeCarIndex].SuspBtn[2].interactable = PlayerPrefsExtra.GetBool("BatMobileSuspBtn2");
                tier.car[activeCarIndex].SuspImg[2].enabled = PlayerPrefsExtra.GetBool("BatMobileSuspImg2");

                tier.car[activeCarIndex].TBBtn[0].interactable = PlayerPrefsExtra.GetBool("BatMobileTBBtn");
                tier.car[activeCarIndex].TBImg[0].enabled = PlayerPrefsExtra.GetBool("BatMobileTBImg");
                tier.car[activeCarIndex].TBBtn[1].interactable = PlayerPrefsExtra.GetBool("BatMobileTBBtn1");
                tier.car[activeCarIndex].TBImg[1].enabled = PlayerPrefsExtra.GetBool("BatMobileTBImg1");
                tier.car[activeCarIndex].TBBtn[2].interactable = PlayerPrefsExtra.GetBool("BatMobileTBBtn2");
                tier.car[activeCarIndex].TBImg[2].enabled = PlayerPrefsExtra.GetBool("BatMobileTBImg2");

                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBatMobile");
                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatBatMobile");
                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBatMobile");
            }
            //SCD2
            if (activeCarIndex == 2)
            {

                if (PlayerPrefs.GetInt("SCD2EngCount") > 0)
                {
                    ActiveCar.powertrain.transmission.finalGearRatio = PlayerPrefs.GetFloat("SCD2EngGPower");
                    ActiveCar.powertrain.engine.maxPower = PlayerPrefs.GetFloat("SCD2EngMaxPower");
                }
                if (PlayerPrefs.GetInt("SCD2SuspCount") > 0)
                {
                    ActiveCar.steering.maximumSteerAngle = PlayerPrefs.GetFloat("SCD2Steer");
                    ActiveCar.brakes.maxTorque = PlayerPrefs.GetFloat("SCD2Brake");
                }
                if (PlayerPrefs.GetInt("SCD2TBCount") > 0)
                {
                    for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                    {
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient = PlayerPrefs.GetFloat("SCD2SideForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient = PlayerPrefs.GetFloat("SCD2ForwardForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient = PlayerPrefs.GetFloat("SCD2SideSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient = PlayerPrefs.GetFloat("SCD2ForwardSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength = PlayerPrefs.GetFloat("SCD2MaxLength" + i);
                    }
                }


                //Debug.Log("SCD2 SPECS");
                tier.car[activeCarIndex].EngBtn[0].interactable = PlayerPrefsExtra.GetBool("SCD2EngBtn");
                tier.car[activeCarIndex].EngImg[0].enabled = PlayerPrefsExtra.GetBool("SCD2EngImg");
                tier.car[activeCarIndex].EngBtn[1].interactable = PlayerPrefsExtra.GetBool("SCD2EngBtn1");
                tier.car[activeCarIndex].EngImg[1].enabled = PlayerPrefsExtra.GetBool("SCD2EngImg1");
                tier.car[activeCarIndex].EngBtn[2].interactable = PlayerPrefsExtra.GetBool("SCD2EngBtn2");
                tier.car[activeCarIndex].EngImg[2].enabled = PlayerPrefsExtra.GetBool("SCD2EngImg2");

                tier.car[activeCarIndex].SuspBtn[0].interactable = PlayerPrefsExtra.GetBool("SCD2SuspBtn");
                tier.car[activeCarIndex].SuspImg[0].enabled = PlayerPrefsExtra.GetBool("SCD2SuspImg");
                tier.car[activeCarIndex].SuspBtn[1].interactable = PlayerPrefsExtra.GetBool("SCD2SuspBtn1");
                tier.car[activeCarIndex].SuspImg[1].enabled = PlayerPrefsExtra.GetBool("SCD2SuspImg1");
                tier.car[activeCarIndex].SuspBtn[2].interactable = PlayerPrefsExtra.GetBool("SCD2SuspBtn2");
                tier.car[activeCarIndex].SuspImg[2].enabled = PlayerPrefsExtra.GetBool("SCD2SuspImg2");

                tier.car[activeCarIndex].TBBtn[0].interactable = PlayerPrefsExtra.GetBool("SCD2TBBtn");
                tier.car[activeCarIndex].TBImg[0].enabled = PlayerPrefsExtra.GetBool("SCD2TBImg");
                tier.car[activeCarIndex].TBBtn[1].interactable = PlayerPrefsExtra.GetBool("SCD2TBBtn1");
                tier.car[activeCarIndex].TBImg[1].enabled = PlayerPrefsExtra.GetBool("SCD2TBImg1");
                tier.car[activeCarIndex].TBBtn[2].interactable = PlayerPrefsExtra.GetBool("SCD2TBBtn2");
                tier.car[activeCarIndex].TBImg[2].enabled = PlayerPrefsExtra.GetBool("SCD2TBImg2");

                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSCD2");
                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatSCD2");
                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSCD2");
            }

            //RampCarN1ew
            if (activeCarIndex == 3)
            {

                if (PlayerPrefs.GetInt("RampEngCount") > 0)
                {
                    ActiveCar.powertrain.transmission.finalGearRatio = PlayerPrefs.GetFloat("RampEngGPower");
                    ActiveCar.powertrain.engine.maxPower = PlayerPrefs.GetFloat("RampEngMaxPower");
                    AccelerationStat.fillAmount += 0.1f; //get playerpref values over here when you make the functionality ahead. save the playerpref values in the upgrade conditions of the cars below.
                    TopspeedStat.fillAmount += 0.1f;
                }
                if (PlayerPrefs.GetInt("RampSuspCount") > 0)
                {
                    ActiveCar.steering.maximumSteerAngle = PlayerPrefs.GetFloat("RampSteer");
                    ActiveCar.brakes.maxTorque = PlayerPrefs.GetFloat("RampBrake");
                    AccelerationStat.fillAmount += 0.1f;
                    HandlingStat.fillAmount += 0.1f;
                }
                if (PlayerPrefs.GetInt("RampTBCount") > 0)
                {
                    for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                    {
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient = PlayerPrefs.GetFloat("RampSideForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient = PlayerPrefs.GetFloat("RampForwardForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient = PlayerPrefs.GetFloat("RampSideSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient = PlayerPrefs.GetFloat("RampForwardSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength = PlayerPrefs.GetFloat("RampMaxLength" + i);
                    }
                }


                //Debug.Log("Ramp SPECS");
                tier.car[activeCarIndex].EngBtn[0].interactable = PlayerPrefsExtra.GetBool("RampEngBtn");
                tier.car[activeCarIndex].EngImg[0].enabled = PlayerPrefsExtra.GetBool("RampEngImg");
                tier.car[activeCarIndex].EngBtn[1].interactable = PlayerPrefsExtra.GetBool("RampEngBtn1");
                tier.car[activeCarIndex].EngImg[1].enabled = PlayerPrefsExtra.GetBool("RampEngImg1");
                tier.car[activeCarIndex].EngBtn[2].interactable = PlayerPrefsExtra.GetBool("RampEngBtn2");
                tier.car[activeCarIndex].EngImg[2].enabled = PlayerPrefsExtra.GetBool("RampEngImg2");

                tier.car[activeCarIndex].SuspBtn[0].interactable = PlayerPrefsExtra.GetBool("RampSuspBtn");
                tier.car[activeCarIndex].SuspImg[0].enabled = PlayerPrefsExtra.GetBool("RampSuspImg");
                tier.car[activeCarIndex].SuspBtn[1].interactable = PlayerPrefsExtra.GetBool("RampSuspBtn1");
                tier.car[activeCarIndex].SuspImg[1].enabled = PlayerPrefsExtra.GetBool("RampSuspImg1");
                tier.car[activeCarIndex].SuspBtn[2].interactable = PlayerPrefsExtra.GetBool("RampSuspBtn2");
                tier.car[activeCarIndex].SuspImg[2].enabled = PlayerPrefsExtra.GetBool("RampSuspImg2");

                tier.car[activeCarIndex].TBBtn[0].interactable = PlayerPrefsExtra.GetBool("RampTBBtn");
                tier.car[activeCarIndex].TBImg[0].enabled = PlayerPrefsExtra.GetBool("RampTBImg");
                tier.car[activeCarIndex].TBBtn[1].interactable = PlayerPrefsExtra.GetBool("RampTBBtn1");
                tier.car[activeCarIndex].TBImg[1].enabled = PlayerPrefsExtra.GetBool("RampTBImg1");
                tier.car[activeCarIndex].TBBtn[2].interactable = PlayerPrefsExtra.GetBool("RampTBBtn2");
                tier.car[activeCarIndex].TBImg[2].enabled = PlayerPrefsExtra.GetBool("RampTBImg2");

                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatRamp");
                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatRamp");
                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatRamp");
            }


            //SDCF1
            if (activeCarIndex == 4)
            {

                if (PlayerPrefs.GetInt("SDCF1EngCount") > 0)
                {
                    ActiveCar.powertrain.transmission.finalGearRatio = PlayerPrefs.GetFloat("SDCF1EngGPower");
                    ActiveCar.powertrain.engine.maxPower = PlayerPrefs.GetFloat("SDCF1EngMaxPower");
                }
                if (PlayerPrefs.GetInt("SDCF1SuspCount") > 0)
                {
                    ActiveCar.steering.maximumSteerAngle = PlayerPrefs.GetFloat("SDCF1Steer");
                    ActiveCar.brakes.maxTorque = PlayerPrefs.GetFloat("SDCF1Brake");
                }
                if (PlayerPrefs.GetInt("SDCF1TBCount") > 0)
                {
                    for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                    {
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient = PlayerPrefs.GetFloat("SDCF1SideForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient = PlayerPrefs.GetFloat("SDCF1ForwardForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient = PlayerPrefs.GetFloat("SDCF1SideSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient = PlayerPrefs.GetFloat("SDCF1ForwardSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength = PlayerPrefs.GetFloat("SDCF1MaxLength" + i);
                    }
                }


                //Debug.Log("SDCF1 SPECS");
                tier.car[activeCarIndex].EngBtn[0].interactable = PlayerPrefsExtra.GetBool("SDCF1EngBtn");
                tier.car[activeCarIndex].EngImg[0].enabled = PlayerPrefsExtra.GetBool("SDCF1EngImg");
                tier.car[activeCarIndex].EngBtn[1].interactable = PlayerPrefsExtra.GetBool("SDCF1EngBtn1");
                tier.car[activeCarIndex].EngImg[1].enabled = PlayerPrefsExtra.GetBool("SDCF1EngImg1");
                tier.car[activeCarIndex].EngBtn[2].interactable = PlayerPrefsExtra.GetBool("SDCF1EngBtn2");
                tier.car[activeCarIndex].EngImg[2].enabled = PlayerPrefsExtra.GetBool("SDCF1EngImg2");


                tier.car[activeCarIndex].SuspBtn[0].interactable = PlayerPrefsExtra.GetBool("SDCF1SuspBtn");
                tier.car[activeCarIndex].SuspImg[0].enabled = PlayerPrefsExtra.GetBool("SDCF1SuspImg");
                tier.car[activeCarIndex].SuspBtn[1].interactable = PlayerPrefsExtra.GetBool("SDCF1SuspBtn1");
                tier.car[activeCarIndex].SuspImg[1].enabled = PlayerPrefsExtra.GetBool("SDCF1SuspImg1");
                tier.car[activeCarIndex].SuspBtn[2].interactable = PlayerPrefsExtra.GetBool("SDCF1SuspBtn2");
                tier.car[activeCarIndex].SuspImg[2].enabled = PlayerPrefsExtra.GetBool("SDCF1SuspImg2");

                tier.car[activeCarIndex].TBBtn[0].interactable = PlayerPrefsExtra.GetBool("SDCF1TBBtn");
                tier.car[activeCarIndex].TBImg[0].enabled = PlayerPrefsExtra.GetBool("SDCF1TBImg");
                tier.car[activeCarIndex].TBBtn[1].interactable = PlayerPrefsExtra.GetBool("SDCF1TBBtn1");
                tier.car[activeCarIndex].TBImg[1].enabled = PlayerPrefsExtra.GetBool("SDCF1TBImg1");
                tier.car[activeCarIndex].TBBtn[2].interactable = PlayerPrefsExtra.GetBool("SDCF1TBBtn2");
                tier.car[activeCarIndex].TBImg[2].enabled = PlayerPrefsExtra.GetBool("SDCF1TBImg2");

                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSDCF1");
                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatSDCF1");
                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSDCF1");
            }

            //F1Concept
            if (activeCarIndex == 5)
            {

                if (PlayerPrefs.GetInt("F1ConceptEngCount") > 0)
                {
                    ActiveCar.powertrain.transmission.finalGearRatio = PlayerPrefs.GetFloat("F1ConceptEngGPower");
                    ActiveCar.powertrain.engine.maxPower = PlayerPrefs.GetFloat("F1ConceptEngMaxPower");
                }
                if (PlayerPrefs.GetInt("F1ConceptSuspCount") > 0)
                {
                    ActiveCar.steering.maximumSteerAngle = PlayerPrefs.GetFloat("F1ConceptSteer");
                    ActiveCar.brakes.maxTorque = PlayerPrefs.GetFloat("F1ConceptBrake");
                }
                if (PlayerPrefs.GetInt("F1ConceptTBCount") > 0)
                {
                    for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                    {
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient = PlayerPrefs.GetFloat("F1ConceptSideForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient = PlayerPrefs.GetFloat("F1ConceptForwardForce" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient = PlayerPrefs.GetFloat("F1ConceptSideSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient = PlayerPrefs.GetFloat("F1ConceptForwardSlip" + i);
                        tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength = PlayerPrefs.GetFloat("F1ConceptMaxLength" + i);
                    }
                }


                //Debug.Log("F1CONCEPT SPECS");
                tier.car[activeCarIndex].EngBtn[0].interactable = PlayerPrefsExtra.GetBool("F1ConceptEngBtn");
                tier.car[activeCarIndex].EngImg[0].enabled = PlayerPrefsExtra.GetBool("F1ConceptEngImg");
                tier.car[activeCarIndex].EngBtn[1].interactable = PlayerPrefsExtra.GetBool("F1ConceptEngBtn1");
                tier.car[activeCarIndex].EngImg[1].enabled = PlayerPrefsExtra.GetBool("F1ConceptEngImg1");
                tier.car[activeCarIndex].EngBtn[2].interactable = PlayerPrefsExtra.GetBool("F1ConceptEngBtn2");
                tier.car[activeCarIndex].EngImg[2].enabled = PlayerPrefsExtra.GetBool("F1ConceptEngImg2");

                tier.car[activeCarIndex].SuspBtn[0].interactable = PlayerPrefsExtra.GetBool("F1ConceptSuspBtn");
                tier.car[activeCarIndex].SuspImg[0].enabled = PlayerPrefsExtra.GetBool("F1ConceptSuspImg");
                tier.car[activeCarIndex].SuspBtn[1].interactable = PlayerPrefsExtra.GetBool("F1ConceptSuspBtn1");
                tier.car[activeCarIndex].SuspImg[1].enabled = PlayerPrefsExtra.GetBool("F1ConceptSuspImg1");
                tier.car[activeCarIndex].SuspBtn[2].interactable = PlayerPrefsExtra.GetBool("F1ConceptSuspBtn2");
                tier.car[activeCarIndex].SuspImg[2].enabled = PlayerPrefsExtra.GetBool("F1ConceptSuspImg2");

                tier.car[activeCarIndex].TBBtn[0].interactable = PlayerPrefsExtra.GetBool("F1ConceptTBBtn");
                tier.car[activeCarIndex].TBImg[0].enabled = PlayerPrefsExtra.GetBool("F1ConceptTBImg");
                tier.car[activeCarIndex].TBBtn[1].interactable = PlayerPrefsExtra.GetBool("F1ConceptTBBtn1");
                tier.car[activeCarIndex].TBImg[1].enabled = PlayerPrefsExtra.GetBool("F1ConceptTBImg1");
                tier.car[activeCarIndex].TBBtn[2].interactable = PlayerPrefsExtra.GetBool("F1ConceptTBBtn2");
                tier.car[activeCarIndex].TBImg[2].enabled = PlayerPrefsExtra.GetBool("F1ConceptTBImg2");

                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatF1Concept");
                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatF1Concept");
                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatF1Concept");
            }

        }

        #region UpgradeButtonClicks
        public void OnSuspensionButtonClick(int currencyType)
        {
            Debug.Log("suspension");
            string status = PlayerPrefs.GetString("PartUpgrading");
            if (status == "false")
            {
                tier = GetActiveCar();
                Debug.Log(tier);
                if (!PlayerPrefs.HasKey(ActiveCar.gameObject.name + "Suspension"))
                {
                    PlayerPrefs.SetInt(ActiveCar.gameObject.name + "Suspension", 0);
                }
                int index = PlayerPrefs.GetInt(ActiveCar.gameObject.name + "Suspension");
                if (currencyType == 0)
                {
                    if (playerCash >= tier.car[activeCarIndex].suspension[index].Coins)
                    {
                        float buyCash = playerCash - tier.car[activeCarIndex].suspension[index].Coins;
                        //PlayerPrefs.SetInt(Login.instance.playerName + "SavedCash", buyCash);
                        TimeMaster.instance.SaveDate(ActiveCar.gameObject.name + "SuspensionTime");
                        PlayerPrefs.SetString("suspensionUpgrading", "true");
                        PlayerPrefs.SetString("PartUpgrading", "true");
                    }
                    else
                    {
                        carselection.LowCashBanner.SetActive(true);
                        StartCoroutine(LowCash());
                        if (notEnoughCashText != null) notEnoughCashText.text = "Not Enough Coins To Make the Purchase";
                        StartCoroutine("textPopUp");
                    }
                }
                else if (currencyType == 1)
                {
                    if (playerDaimonds > tier.car[activeCarIndex].suspension[index].Daimonds)
                    {
                        int buyCash = playerDaimonds - tier.car[activeCarIndex].suspension[index].Daimonds;
                        //PlayerPrefs.SetInt(Login.instance.playerName + "SavedCash", buyCash);
                        TimeMaster.instance.SaveDate(ActiveCar.gameObject.name + "SuspensionTime");
                        PlayerPrefs.SetString("suspensionUpgrading", "true");
                        PlayerPrefs.SetString("PartUpgrading", "true");
                    }
                    else
                    {
                        if (notEnoughCashText != null) notEnoughCashText.text = "Not Enough Daimonds To Make the Purchase";
                        StartCoroutine("textPopUp");
                    }
                }
            }
            else
            {
                //Upgrade in Process
            }
        }
        public void OnEngineButtonClick(int currencyType)
        {
            Debug.Log("engine");
            string status = PlayerPrefs.GetString("PartUpgrading");
            if (status == "false")
            {
                Debug.Log("false");
                tier = GetActiveCar();
                if (!PlayerPrefs.HasKey(ActiveCar.gameObject.name + "Engine"))
                {
                    PlayerPrefs.SetInt(ActiveCar.gameObject.name + "Engine", 0);
                    Debug.Log("0.5");
                }
                int index = PlayerPrefs.GetInt(ActiveCar.gameObject.name + "Engine");
                Debug.Log("engine level" + index);

                if (currencyType == 0)
                {
                    if (playerCash >= tier.car[activeCarIndex].engine[index].Coins)
                    {
                        Debug.Log("1");

                        float buyCash = playerCash - tier.car[activeCarIndex].engine[index].Coins;
                        PlayerPrefs.SetFloat("Wallet", buyCash);
                        //PlayerPrefs.SetInt(Login.instance.playerName + "SavedCash", buyCash);
                        TimeMaster.instance.SaveDate(ActiveCar.gameObject.name + "EngineTime");
                        PlayerPrefs.SetString("engineUpgrading", "true");
                        PlayerPrefs.SetString("PartUpgrading", "true");
                    }
                    else
                    {
                        //Debug.Log("1.5");
                        carselection.LowCashBanner.SetActive(true);
                        StartCoroutine(LowCash());
                        if (notEnoughCashText != null) notEnoughCashText.text = "Not Enough Coins To Make the Purchase";
                        StartCoroutine("textPopUp");
                    }
                }
                else if (currencyType == 1)
                {
                    if (playerDaimonds > tier.car[activeCarIndex].engine[index].Daimonds)
                    {
                        int buyCash = playerDaimonds - tier.car[activeCarIndex].engine[index].Daimonds;
                        //PlayerPrefs.SetInt(Login.instance.playerName + "SavedCash", buyCash);
                        TimeMaster.instance.SaveDate(ActiveCar.gameObject.name + "EngineTime");
                        PlayerPrefs.SetString("engineUpgrading", "true");
                        PlayerPrefs.SetString("PartUpgrading", "true");
                    }
                    else
                    {
                        if (notEnoughCashText != null) notEnoughCashText.text = "Not Enough Coins To Make the Purchase";
                        StartCoroutine("textPopUp");
                    }
                }
            }
            else
            {
                //Upgrade Already in process
            }
        }
        //public void OnNOSButtonClick(int currencyType)
        //{
        //    string status = PlayerPrefs.GetString("PartUpgrading");
        //    if (status == "false")
        //    {
        //        tier = GetActiveCar();
        //        if (!PlayerPrefs.HasKey(ActiveCar.gameObject.name + "NOS"))
        //        {
        //            PlayerPrefs.SetInt(ActiveCar.gameObject.name + "NOS", 0);
        //        }
        //        int index = PlayerPrefs.GetInt(ActiveCar.gameObject.name + "NOS");
        //        if (currencyType == 0)
        //        {
        //            if (playerCash > tier.car[activeCarIndex].nos[index].Coins)
        //            {
        //                int buyCash = playerCash - tier.car[activeCarIndex].nos[index].Coins;
        //                //PlayerPrefs.SetInt(Login.instance.playerName + "SavedCash", buyCash);
        //                TimeMaster.instance.SaveDate(ActiveCar.gameObject.name + "NOSTime");
        //                PlayerPrefs.SetString("nosUpgrading", "true");
        //                PlayerPrefs.SetString("PartUpgrading", "true");
        //            }
        //            else
        //            {
        //                notEnoughCashText.text = "Not Enough Coins To Make the Purchase";
        //            }
        //        }
        //        else if (currencyType == 1)
        //        {
        //            if (playerDaimonds > tier.car[activeCarIndex].nos[index].Daimonds)
        //            {
        //                int buyCash = playerDaimonds - tier.car[activeCarIndex].nos[index].Daimonds;
        //                //PlayerPrefs.SetInt(Login.instance.playerName + "SavedCash", buyCash);
        //                TimeMaster.instance.SaveDate(ActiveCar.gameObject.name + "NOSTime");
        //                PlayerPrefs.SetString("nosUpgrading", "true");
        //                PlayerPrefs.SetString("PartUpgrading", "true");
        //            }
        //            else
        //            {
        //                notEnoughCashText.text = "Not Enough Coins To Make the Purchase";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Upgrade Already in Process
        //    }
        //}
        public void OnTyreButtonClick(int currencyType)
        {
            string status = PlayerPrefs.GetString("PartUpgrading");
            if (status == "false")
            {
                tier = GetActiveCar();
                if (!PlayerPrefs.HasKey(ActiveCar.gameObject.name + "Tyre"))
                {
                    PlayerPrefs.SetInt(ActiveCar.gameObject.name + "Tyre", 0);
                }
                int index = PlayerPrefs.GetInt(ActiveCar.gameObject.name + "Tyre");
                if (currencyType == 0)
                {
                    if (playerCash >= tier.car[activeCarIndex].tyre[index].Coins)
                    {
                        float buyCash = playerCash - tier.car[activeCarIndex].tyre[index].Coins;
                        //PlayerPrefs.SetInt(Login.instance.playerName + "SavedCash", buyCash);
                        TimeMaster.instance.SaveDate(ActiveCar.gameObject.name + "TyreTime");
                        PlayerPrefs.SetString("tyreUpgrading", "true");
                        PlayerPrefs.SetString("PartUpgrading", "true");
                    }
                    else
                    {
                        carselection.LowCashBanner.SetActive(true);
                        StartCoroutine(LowCash());
                        if (notEnoughCashText != null) notEnoughCashText.text = "Not Enough Coins To Make the Purchase";
                    }
                }
                else if (currencyType == 1)
                {
                    if (playerDaimonds > tier.car[activeCarIndex].tyre[index].Daimonds)
                    {
                        int buyCash = playerDaimonds - tier.car[activeCarIndex].tyre[index].Daimonds;
                        //PlayerPrefs.SetInt(Login.instance.playerName + "SavedCash", buyCash);
                        TimeMaster.instance.SaveDate(ActiveCar.gameObject.name + "TyreTime");
                        PlayerPrefs.SetString("tyreUpgrading", "true");
                        PlayerPrefs.SetString("PartUpgrading", "true");
                    }
                    else
                    {
                        if (notEnoughCashText != null) notEnoughCashText.text = "Not Enough Coins To Make the Purchase";
                    }
                }
            }
            else
            {
                // Upgrade Already in Process
            }
        }
        #endregion

        #region UpgradeTimerCheck
        private void SuspensionTimeCheck()
        {
            if (PlayerPrefs.HasKey("suspensionUpgrading"))
            {
                string suspensionUpgrading = PlayerPrefs.GetString("suspensionUpgrading");
                if (suspensionUpgrading == "true")
                {
                    float time = TimeMaster.instance.checkDate(ActiveCar.gameObject.name + "SuspensionTime");
                    if (timeToUpgrade != null) timeToUpgrade.text = time.ToString();
                    int index = PlayerPrefs.GetInt(ActiveCar.gameObject.name + "Suspension");
                    TimeLeftBanner.text = "TIME LEFT IN UPGRADE: " + tier.car[activeCarIndex].engine[index].Timer + "secs".ToString();
                    if (time > tier.car[activeCarIndex].suspension[index].Timer)
                    {
                        TimeLeftBanner.text = "";
                        ActiveCar.steering.maximumSteerAngle += tier.car[activeCarIndex].suspension[index].Handling;
                        ActiveCar.brakes.maxTorque += tier.car[activeCarIndex].suspension[index].BrakeTorque;
                        //ActiveCar.steerAngle = ActiveCar.steerAngle + tier.car[activeCarIndex].suspension[index].Handling;
                        //ActiveCar.brakeTorque = ActiveCar.brakeTorque + tier.car[activeCarIndex].suspension[index].BrakeTorque;
                        if (activeCarIndex == 0)
                        {
                            if (tier.car[activeCarIndex].carNumber == 0 && PlayerPrefs.GetInt("BacSuspCount") == 0)
                            {
                                PlayerPrefs.SetFloat("BacMonoSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("BacMonoBrake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[0].interactable = false;
                                tier.car[activeCarIndex].SuspImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoSuspBtn", tier.car[activeCarIndex].SuspBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("BacMonoSuspImg", tier.car[activeCarIndex].SuspImg[0].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = true;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoSuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("BacMonoSuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBacMono");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBacMono", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBacMono");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatBacMono", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 0 && PlayerPrefs.GetInt("BacSuspCount") == 1)
                            {
                                PlayerPrefs.SetFloat("BacMonoSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("BacMonoBrake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = false;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoSuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("BacMonoSuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[2].interactable = true;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoSuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("BacMonoSuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBacMono");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBacMono", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBacMono");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatBacMono", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 0 && PlayerPrefs.GetInt("BacSuspCount") == 2)
                            {
                                tier.car[activeCarIndex].SuspBtn[2].interactable = false;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoSuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("BacMonoSuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);
                                PlayerPrefs.SetFloat("BacMonoSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("BacMonoBrake", ActiveCar.brakes.maxTorque);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBacMono");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatBacMono", HandlingStat.fillAmount);
                            }
                            BacSuspCount++;
                            PlayerPrefs.SetInt("BacSuspCount", BacSuspCount);
                        }
                        if (activeCarIndex == 1)
                        {

                            if (tier.car[activeCarIndex].carNumber == 1 && PlayerPrefs.GetInt("BatSuspCount") == 0)
                            {
                                PlayerPrefs.SetFloat("BatMobileSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("BatMobileBrake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[0].interactable = false;
                                tier.car[activeCarIndex].SuspImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileSuspBtn", tier.car[activeCarIndex].SuspBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("BatMobileSuspImg", tier.car[activeCarIndex].SuspImg[0].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = true;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileSuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("BatMobileSuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBatMobile");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBatMobile", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBatMobile");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatBatMobile", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 1 && PlayerPrefs.GetInt("BatSuspCount") == 1)
                            {
                                PlayerPrefs.SetFloat("BatMobileSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("BatMobileBrake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = false;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileSuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("BatMobileSuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[2].interactable = true;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileSuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("BatMobileSuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBatMobile");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBatMobile", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBatMobile");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatBatMobile", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 1 && PlayerPrefs.GetInt("BatSuspCount") == 2)
                            {
                                PlayerPrefs.SetFloat("BatMobileSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("BatMobileBrake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[2].interactable = false;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileSuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("BatMobileSuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBatMobile");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatBatMobile", HandlingStat.fillAmount);
                            }
                            BatSuspCount++;
                            PlayerPrefs.SetInt("BatSuspCount", BatSuspCount);
                        }

                        if (activeCarIndex == 2)
                        {

                            if (tier.car[activeCarIndex].carNumber == 2 && PlayerPrefs.GetInt("SCD2SuspCount") == 0)
                            {
                                PlayerPrefs.SetFloat("SCD2Steer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("SCD2Brake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[0].interactable = false;
                                tier.car[activeCarIndex].SuspImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2SuspBtn", tier.car[activeCarIndex].SuspBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("SCD2SuspImg", tier.car[activeCarIndex].SuspImg[0].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = true;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2SuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("SCD2SuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSCD2");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSCD2", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSCD2");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatSC2", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 2 && PlayerPrefs.GetInt("SCD2SuspCount") == 1)
                            {
                                PlayerPrefs.SetFloat("SCD2Steer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("SCD2Brake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = false;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2SuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("SCD2SuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[2].interactable = true;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2SuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("SCD2SuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSCD2");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSCD2", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSCD2");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatSC2", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 2 && PlayerPrefs.GetInt("SCD2SuspCount") == 2)
                            {
                                PlayerPrefs.SetFloat("SCD2Steer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("SCD2Brake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[2].interactable = false;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2SuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("SCD2SuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSCD2");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatSC2", HandlingStat.fillAmount);
                            }
                            SCD2SuspCount++;
                            PlayerPrefs.SetInt("SCD2SuspCount", SCD2SuspCount);
                        }

                        if (activeCarIndex == 3)
                        {

                            if (tier.car[activeCarIndex].carNumber == 3 && PlayerPrefs.GetInt("RampSuspCount") == 0)
                            {
                                PlayerPrefs.SetFloat("RampSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("RampBrake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[0].interactable = false;
                                tier.car[activeCarIndex].SuspImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("RampSuspBtn", tier.car[activeCarIndex].SuspBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("RampSuspImg", tier.car[activeCarIndex].SuspImg[0].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = true;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("RampSuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("RampSuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatRamp");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatRamp", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatRamp");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatRamp", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 3 && PlayerPrefs.GetInt("RampSuspCount") == 1)
                            {
                                PlayerPrefs.SetFloat("RampSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("RampBrake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = false;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("RampSuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("RampSuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[2].interactable = true;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("RampSuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("RampSuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatRamp");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatRamp", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatRamp");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatRamp", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 3 && PlayerPrefs.GetInt("RampSuspCount") == 2)
                            {
                                PlayerPrefs.SetFloat("RampSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("RampBrake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[2].interactable = false;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("RampSuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("RampSuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatRamp");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatRamp", HandlingStat.fillAmount);
                            }
                            RampSuspCount++;
                            PlayerPrefs.SetInt("RampSuspCount", RampSuspCount);
                        }

                        //SDCF1
                        if (activeCarIndex == 4)
                        {

                            if (tier.car[activeCarIndex].carNumber == 4 && PlayerPrefs.GetInt("SDCF1SuspCount") == 0)
                            {
                                PlayerPrefs.SetFloat("SDCF1Steer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("SDCF1Brake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[0].interactable = false;
                                tier.car[activeCarIndex].SuspImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1SuspBtn", tier.car[activeCarIndex].SuspBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("SDCF1SuspImg", tier.car[activeCarIndex].SuspImg[0].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = true;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1SuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("SDCF1SuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSDCF1");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSDCF1", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSDCF1");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatSDCF1", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 4 && PlayerPrefs.GetInt("SDCF1SuspCount") == 1)
                            {
                                PlayerPrefs.SetFloat("SDCF1Steer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("SDCF1Brake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = false;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1SuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("SDCF1SuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[2].interactable = true;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1SuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("SDCF1SuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSDCF1");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSDCF1", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSDCF1");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatSDCF1", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 4 && PlayerPrefs.GetInt("SDCF1SuspCount") == 2)
                            {
                                PlayerPrefs.SetFloat("SDCF1Steer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("SDCF1Brake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[2].interactable = false;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1SuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("SDCF1SuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSDCF1");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatSDCF1", HandlingStat.fillAmount);
                            }
                            SDCF1SuspCount++;
                            PlayerPrefs.SetInt("SDCF1SuspCount", SDCF1SuspCount);
                        }

                        //F1Concept
                        if (activeCarIndex == 5)
                        {

                            if (tier.car[activeCarIndex].carNumber == 5 && PlayerPrefs.GetInt("F1ConceptSuspCount") == 0)
                            {
                                PlayerPrefs.SetFloat("F1ConceptSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("F1ConceptBrake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[0].interactable = false;
                                tier.car[activeCarIndex].SuspImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptSuspBtn", tier.car[activeCarIndex].SuspBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("F1ConceptSuspImg", tier.car[activeCarIndex].SuspImg[0].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = true;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptSuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("F1ConceptSuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatF1Concept");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatF1Concept", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatF1Concept");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatF1Concept", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 5 && PlayerPrefs.GetInt("F1ConceptSuspCount") == 1)
                            {
                                PlayerPrefs.SetFloat("F1ConceptSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("F1ConceptBrake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[1].interactable = false;
                                tier.car[activeCarIndex].SuspImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptSuspBtn1", tier.car[activeCarIndex].SuspBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("F1ConceptSuspImg1", tier.car[activeCarIndex].SuspImg[1].enabled = false);
                                tier.car[activeCarIndex].SuspBtn[2].interactable = true;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptSuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("F1ConceptSuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatF1Concept");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatF1Concept", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatF1Concept");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatF1Concept", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 5 && PlayerPrefs.GetInt("F1ConceptSuspCount") == 2)
                            {
                                PlayerPrefs.SetFloat("F1ConceptSteer", ActiveCar.steering.maximumSteerAngle);
                                PlayerPrefs.SetFloat("F1ConceptBrake", ActiveCar.brakes.maxTorque);
                                tier.car[activeCarIndex].SuspBtn[2].interactable = false;
                                tier.car[activeCarIndex].SuspImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptSuspBtn2", tier.car[activeCarIndex].SuspBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("F1ConceptSuspImg2", tier.car[activeCarIndex].SuspImg[2].enabled = false);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatF1Concept");
                                HandlingStat.fillAmount += 0.1f;
                                PlayerPrefs.SetFloat("HandlingStatF1Concept", HandlingStat.fillAmount);
                            }
                            F1ConceptSuspCount++;
                            PlayerPrefs.SetInt("F1ConceptSuspCount", F1ConceptSuspCount);
                        }


                        index = index + 1;
                        PlayerPrefs.SetInt(ActiveCar.gameObject.name + "Suspension", index);
                        PlayerPrefs.SetString(ActiveCar.gameObject.name + "Suspension" + index, "true");
                        PlayerPrefs.SetString("suspensionUpgrading", "false");
                        PlayerPrefs.SetString("PartUpgrading", "false");
                    }
                }
            }
        }
        private void EngineCheckTime()
        {
            if (PlayerPrefs.HasKey("engineUpgrading"))
            {
                string engineUpgrading = PlayerPrefs.GetString("engineUpgrading");
                if (engineUpgrading == "true")
                {
                    Debug.Log("upgrading");

                    float time = TimeMaster.instance.checkDate(ActiveCar.gameObject.name + "EngineTime");
                    if (timeToUpgrade != null) timeToUpgrade.text = time.ToString();
                    int index = PlayerPrefs.GetInt(ActiveCar.gameObject.name + "Engine");
                    TimeLeftBanner.text = "TIME LEFT IN UPGRADE: " + tier.car[activeCarIndex].engine[index].Timer + "secs".ToString();
                    if (time > tier.car[activeCarIndex].engine[index].Timer)
                    {
                        TimeLeftBanner.text = "";
                        //ActiveCar.powertrain.engine.generatedPower += tier.car[activeCarIndex].engine[index].Acceleration;//change top speed to power.
                        //ActiveCar.powertrain.engine.maxPower += tier.car[activeCarIndex].engine[index].TopSpeed;//change top speed to maxpower.
                        ActiveCar.powertrain.engine.maxPower += tier.car[activeCarIndex].engine[index].Acceleration;
                        ActiveCar.powertrain.transmission.finalGearRatio -= tier.car[activeCarIndex].engine[index].TopSpeed;


                        //tier.car[activeCarIndex].EngCount++;
                        //if(tier.car[activeCarIndex].EngCount == 1)
                        //{
                        //BacMono Engine Upgarde Path
                        if (activeCarIndex == 0)
                        {

                            if (tier.car[activeCarIndex].carNumber == 0 && PlayerPrefs.GetInt("BacEngCount") == 0)
                            {
                                PlayerPrefs.SetFloat("BacMonoEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("BacMonoEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[0].interactable = false;
                                tier.car[activeCarIndex].EngImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoEngBtn", tier.car[activeCarIndex].EngBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("BacMonoEngImg", tier.car[activeCarIndex].EngImg[0].enabled = false);
                                tier.car[activeCarIndex].EngBtn[1].interactable = true;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoEngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("BacMonoEngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBacMono");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBacMono", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatBacMono");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatBacMono", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 0 && PlayerPrefs.GetInt("BacEngCount") == 1)
                            {

                                PlayerPrefs.SetFloat("BacMonoEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("BacMonoEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[1].interactable = false;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoEngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("BacMonoEngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);
                                tier.car[activeCarIndex].EngBtn[2].interactable = true;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoEngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("BacMonoEngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBacMono");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBacMono", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatBacMono");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatBacMono", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 0 && PlayerPrefs.GetInt("BacEngCount") == 2)
                            {
                                PlayerPrefs.SetFloat("BacMonoEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("BacMonoEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[2].interactable = false;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoEngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("BacMonoEngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBacMono");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBacMono", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatBacMono");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatBacMono", TopspeedStat.fillAmount);
                            }
                            BacEngCount++;
                            PlayerPrefs.SetInt("BacEngCount", BacEngCount);
                        }

                        //BatMobile Engine Upgarde Path
                        if (activeCarIndex == 1)
                        {
                            if (tier.car[activeCarIndex].carNumber == 1 && PlayerPrefs.GetInt("BatEngCount") == 0)
                            {

                                PlayerPrefs.SetFloat("BatMobileEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("BatMobileEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[0].interactable = false;
                                tier.car[activeCarIndex].EngImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileEngBtn", tier.car[activeCarIndex].EngBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("BatMobileEngImg", tier.car[activeCarIndex].EngImg[0].enabled = false);
                                tier.car[activeCarIndex].EngBtn[1].interactable = true;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileEngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("BatMobileEngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBatMobile");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBatMobile", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatBatMobile");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatBatMobile", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 1 && PlayerPrefs.GetInt("BatEngCount") == 1)
                            {

                                PlayerPrefs.SetFloat("BatMobileEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("BatMobileEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[1].interactable = false;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileEngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("BatMobileEngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);
                                tier.car[activeCarIndex].EngBtn[2].interactable = true;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileEngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("BatMobileEngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBatMobile");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBatMobile", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatBatMobile");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatBatMobile", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 1 && PlayerPrefs.GetInt("BatEngCount") == 2)
                            {
                                PlayerPrefs.SetFloat("BatMobileEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("BatMobileEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[2].interactable = false;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileEngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("BatMobileEngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBatMobile");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBatMobile", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatBatMobile");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatBatMobile", TopspeedStat.fillAmount);
                            }
                            BatEngCount++;
                            PlayerPrefs.SetInt("BatEngCount", BatEngCount);
                            //}
                            //Debug.Log("upgraded" + ActiveCar.gameObject.name);
                        }

                        //SCD2
                        if (activeCarIndex == 2)
                        {
                            if (tier.car[activeCarIndex].carNumber == 2 && PlayerPrefs.GetInt("SCD2EngCount") == 0)
                            {

                                PlayerPrefs.SetFloat("SCD2EngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("SCD2EngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[0].interactable = false;
                                tier.car[activeCarIndex].EngImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2EngBtn", tier.car[activeCarIndex].EngBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("SCD2EngImg", tier.car[activeCarIndex].EngImg[0].enabled = false);
                                tier.car[activeCarIndex].EngBtn[1].interactable = true;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2EngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("SCD2EngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSCD2");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSCD2", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatSCD2");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatSCD2", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 2 && PlayerPrefs.GetInt("SCD2EngCount") == 1)
                            {

                                PlayerPrefs.SetFloat("SCD2EngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("SCD2EngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[1].interactable = false;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2EngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("SCD2EngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);
                                tier.car[activeCarIndex].EngBtn[2].interactable = true;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2EngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("SCD2EngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSCD2");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSCD2", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatSCD2");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatSCD2", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 2 && PlayerPrefs.GetInt("SCD2EngCount") == 2)
                            {

                                PlayerPrefs.SetFloat("SCD2EngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("SCD2EngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[2].interactable = false;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2EngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("SCD2EngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSCD2");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSCD2", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatSCD2");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatSCD2", TopspeedStat.fillAmount);
                            }
                            SCD2EngCount++;
                            PlayerPrefs.SetInt("SCD2EngCount", SCD2EngCount);

                        }

                        //Ramp car N1ew
                        if (activeCarIndex == 3)
                        {
                            if (tier.car[activeCarIndex].carNumber == 3 && PlayerPrefs.GetInt("RampEngCount") == 0)
                            {

                                PlayerPrefs.SetFloat("RampEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("RampEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[0].interactable = false;
                                tier.car[activeCarIndex].EngImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("RampEngBtn", tier.car[activeCarIndex].EngBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("RampEngImg", tier.car[activeCarIndex].EngImg[0].enabled = false);
                                tier.car[activeCarIndex].EngBtn[1].interactable = true;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("RampEngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("RampEngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatRamp");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatRamp", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatRamp");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatRamp", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 3 && PlayerPrefs.GetInt("RampEngCount") == 1)
                            {

                                PlayerPrefs.SetFloat("RampEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("RampEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[1].interactable = false;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("RampEngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("RampEngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);
                                tier.car[activeCarIndex].EngBtn[2].interactable = true;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("RampEngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("RampEngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatRamp");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatRamp", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatRamp");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatRamp", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 3 && PlayerPrefs.GetInt("RampEngCount") == 2)
                            {

                                PlayerPrefs.SetFloat("RampEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("RampEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[2].interactable = false;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("RampEngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("RampEngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatRamp");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatRamp", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatRamp");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatRamp", TopspeedStat.fillAmount);
                            }
                            RampEngCount++;
                            PlayerPrefs.SetInt("RampEngCount", RampEngCount);

                        }

                        //SDCF1
                        if (activeCarIndex == 4)
                        {
                            if (tier.car[activeCarIndex].carNumber == 4 && PlayerPrefs.GetInt("SDCF1EngCount") == 0)
                            {

                                PlayerPrefs.SetFloat("SDCF1EngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("SDCF1EngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[0].interactable = false;
                                tier.car[activeCarIndex].EngImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1EngBtn", tier.car[activeCarIndex].EngBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("SDCF1EngImg", tier.car[activeCarIndex].EngImg[0].enabled = false);
                                tier.car[activeCarIndex].EngBtn[1].interactable = true;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1EngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("SDCF1EngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSDCF1");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSDCF1", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatSDCF1");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatSDCF1", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 4 && PlayerPrefs.GetInt("SDCF1EngCount") == 1)
                            {

                                PlayerPrefs.SetFloat("SDCF1EngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("SDCF1pEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[1].interactable = false;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1EngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("SDCF1EngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);
                                tier.car[activeCarIndex].EngBtn[2].interactable = true;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1EngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("SDCF1EngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSDCF1");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSDCF1", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatSDCF1");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatSDCF1", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 4 && PlayerPrefs.GetInt("SDCF1EngCount") == 2)
                            {

                                PlayerPrefs.SetFloat("SDCF1EngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("SDCF1pEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[2].interactable = false;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1EngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("SDCF1EngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSDCF1");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSDCF1", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatSDCF1");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatSDCF1", TopspeedStat.fillAmount);
                            }
                            SDCF1EngCount++;
                            PlayerPrefs.SetInt("SDCF1EngCount", SDCF1EngCount);

                        }

                        //F1Concept
                        if (activeCarIndex == 5)
                        {

                            if (tier.car[activeCarIndex].carNumber == 5 && PlayerPrefs.GetInt("F1ConceptEngCount") == 0)
                            {
                                PlayerPrefs.SetFloat("F1ConceptEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("F1ConceptEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[0].interactable = false;
                                tier.car[activeCarIndex].EngImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptEngBtn", tier.car[activeCarIndex].EngBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("F1ConceptEngImg", tier.car[activeCarIndex].EngImg[0].enabled = false);
                                tier.car[activeCarIndex].EngBtn[1].interactable = true;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptEngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("F1ConceptEngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatF1Concept");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatF1Concept", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatF1Concept");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatF1Concept", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 5 && PlayerPrefs.GetInt("F1ConceptEngCount") == 1)
                            {

                                PlayerPrefs.SetFloat("F1ConceptEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("F1ConceptEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[1].interactable = false;
                                tier.car[activeCarIndex].EngImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptEngBtn1", tier.car[activeCarIndex].EngBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("F1ConceptEngImg1", tier.car[activeCarIndex].EngImg[1].enabled = false);
                                tier.car[activeCarIndex].EngBtn[2].interactable = true;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptEngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("F1ConceptEngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatF1Concept");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatF1Concept", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatF1Concept");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatF1Concept", TopspeedStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 5 && PlayerPrefs.GetInt("F1ConceptEngCount") == 2)
                            {
                                PlayerPrefs.SetFloat("F1ConceptEngGPower", ActiveCar.powertrain.transmission.finalGearRatio);
                                PlayerPrefs.SetFloat("F1ConceptEngMaxPower", ActiveCar.powertrain.engine.maxPower);
                                tier.car[activeCarIndex].EngBtn[2].interactable = false;
                                tier.car[activeCarIndex].EngImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptEngBtn2", tier.car[activeCarIndex].EngBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("F1ConceptEngImg2", tier.car[activeCarIndex].EngImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatF1Concept");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatF1Concept", AccelerationStat.fillAmount);

                                TopspeedStat.fillAmount = PlayerPrefs.GetFloat("TopSpeedStatF1Concept");
                                TopspeedStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("TopSpeedStatF1Concept", TopspeedStat.fillAmount);
                            }
                            F1ConceptEngCount++;
                            PlayerPrefs.SetInt("F1ConceptEngCount", F1ConceptEngCount);
                        }


                        //ActiveCar.maxspeed = ActiveCar.maxspeed + tier.car[activeCarIndex].engine[index].TopSpeed;
                        //ActiveCar.maxEngineTorque = ActiveCar.maxEngineTorque + tier.car[activeCarIndex].engine[index].Acceleration;
                        index = index + 1;
                        Debug.Log(index + "index");
                        PlayerPrefs.SetInt(ActiveCar.gameObject.name + "Engine", index);
                        Debug.Log("set value" + PlayerPrefs.GetInt(ActiveCar.gameObject.name + "Engine"));
                        PlayerPrefs.SetString("engineUpgrading", "false");
                        PlayerPrefs.SetString("PartUpgrading", "false");

                    }
                }
            }
        }
        //private void NOSCheckTime()
        //{
        //    if (PlayerPrefs.HasKey("nosUpgrading"))
        //    {
        //        string nosUpgrading = PlayerPrefs.GetString("nosUpgrading");
        //        if (nosUpgrading == "true")
        //        {
        //            float time = TimeMaster.instance.checkDate(ActiveCar.gameObject.name + "NOSTime");
        //            timeToUpgrade.text = time.ToString();
        //            int index = PlayerPrefs.GetInt(ActiveCar.gameObject.name + "NOS");
        //            if (time > tier.car[activeCarIndex].nos[index].Timer)
        //            {
        //                ActiveCar.maxEngineTorque = ActiveCar.maxEngineTorque + tier.car[activeCarIndex].nos[index].Acceleration;
        //                ActiveCar.NosLimitTimer = ActiveCar.NosLimitTimer + tier.car[activeCarIndex].nos[index + 1].NosTimer;
        //                PlayerPrefs.SetInt(ActiveCar.gameObject.name + "NOS", index);
        //                PlayerPrefs.SetString("nosUpgrading", "false");
        //                PlayerPrefs.SetString("PartUpgrading", "false");
        //            }
        //        }
        //    }
        //}
        private void TyreCheckTime()
        {
            if (PlayerPrefs.HasKey("tyreUpgrading"))
            {
                string tyreUpgrading = PlayerPrefs.GetString("tyreUpgrading");
                if (tyreUpgrading == "true")
                {
                    float time = TimeMaster.instance.checkDate(ActiveCar.gameObject.name + "TyreTime");
                    if (timeToUpgrade != null) timeToUpgrade.text = time.ToString();
                    int index = PlayerPrefs.GetInt(ActiveCar.gameObject.name + "Tyre");
                    TimeLeftBanner.text = "TIME LEFT IN UPGRADE: " + tier.car[activeCarIndex].engine[index].Timer + "secs".ToString();
                    if (time > tier.car[activeCarIndex].tyre[index].Timer)
                    {
                        TimeLeftBanner.text = "";
                        Debug.Log(tier.car[activeCarIndex].NWH_Wheels.Length);
                        for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                        {
                            tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient += tier.car[activeCarIndex].tyre[index].gripvalue;
                            tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient += tier.car[activeCarIndex].tyre[index].gripvalue;
                            tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient -= tier.car[activeCarIndex].tyre[index].slipvalue;
                            tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient -= tier.car[activeCarIndex].tyre[index].slipvalue;
                            tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength -= tier.car[activeCarIndex].tyre[index].springlength;
                            //ActiveCar.steerAngle = ActiveCar.steerAngle + tier.car[activeCarIndex].tyre[index].Handling;
                            //ActiveCar.DriftPower = ActiveCar.brakeTorque + tier.car[activeCarIndex].tyre[index].DriftPOwer;
                        }
                        index = index + 1;
                        PlayerPrefs.SetInt(ActiveCar.gameObject.name + "Tyre", index);
                        PlayerPrefs.SetString("tyreUpgrading", "false");
                        PlayerPrefs.SetString("PartUpgrading", "false");

                        //BacMono Tyres&Brakes Upgarde Path
                        if (activeCarIndex == 0)
                        {
                            if (tier.car[activeCarIndex].carNumber == 0 && PlayerPrefs.GetInt("BacTBCount") == 0)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("BacMonoSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[0].interactable = false;
                                tier.car[activeCarIndex].TBImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoTBBtn", tier.car[activeCarIndex].TBBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("BacMonoTBImg", tier.car[activeCarIndex].TBImg[0].enabled = false);
                                tier.car[activeCarIndex].TBBtn[1].interactable = true;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoTBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("BacMonoTBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBacMono");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBacMono", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBacMono");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatBacMono", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 0 && PlayerPrefs.GetInt("BacTBCount") == 1)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("BacMonoSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[1].interactable = false;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoTBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("BacMonoTBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);
                                tier.car[activeCarIndex].TBBtn[2].interactable = true;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoTBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("BacMonoTBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBacMono");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBacMono", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBacMono");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatBacMono", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 0 && PlayerPrefs.GetInt("BacTBCount") == 2)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("BacMonoSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BacMonoMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[2].interactable = false;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BacMonoTBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("BacMonoTBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBacMono");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBacMono", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBacMono");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatBacMono", HandlingStat.fillAmount);
                            }
                            BacTBCount++;
                            PlayerPrefs.SetInt("BacTBCount", BacTBCount);
                        }

                        //BatMobile Tyres&Brakes Upgarde Path
                        if (activeCarIndex == 1)
                        {
                            if (tier.car[activeCarIndex].carNumber == 1 && PlayerPrefs.GetInt("BatTBCount") == 0)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("BatMobileSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[0].interactable = false;
                                tier.car[activeCarIndex].TBImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileTBBtn", tier.car[activeCarIndex].TBBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("BatMobileTBImg", tier.car[activeCarIndex].TBImg[0].enabled = false);
                                tier.car[activeCarIndex].TBBtn[1].interactable = true;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileTBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("BatMobileTBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBatMobile");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBatMobile", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBatMobile");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatBatMobile", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 1 && PlayerPrefs.GetInt("BatTBCount") == 1)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("BatMobileSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[1].interactable = false;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileTBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("BatMobileTBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);
                                tier.car[activeCarIndex].TBBtn[2].interactable = true;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileTBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("BatMobileTBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBatMobile");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBatMobile", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBatMobile");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatBatMobile", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 1 && PlayerPrefs.GetInt("BatTBCount") == 2)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("BatMobileSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("BatMobileMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[2].interactable = false;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("BatMobileTBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("BatMobileTBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatBatMobile");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatBatMobile", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatBatMobile");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatBatMobile", HandlingStat.fillAmount);

                            }
                            BatTBCount++;
                            PlayerPrefs.SetInt("BatTBCount", BatTBCount);
                        }

                        //SCD2
                        if (activeCarIndex == 2)
                        {
                            if (tier.car[activeCarIndex].carNumber == 2 && PlayerPrefs.GetInt("SCD2TBCount") == 0)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("SCD2SideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SCD2ForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SCD2SideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SCD2ForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SCD2MaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[0].interactable = false;
                                tier.car[activeCarIndex].TBImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2TBBtn", tier.car[activeCarIndex].TBBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("SCD2TBImg", tier.car[activeCarIndex].TBImg[0].enabled = false);
                                tier.car[activeCarIndex].TBBtn[1].interactable = true;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2TBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("SCD2TBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSCD2");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSCD2", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSCD2");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatSCD2", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 2 && PlayerPrefs.GetInt("SCD2TBCount") == 1)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("SCD2SideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SCD2ForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SCD2SideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SCD2ForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SCD2MaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[1].interactable = false;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2TBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("SCD2TBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);
                                tier.car[activeCarIndex].TBBtn[2].interactable = true;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2TBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("SCD2TBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSCD2");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSCD2", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSCD2");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatSCD2", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 2 && PlayerPrefs.GetInt("SCD2TBCount") == 2)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("SCD2SideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SCD2ForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SCD2SideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SCD2ForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SCD2MaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[2].interactable = false;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SCD2TBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("SCD2TBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSCD2");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSCD2", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSCD2");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatSCD2", HandlingStat.fillAmount);
                            }
                            SCD2TBCount++;
                            PlayerPrefs.SetInt("SCD2TBCount", SCD2TBCount);
                        }

                        //Ramp car n1ew
                        if (activeCarIndex == 3)
                        {
                            if (tier.car[activeCarIndex].carNumber == 3 && PlayerPrefs.GetInt("RampTBCount") == 0)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("RampSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("RampForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("RampSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("RampForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("RampMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[0].interactable = false;
                                tier.car[activeCarIndex].TBImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("RampTBBtn", tier.car[activeCarIndex].TBBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("RampTBImg", tier.car[activeCarIndex].TBImg[0].enabled = false);
                                tier.car[activeCarIndex].TBBtn[1].interactable = true;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("RampTBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("RampTBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatRamp");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatRamp", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatRamp");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatRamp", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 3 && PlayerPrefs.GetInt("RampTBCount") == 1)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("RampSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("RampForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("RampSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("RampForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("RampMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[1].interactable = false;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("RampTBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("RampTBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);
                                tier.car[activeCarIndex].TBBtn[2].interactable = true;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("RampTBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("RampTBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatRamp");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatRamp", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatRamp");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatRamp", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 3 && PlayerPrefs.GetInt("RampTBCount") == 2)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("RampSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("RampForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("RampSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("RampForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("RampMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[2].interactable = false;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("RampTBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("RampTBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatRamp");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatRamp", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatRamp");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatRamp", HandlingStat.fillAmount);
                            }
                            RampTBCount++;
                            PlayerPrefs.SetInt("RampTBCount", RampTBCount);
                        }

                        //SDCF1
                        if (activeCarIndex == 4)
                        {
                            if (tier.car[activeCarIndex].carNumber == 4 && PlayerPrefs.GetInt("SDCF1TBCount") == 0)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("SDCF1SideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1ForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1SideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1ForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1MaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[0].interactable = false;
                                tier.car[activeCarIndex].TBImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1TBBtn", tier.car[activeCarIndex].TBBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("SDCF1TBImg", tier.car[activeCarIndex].TBImg[0].enabled = false);
                                tier.car[activeCarIndex].TBBtn[1].interactable = true;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1TBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("SDCF1TBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSDCF1");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSDCF1", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSDCF1");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatSDCF1", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 4 && PlayerPrefs.GetInt("SDCF1TBCount") == 1)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("SDCF1SideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1ForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1SideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1ForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1MaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[1].interactable = false;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1TBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("SDCF1TBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);
                                tier.car[activeCarIndex].TBBtn[2].interactable = true;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1TBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("SDCF1TBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSDCF1");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSDCF1", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSDCF1");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatSDCF1", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 4 && PlayerPrefs.GetInt("SDCF1TBCount") == 2)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("SDCF1SideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1ForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1SideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1ForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("SDCF1MaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[2].interactable = false;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("SDCF1TBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("SDCF1TBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatSDCF1");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatSDCF1", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatSDCF1");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatSDCF1", HandlingStat.fillAmount);
                            }
                            SDCF1TBCount++;
                            PlayerPrefs.SetInt("SDCF1TBCount", SDCF1TBCount);
                        }

                        //F1Concept
                        if (activeCarIndex == 5)
                        {
                            if (tier.car[activeCarIndex].carNumber == 5 && PlayerPrefs.GetInt("F1ConceptTBCount") == 0)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("F1ConceptSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[0].interactable = false;
                                tier.car[activeCarIndex].TBImg[0].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptTBBtn", tier.car[activeCarIndex].TBBtn[0].interactable = false);
                                PlayerPrefsExtra.SetBool("F1ConceptTBImg", tier.car[activeCarIndex].TBImg[0].enabled = false);
                                tier.car[activeCarIndex].TBBtn[1].interactable = true;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptTBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = true);
                                PlayerPrefsExtra.SetBool("F1ConceptTBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatF1Concept");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatF1Concept", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatF1Concept");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatF1Concept", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 5 && PlayerPrefs.GetInt("F1ConceptTBCount") == 1)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("F1ConceptSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[1].interactable = false;
                                tier.car[activeCarIndex].TBImg[1].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptTBBtn1", tier.car[activeCarIndex].TBBtn[1].interactable = false);
                                PlayerPrefsExtra.SetBool("F1ConceptTBImg1", tier.car[activeCarIndex].TBImg[1].enabled = false);
                                tier.car[activeCarIndex].TBBtn[2].interactable = true;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptTBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = true);
                                PlayerPrefsExtra.SetBool("F1ConceptTBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatF1Concept");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatF1Concept", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatF1Concept");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatF1Concept", HandlingStat.fillAmount);
                            }
                            if (tier.car[activeCarIndex].carNumber == 5 && PlayerPrefs.GetInt("F1ConceptTBCount") == 2)
                            {
                                for (int i = 0; i < tier.car[activeCarIndex].NWH_Wheels.Length; i++)
                                {
                                    PlayerPrefs.SetFloat("F1ConceptSideForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptForwardForce" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.forceCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptSideSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].sideFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptForwardSlip" + i, tier.car[activeCarIndex].NWH_Wheels[i].forwardFriction.slipCoefficient);
                                    PlayerPrefs.SetFloat("F1ConceptMaxLength" + i, tier.car[activeCarIndex].NWH_Wheels[i].spring.maxLength);
                                }
                                tier.car[activeCarIndex].TBBtn[2].interactable = false;
                                tier.car[activeCarIndex].TBImg[2].enabled = false;
                                PlayerPrefsExtra.SetBool("F1ConceptTBBtn2", tier.car[activeCarIndex].TBBtn[2].interactable = false);
                                PlayerPrefsExtra.SetBool("F1ConceptTBImg2", tier.car[activeCarIndex].TBImg[2].enabled = false);

                                AccelerationStat.fillAmount = PlayerPrefs.GetFloat("AccStatF1Concept");
                                AccelerationStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("AccStatF1Concept", AccelerationStat.fillAmount);

                                HandlingStat.fillAmount = PlayerPrefs.GetFloat("HandlingStatF1Concept");
                                HandlingStat.fillAmount += 0.05f;
                                PlayerPrefs.SetFloat("HandlingStatF1Concept", HandlingStat.fillAmount);
                            }
                            F1ConceptTBCount++;
                            PlayerPrefs.SetInt("F1ConceptTBCount", F1ConceptTBCount);
                        }


                    }
                }
            }
        }
        #endregion

        #region UI Changing
        public void Next()
        {
            if (indexcount < partModels.Length - 1)
            {
                indexcount++;
                if (indexcount > 0)
                {
                    previousPng.GetComponent<Image>().sprite = partModels[indexcount - 1];
                }
                midPng.GetComponent<Image>().sprite = partModels[indexcount];
                if (indexcount < partModels.Length - 1)
                {
                    nextPng.GetComponent<Image>().sprite = partModels[indexcount + 1];
                }
                string displayName = PlayerPrefs.GetString("UpgradingPart");
                if (timeToUpgrade != null) timeToUpgrade.text = tier.car[activeCarIndex].suspension[indexcount].Timer.ToString();
                if (prize != null) prize.text = tier.car[activeCarIndex].suspension[indexcount].Coins.ToString() + "$";
                if (LevelCheck(displayName) == true)
                {
                    SpecsValuesForDisplay(displayName);
                    upgradeButton.gameObject.SetActive(false);
                }
                else
                {
                    settingSpecsValuesForDisplay(displayName);
                    upgradeButton.gameObject.SetActive(true);
                }
            }
        }
        public void previous()
        {
            if (indexcount > 0)
            {
                indexcount--;
                if (indexcount > 0)
                {
                    previousPng.GetComponent<Image>().sprite = partModels[indexcount - 1];
                }
                midPng.GetComponent<Image>().sprite = partModels[indexcount];
                if (indexcount < partModels.Length - 1)
                {
                    nextPng.GetComponent<Image>().sprite = partModels[indexcount + 1];
                }
                string displayName = PlayerPrefs.GetString("UpgradingPart");
                if (timeToUpgrade != null) timeToUpgrade.text = tier.car[activeCarIndex].suspension[indexcount].Timer.ToString();
                if (prize != null) prize.text = tier.car[activeCarIndex].suspension[indexcount].Coins.ToString() + "$";
                if (LevelCheck(displayName) == true)
                {
                    SpecsValuesForDisplay(displayName);
                }
                else
                {
                    settingSpecsValuesForDisplay(displayName);
                }
            }
        }
        private void SpecsValuesForDisplay(string displayName)
        {
            Acceleration.fillAmount = ActiveCar.powertrain.engine.generatedPower / 4000;
            TopSpeed.fillAmount = ActiveCar.powertrain.engine.maxPower / 300;
            //NOS.fillAmount = ActiveCar.NosLimitTimer / 60;
            Break.fillAmount = ActiveCar.brakes.maxTorque / 10000;
        }
        private void settingSpecsValuesForDisplay(string displayname)
        {
            switch (displayname)
            {
                case "Suspension":
                    Acceleration.fillAmount = ActiveCar.powertrain.engine.generatedPower / 2000;
                    TopSpeed.fillAmount = ActiveCar.powertrain.engine.maxPower / 300;
                    //NOS.fillAmount = ActiveCar.NosLimitTimer / 20;
                    Break.fillAmount = ActiveCar.brakes.maxTorque / 4000;
                    AddingBreak.fillAmount = (ActiveCar.brakes.maxTorque + tier.car[activeCarIndex].suspension[indexcount].BrakeTorque) / 4000;
                    //ActiveCar.steerAngle = suspension[index].Handling;
                    break;
                case "Engine":
                    AddingAcceleration.fillAmount = (ActiveCar.powertrain.engine.generatedPower + tier.car[activeCarIndex].engine[indexcount].Acceleration) / 2000;
                    AddingTopSpeed.fillAmount = (ActiveCar.powertrain.engine.maxPower + tier.car[activeCarIndex].engine[indexcount].TopSpeed) / 300;
                    Acceleration.fillAmount = ActiveCar.powertrain.engine.generatedPower / 2000;
                    TopSpeed.fillAmount = ActiveCar.powertrain.engine.maxPower / 300;
                    // NOS.fillAmount = ActiveCar.NosLimitTimer / 20;
                    Break.fillAmount = ActiveCar.brakes.maxTorque / 4000;
                    break;
                //case "NOS":
                //    AddingAcceleration.fillAmount = (ActiveCar.maxEngineTorque + tier.car[activeCarIndex].engine[indexcount].Acceleration) / 2000;
                //    Acceleration.fillAmount = ActiveCar.maxEngineTorque / 2000;
                //    TopSpeed.fillAmount = ActiveCar.maxspeed / 300;
                //    AddingNOS.fillAmount = (ActiveCar.NosLimitTimer + tier.car[activeCarIndex].nos[indexcount].NosTimer) / 20;
                //    NOS.fillAmount = ActiveCar.NosLimitTimer / 20;
                //    Break.fillAmount = ActiveCar.brakeTorque / 4000;
                //    break;
                case "Tyre":
                    Acceleration.fillAmount = ActiveCar.powertrain.engine.generatedPower / 2000;
                    TopSpeed.fillAmount = ActiveCar.powertrain.engine.maxPower / 300;
                    //NOS.fillAmount = ActiveCar.NosLimitTimer / 20;
                    Break.fillAmount = ActiveCar.brakes.maxTorque / 4000;
                    AddingBreak.fillAmount = (ActiveCar.brakes.maxTorque + tier.car[activeCarIndex].suspension[indexcount].BrakeTorque) / 4000;
                    break;
            }
        }
        private bool LevelCheck(string displayName)
        {
            int index = PlayerPrefs.GetInt(ActiveCar.gameObject.name + displayName);
            string levelcheck = PlayerPrefs.GetString(ActiveCar.gameObject.name + displayName + index);
            if (levelcheck == "true")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        Tiers GetActiveCar()
        {
            carselection = FindObjectOfType<CarSelection>();
            int CarIndex = carselection.Selected;//PlayerPrefs.GetInt("selectedcar");
            //Debug.Log(CarIndex);
            Tiers tier;
            if (CarIndex >= 0 && CarIndex < 6)
            {
                //Debug.Log(tier1.car[CarIndex].NWH_Car);
                ActiveCar = tier1.car[CarIndex].NWH_Car;
                activeCarIndex = tier1.car[CarIndex].carNumber;
                //Debug.Log("CAR NUMBER: " +tier1.car[1].carNumber);
                tier = tier1;
                return tier;
            }
            //else if (CarIndex >= 8 && CarIndex < 16)
            //{
            //    ActiveCar = tier2.car[CarIndex - 8].NWH_Car;
            //    activeCarIndex = tier2.car[CarIndex].carNumber;
            //    tier = tier2;
            //    return tier;
            //}
            //else if (CarIndex >= 16 && CarIndex < 24)
            //{
            //    ActiveCar = tier3.car[CarIndex - 16].NWH_Car;
            //    activeCarIndex = tier3.car[CarIndex].carNumber;
            //    tier = tier3;
            //    return tier;
            //}
            else
            {
                return tier = null;
                //no car avaiable
            }
        }
        #endregion

        IEnumerator LowCash()
        {
            yield return new WaitForSeconds(1.5f);
            carselection.LowCashBanner.SetActive(false);
        }
        IEnumerator textPopUp()
        {
            yield return new WaitForSeconds(2.0f);
            if (notEnoughCashText != null) notEnoughCashText.text = "";
        }
    }
}
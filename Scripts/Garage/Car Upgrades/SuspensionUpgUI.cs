using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NWH.WheelController3D;

[System.Serializable]

public class SuspensionUpgUI 
{
    //All of these public fields will be filled at run time. So need to assign them manually.

    public WheelController[] wheelControllers = new WheelController[4];  // ref for all 4 wheels of car

    //Divider 1 Refs:
    [Header("Divider 1 Refs")]
    public TextMeshProUGUI carNameBig;
    public TextMeshProUGUI carNameSmall;
    public TextMeshProUGUI carPower;

    //Divider 2 Refs:
    [Header("Divider 2 Refs")]
    //Top Speed Fill Image Refs
    [Header("Top Speed UI Refs")]
    public Image tsBackImage;
    public Image tsFrontImage;
    public TextMeshProUGUI currentTS;
    public TextMeshProUGUI newTS;
    //Acceleration Fill Image Refs
    [Header("Acceleration UI Refs")]
    public Image acBackImage;
    public Image acFrontImage;
    public TextMeshProUGUI currentXL;
    public TextMeshProUGUI newXL;
    //Power Fill Image Refs
    [Header("Power UI Refs")]
    public Image pwBackImage;
    public Image pwFrontImage;
    public TextMeshProUGUI currentPW;
    public TextMeshProUGUI newPW;
    //Torque Fill Image Refs
    [Header("Torque UI Refs")]
    public Image tqBackImage;
    public Image tqFrontImage;
    public TextMeshProUGUI currentTQ;
    public TextMeshProUGUI newTQ;

    [Header("Divider 3 Refs")]
    public TextMeshProUGUI spName;

    public Button[] SPUpgBtns = new Button[3];         // engine upgragde buttons ref
    public TextMeshProUGUI[] SPPriceText = new TextMeshProUGUI[3];           // engine price text ref
    public Image[] SPLockImages = new Image[3];        // engine lock images ref
}

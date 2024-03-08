using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AirCarControl : MonoBehaviour
{
    public Transform AirFlipRefObj;
    public GameObject objToRotate;
    public Camera vehicleCam;
    public Camera inAirCam;
    private GameObject mobCtrls;
    private GameObject airCtrls;

    public float groundCheckDistance;
    public float upDownSpeed = 300f;
    public float leftRightSpeed = 150f;

    private float rotationX_back;
    private float rotationX_front;
    private float rotationZ_left;
    private float rotationZ_right;
    [HideInInspector] public int backFlipCount;
    [HideInInspector] public int frontFlipCount;
    [HideInInspector] public int leftRollCount;
    [HideInInspector] public int rightRollCount;
    
    [HideInInspector] public int objectiveBonus;    // This is a reference for reward scripts.

    private bool flipping;
    private bool frontFlip;
    private bool backFlip;
    private bool leftRoll;
    private bool rightRoll;
    public bool grounded;

    private Text backFlip_txt;
    private Text frontFlip_txt;
    private Text leftRoll_txt;
    private Text rightRoll_txt;

    BtnLeftDetection left;
    BtnRightDetection right;
    BtnUpDetection up;
    BrakeButtonScript down;

    string sceneName;

    public static AirCarControl INSTANCE;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
        }

        sceneName = SceneManager.GetActiveScene().name;
        btnInitialization();

        inAirCam.gameObject.SetActive(false);
    }

    private void Start()
    {
        // For Earth GamePlay 
        if (sceneName == "Gameplay")
        {
            mobCtrls = GameplayCar.INSTANCE.mobCtrls;
            airCtrls = GameplayCar.INSTANCE.airCtrls;
            backFlip_txt = UIManager.Instance.backFlip_txt;
            frontFlip_txt = UIManager.Instance.frontFlip_txt;
            leftRoll_txt = UIManager.Instance.leftRoll_txt;
            rightRoll_txt = UIManager.Instance.rightRoll_txt;
        }
        // For Moon GamePlay 
        if (sceneName == "MoonGameplay")
        {
            mobCtrls = MoonGameplayCar.INSTANCE.mobCtrls;
            airCtrls = MoonGameplayCar.INSTANCE.airCtrls;
            backFlip_txt = MoonUIManager.Instance.backFlip_txt;
            frontFlip_txt = MoonUIManager.Instance.frontFlip_txt;
            leftRoll_txt = MoonUIManager.Instance.leftRoll_txt;
            rightRoll_txt = MoonUIManager.Instance.rightRoll_txt;
        }
        // For Volcano GamePlay 
        if (sceneName == "volcano")
        {
            mobCtrls = VolcanoGameplayCar.INSTANCE.mobCtrls;
            airCtrls = VolcanoGameplayCar.INSTANCE.airCtrls;
            backFlip_txt = VolcanoUIManager.Instance.backFlip_txt;
            frontFlip_txt = VolcanoUIManager.Instance.frontFlip_txt;
            leftRoll_txt = VolcanoUIManager.Instance.leftRoll_txt;
            rightRoll_txt = VolcanoUIManager.Instance.rightRoll_txt;
        }
        // For Mars GamePlay 
        if (sceneName == "GameplayMars")
        {
            mobCtrls = MarsGameplayCar.INSTANCE.mobCtrls;
            airCtrls = MarsGameplayCar.INSTANCE.airCtrls;
            backFlip_txt = MarsUIManager.Instance.backFlip_txt;
            frontFlip_txt = MarsUIManager.Instance.frontFlip_txt;
            leftRoll_txt = MarsUIManager.Instance.leftRoll_txt;
            rightRoll_txt = MarsUIManager.Instance.rightRoll_txt;
        }
        // For Fantasy map GamePlay
        if (sceneName == "FantasyScene")
        {
            mobCtrls = FantasyGameplayCar.INSTANCE.mobCtrls;
            airCtrls = FantasyGameplayCar.INSTANCE.airCtrls;
            backFlip_txt = FantasyUIManager.Instance.backFlip_txt;
            frontFlip_txt = FantasyUIManager.Instance.frontFlip_txt;
            leftRoll_txt = FantasyUIManager.Instance.leftRoll_txt;
            rightRoll_txt = FantasyUIManager.Instance.rightRoll_txt;
        }
        // For Height map GamePlay 
        if (sceneName == "HeightMaps")
        {
            mobCtrls = HeightGameplayCar.INSTANCE.mobCtrls;
            airCtrls = HeightGameplayCar.INSTANCE.airCtrls;
            backFlip_txt = HeightUIManager.Instance.backFlip_txt;
            frontFlip_txt = HeightUIManager.Instance.frontFlip_txt;
            leftRoll_txt = HeightUIManager.Instance.leftRoll_txt;
            rightRoll_txt = HeightUIManager.Instance.rightRoll_txt;
        }

        backFlip_txt.gameObject.SetActive(false);
        frontFlip_txt.gameObject.SetActive(false);
        leftRoll_txt.gameObject.SetActive(false);
        rightRoll_txt.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (grounded == true)
        {
            inAirCam.transform.SetParent(this.gameObject.transform);
            mobCtrls.SetActive(true);
            airCtrls.SetActive(false);

            rotationX_back = 0;
            backFlipCount = 0;

            rotationX_front = 0;
            frontFlipCount = 0;
        }
        if (grounded == false)
        {
            inAirCam.transform.SetParent(null);
            airCtrls.SetActive(true);
            mobCtrls.SetActive(false);

            // back flip
            BackFlip();

            // front flip
            FrontFlip();

            // Rotate left
            LeftSideRoll();

            //Rotate Right
            RightSideRoll();
        }
    }

    private void BackFlip()
    {
        if (up.upBtn == true)
        {
            transform.Rotate(Vector3.left * upDownSpeed * Time.deltaTime);

            float newXRotation_2 = rotationX_back - (upDownSpeed * Time.deltaTime);
            Quaternion rotation_reset = Quaternion.Euler(newXRotation_2, 0, 0);
            AirFlipRefObj.rotation = rotation_reset;
            rotationX_back = newXRotation_2;

            if (rotationX_back <= -340 && rotationX_back >= -360f && backFlipCount == 0)
            {
                backFlip = true;
            }
            if (rotationX_back <= -700 && rotationX_back >= -720f && backFlipCount == 1)
            {
                backFlip = true;
            }
            if (rotationX_back <= -1060 && rotationX_back >= -1080f && backFlipCount == 2)
            {
                backFlip = true;
            }
            if (rotationX_back <= -1420 && rotationX_back >= -1440f && backFlipCount == 3)
            {
                backFlip = true;
            }
        }
        if (backFlip == true)
        {
            backFlipCount++;
            StartCoroutine(ShowBackFlipText());
            backFlip = false;
        }
    }

    private void FrontFlip()
    {
        if (down.BrakeCheck == true)
        {
            transform.Rotate(Vector3.right * upDownSpeed * Time.deltaTime);

            float newXRotation_2 = rotationX_front + (upDownSpeed * Time.deltaTime);
            Quaternion rotation_reset = Quaternion.Euler(newXRotation_2, 0, 0);
            AirFlipRefObj.rotation = rotation_reset;
            rotationX_front = newXRotation_2;

            if (rotationX_front >= 340 && rotationX_front <= 360f && frontFlipCount == 0)
            {
                frontFlip = true;
            }
            if (rotationX_front >= 700 && rotationX_front <= 720f && frontFlipCount == 1)
            {
                frontFlip = true;
            }
            if (rotationX_front >= 1060 && rotationX_front <= 1080f && frontFlipCount == 2)
            {
                frontFlip = true;
            }
            if (rotationX_front >= 1420 && rotationX_front <= 1440f && frontFlipCount == 3)
            {
                frontFlip = true;
            }
        }
        if (frontFlip == true)
        {
            frontFlipCount++;
            StartCoroutine(ShowFrontFlipText());
            frontFlip = false;
        }
    }

    private void LeftSideRoll()
    {
        if (left.LeftBtn == true)
        {
            transform.Rotate(Vector3.forward * leftRightSpeed * Time.deltaTime);

            float newZRotation_2 = rotationZ_left + (upDownSpeed * Time.deltaTime);
            Quaternion rotation_reset = Quaternion.Euler(0, 0, newZRotation_2);
            AirFlipRefObj.rotation = rotation_reset;
            rotationZ_left = newZRotation_2;

            if (rotationZ_left >= 350 && rotationZ_left <= 360f && leftRollCount == 0)
            {
                leftRoll = true;
            }
            if (rotationZ_left >= 710 && rotationZ_left <= 720f && leftRollCount == 1)
            {
                leftRoll = true;
            }
            if (rotationZ_left >= 1070 && rotationZ_left <= 1080f && leftRollCount == 2)
            {
                leftRoll = true;
            }
            if (rotationZ_left >= 1430 && rotationZ_left <= 1440f && leftRollCount == 3)
            {
                leftRoll = true;
            }
        }
        if (leftRoll == true)
        {
            leftRollCount++;
            StartCoroutine(ShowLeftRollText());
            leftRoll = false;
        }
    }

    private void RightSideRoll()
    {
        if (right.RightBtn == true)
        {
            transform.Rotate(Vector3.back * leftRightSpeed * Time.deltaTime);

            float newZRotation_2 = rotationZ_right + (upDownSpeed * Time.deltaTime);
            Quaternion rotation_reset = Quaternion.Euler(0, 0, newZRotation_2);
            AirFlipRefObj.rotation = rotation_reset;
            rotationZ_right = newZRotation_2;

            if (rotationZ_right >= 350 && rotationZ_right <= 360f && rightRollCount == 0)
            {
                rightRoll = true;
            }
            if (rotationZ_right >= 710 && rotationZ_right <= 720f && rightRollCount == 1)
            {
                rightRoll = true;
            }
            if (rotationZ_right >= 1070 && rotationZ_right <= 1080f && rightRollCount == 2)
            {
                rightRoll = true;
            }
            if (rotationZ_right >= 1430 && rotationZ_right <= 1440f && rightRollCount == 3)
            {
                rightRoll = true;
            }
        }
        if (rightRoll == true)
        {
            rightRollCount++;
            StartCoroutine(ShowRightRollText());
            rightRoll = false;
        }
    }

    private void FixedUpdate()
    {
        grounded = Physics.CheckSphere(transform.position, groundCheckDistance);
    }
    void btnInitialization()
    {
        left = FindObjectOfType<BtnLeftDetection>();
        right = FindObjectOfType<BtnRightDetection>();
        up = FindObjectOfType<BtnUpDetection>();
        down = FindObjectOfType<BrakeButtonScript>();
    }

    IEnumerator ShowFrontFlipText()
    {
        frontFlip_txt.gameObject.SetActive(true);
        frontFlip_txt.text = "FRONT FLIP " + frontFlipCount + "X";
        yield return new WaitForSeconds(1f);
        frontFlip_txt.gameObject.SetActive(false);
    }
    IEnumerator ShowBackFlipText()
    {
        backFlip_txt.gameObject.SetActive(true);
        backFlip_txt.text = "BACK FLIP " + backFlipCount + "X";
        yield return new WaitForSeconds(1f);
        backFlip_txt.gameObject.SetActive(false);
    }

    IEnumerator ShowLeftRollText()
    {
        leftRoll_txt.gameObject.SetActive(true);
        leftRoll_txt.text = "LEFT ROLL " + leftRollCount + "X";
        yield return new WaitForSeconds(1f);
        leftRoll_txt.gameObject.SetActive(false);
    }

    IEnumerator ShowRightRollText()
    {
        rightRoll_txt.gameObject.SetActive(true);
        rightRoll_txt.text = "RIGHT ROLL " + rightRollCount + "X";
        yield return new WaitForSeconds(1f);
        rightRoll_txt.gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, groundCheckDistance);
    }
}
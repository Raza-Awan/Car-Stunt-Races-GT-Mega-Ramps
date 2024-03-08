using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrakeButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public bool BrakeCheck;
    // Start is called before the first frame update
    void Start()
    {
        BrakeCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        BrakeCheck = true;
        //Debug.Log("TRUE");
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        BrakeCheck = false;
        //Debug.Log("FALSE");
    }
}

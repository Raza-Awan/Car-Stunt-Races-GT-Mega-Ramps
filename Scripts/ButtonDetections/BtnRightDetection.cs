using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnRightDetection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public bool RightBtn;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        RightBtn = true;
    }
    public void OnPointerUp(PointerEventData eventData2)
    {
        RightBtn = false;
    }

}

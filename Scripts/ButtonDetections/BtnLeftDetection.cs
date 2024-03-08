using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnLeftDetection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public bool LeftBtn;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        LeftBtn = true;
    }
    public void OnPointerUp(PointerEventData eventData2)
    {
        LeftBtn = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnUpDetection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public bool upBtn;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        upBtn = true;
    }
    public void OnPointerUp(PointerEventData eventData2)
    {
        upBtn = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionBtnHolder : MonoBehaviour
{
    GarageCarSelection carSelection;

    public Button[] carSelectionBtns;

    // Start is called before the first frame update
    void Start()
    {
        carSelection = GetComponent<GarageCarSelection>();

        for (int i = 0; i < carSelectionBtns.Length; i++)
        {
            int index = i;
            carSelectionBtns[i].onClick.AddListener(() => OnClickButton(index));
        }
    }

    public void OnClickButton(int index)
    {
        carSelection.SetCurrentCarIndex(index);
    }
}

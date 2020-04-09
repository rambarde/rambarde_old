using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchClientEquipment : MonoBehaviour
{
    public GameObject clientSelection;
    public GameObject equipmentSelection;


    // Start is called before the first frame update
    void Start()
    {
        clientSelection.SetActive(true);
        equipmentSelection.SetActive(false);
    }

    public void GoToEquipmentSelection()
    {
        clientSelection.SetActive(false);
        equipmentSelection.SetActive(true);
    }
}

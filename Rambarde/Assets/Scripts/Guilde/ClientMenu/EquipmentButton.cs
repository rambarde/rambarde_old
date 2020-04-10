using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentButton : MonoBehaviour
{
    public Characters.Equipment equipment;

    void OnEnable()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = equipment.sprite;
        transform.GetChild(1).GetComponent<Text>().text = equipment.equipmentName;

        if(equipment.type == EquipType.WEAPON)
        {
            transform.GetChild(2).GetComponent<Text>().text = "Arme";
        }
        else
        {
            transform.GetChild(2).GetComponent<Text>().text = "Armure";
        }

        transform.GetChild(3).GetComponent<Text>().text = equipment.allowedType.ToString();  
        transform.GetChild(4).GetComponent<Text>().text = (equipment.numberOwned - equipment.numberEquiped).ToString();
    }

    public void UpdateNumber()
    {
        transform.GetChild(4).GetComponent<Text>().text = (equipment.numberOwned - equipment.numberEquiped).ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentShop : MonoBehaviour
{
    public Characters.Equipment[] equipments;
    public GameObject[] buttons;

    public GameObject infoPanel;

    Characters.Equipment selectedEquipment;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }

        for (int i = 0; i<equipments.Length; i++)
        {
            if (equipments[i].type.ToString() == this.tag && equipments[i].numberOwned<3) //to do later: add condition to display only unlocked items
            {
                buttons[i].SetActive(true);
                buttons[i].transform.GetChild(0).GetComponent<Image>().sprite = equipments[i].sprite;
                buttons[i].transform.GetChild(1).GetComponent<Text>().text = equipments[i].equipmentName;
                buttons[i].transform.GetChild(2).GetComponent<Text>().text = equipments[i].price + "G";
            }
        }
    }

    public void DisplayEquipInfo(Characters.Equipment equipment)
    {
        infoPanel.transform.GetChild(0).GetComponent<Image>().sprite = equipment.sprite;
        infoPanel.transform.GetChild(1).GetComponent<Text>().text = equipment.equipmentName;
        infoPanel.transform.GetChild(2).GetComponent<Text>().text = "Prix : " + equipment.price + "G";
        infoPanel.transform.GetChild(3).GetComponent<Text>().text = "Peut être utilisé par : " + equipment.allowedType;

        // Display information about the equipment
        infoPanel.transform.GetChild(4).GetComponent<Text>().text = "ATQ +" + equipment.atqMod;
        infoPanel.transform.GetChild(5).GetComponent<Text>().text = "PREC +" + equipment.precMod*100 + "%";
        infoPanel.transform.GetChild(6).GetComponent<Text>().text = "CRIT +" + equipment.critMod*100 + "%";
        infoPanel.transform.GetChild(7).GetComponent<Text>().text = "END +" + equipment.endMod;
        infoPanel.transform.GetChild(8).GetComponent<Text>().text = "PROT +" + equipment.protMod*100 + "%";

        // Display the number of similar equipment already owned
        infoPanel.transform.GetChild(9).GetComponent<Text>().text = "Already owned : " + equipment.numberOwned;

        selectedEquipment = equipment;
    }

    public void PurchaseEquipment()
    {
        GameObject goldManager = GameObject.FindGameObjectWithTag("GoldLabel");

        //test if enough gold to purchase equipment
        if (goldManager.GetComponent<GoldValue>().HasEnoughGold(selectedEquipment.price) && selectedEquipment.numberOwned <3) 
        {
            // decrement gold value
            goldManager.GetComponent<GoldValue>().Pay(selectedEquipment.price);

            // if buyinng this equipment for the first time (not already owned), add it to the list of equipment owned
            if(selectedEquipment.numberOwned == 0)
            {
                this.GetComponentInParent<BoutiqueManager>().equipmentOwned.Add(selectedEquipment);
            }

            // increment the number of owned equiment
            selectedEquipment.numberOwned ++;

            // if already 3 similar equipment owned: buying a new one is impossible
            if(selectedEquipment.numberOwned >=3)
            {
                Start();
            }

            // Display the number of similar equipment already owned
            infoPanel.transform.GetChild(9).GetComponent<Text>().text = "Already owned : " + selectedEquipment.numberOwned;
        }
        else
        {
            goldManager.GetComponent<GoldValue>().DisplayNoGoldMessage();
        }
    }
}

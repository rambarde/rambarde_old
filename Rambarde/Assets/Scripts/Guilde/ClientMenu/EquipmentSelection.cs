using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelection : MonoBehaviour
{
    public GameObject boutique;
    List<Client> clients;
    List<Characters.Equipment> equipmentOwned;
    public GameObject[] equipButtons;
    public GameObject clientPanel;
    public Button[] switchClientButtons;

    int selectedClientID = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        // CLIENTS
        clients = GameObject.Find("GuildeMenu").GetComponent<GuildeManagerBehaviour>().clients;

        // Weapons and Armor
        equipmentOwned = boutique.GetComponent<BoutiqueManager>().equipmentOwned;
        for(int i = 0; i<equipButtons.Length; i++)
        {
            if(i<equipmentOwned.Count)
            {
                equipButtons[i].GetComponent<EquipmentButton>().equipment = equipmentOwned[i];
                equipButtons[i].SetActive(true);
            }
            else
            {
                equipButtons[i].SetActive(false);
            }
        }

        DisplayClientInfo();
        ManageButtons();
    }

    void DisplayClientInfo()
    {
        clientPanel.transform.GetChild(0).GetComponent<Text>().text = clients[selectedClientID].Name;
        clientPanel.transform.GetChild(1).GetComponent<Image>().sprite = clients[selectedClientID].Character.clientImage;

        UpdateStatsAndEquip();
    }

    void UpdateStatsAndEquip()
    {
        //clients[selectedClientID].UpdateStats();

        //// stats values
        //clientPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "ATQ = " + clients[selectedClientID].currentStats.atq;    // ATQ
        //clientPanel.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "PREC = " + clients[selectedClientID].currentStats.prec;  // PREC
        //clientPanel.transform.GetChild(2).GetChild(2).GetComponent<Text>().text = "CRIT = " + clients[selectedClientID].currentStats.crit;  // CRIT
        //clientPanel.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = "END = " + clients[selectedClientID].currentStats.maxHp;  // END
        //clientPanel.transform.GetChild(2).GetChild(4).GetComponent<Text>().text = "PROT = " + clients[selectedClientID].currentStats.prot;  // PROT

        // Weapon and armor icons
        clientPanel.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = clients[selectedClientID].equipment[0].sprite;         //weapon
        clientPanel.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = clients[selectedClientID].equipment[1].sprite;         //armor
    }

    public void Equip(int buttonId)
    {
        Characters.Equipment equipment = equipButtons[buttonId].GetComponent<EquipmentButton>().equipment;
        Characters.Equipment currentEquipment;

        // test if client type allowed to equip this item
        if (equipment.allowedType == clients[selectedClientID].Character.charType && (equipment.numberOwned-equipment.numberEquiped) > 0)
        {
            // test equipment type
            if (equipment.type == EquipType.WEAPON)
            {
                currentEquipment = clients[selectedClientID].equipment[0];
                clients[selectedClientID].equipment[0] = equipment; // WEAPON
            }
            else
            {
                currentEquipment = clients[selectedClientID].equipment[0];
                clients[selectedClientID].equipment[1] = equipment; // ARMOR
            }

            currentEquipment.numberEquiped--;
            equipment.numberEquiped++;

            for (int i = 0; i < equipButtons.Length; i++)
            {
                if (i < equipmentOwned.Count)
                {
                    equipButtons[i].GetComponent<EquipmentButton>().UpdateNumber();
                }
            }
            UpdateStatsAndEquip();
        }
    }

    public void ChangeClient(bool increment)
    {
        if (increment)
        {
            selectedClientID++;
        }
        else
        {
            selectedClientID--;
        }
        UpdateStatsAndEquip();
        ManageButtons();
    }

    void ManageButtons()
    {
        switch(selectedClientID)
        {
            case (0): switchClientButtons[0].interactable = false; break;
            case (2): switchClientButtons[1].interactable = false; break;
            default: switchClientButtons[0].interactable = true; switchClientButtons[1].interactable = true; break;
        }
    }
}

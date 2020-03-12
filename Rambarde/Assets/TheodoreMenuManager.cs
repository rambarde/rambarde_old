using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheodoreMenuManager : MonoBehaviour
{ 
    [SerializeField]
    private int _selectedSkill;
    public int SelectedSkill{
        get { return _selectedSkill; }
        set {
            _selectedSkill = value;
            if (value >= 12)
                doneButton.GetComponent<Button>().interactable = true;
            else
                doneButton.GetComponent<Button>().interactable = false;
        }
    }

    protected GameObject doneButton;

    void Start()
    {
        doneButton = transform.GetChild(transform.childCount - 1).gameObject;
        doneButton.GetComponent<Button>().interactable = false;
    }
    
    public void resetSelectedSkill(int nSkills){ SelectedSkill -= nSkills; }

    public void resetTheodoreMenu()
    {
        SlotBehaviour[] slots = transform.GetComponentsInChildren<SlotBehaviour>();
        Counter[] counters = transform.GetComponentsInChildren<Counter>();

        foreach (SlotBehaviour slot in slots)
            slot.resetSlotted();
        foreach (Counter counter in counters)
            counter.resetCounter();
        //pop a 'r u sure ? (you'll lose unsaved change)' window
    }

    public void chooseSkill()
    {
        //save the skills inside the gameManager (+ open a 'r u sure u want those skills' window ?)
        //not sure if we need to do this function here tho, might be a good idea to do it in the gameManager itself and call it OnClick()
    }
}

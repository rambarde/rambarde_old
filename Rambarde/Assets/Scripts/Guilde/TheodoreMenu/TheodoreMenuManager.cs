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

    private SlotBehaviour[] slots;
    private Counter[] counters;
    protected GameObject doneButton;

    void Start()
    {
        doneButton = transform.GetChild(transform.childCount - 1).gameObject;
        doneButton.GetComponent<Button>().interactable = false;
        slots = transform.GetComponentsInChildren<SlotBehaviour>();
        counters = transform.GetComponentsInChildren<Counter>();
    }
    
    public void resetSelectedSkill(int nSkills){ SelectedSkill -= nSkills; }

    public void resetTheodoreMenu()
    {
        //SlotBehaviour[] slots = transform.GetComponentsInChildren<SlotBehaviour>();
        //Counter[] counters = transform.GetComponentsInChildren<Counter>();

        foreach (SlotBehaviour slot in slots)
            slot.resetSlotted();
        foreach (Counter counter in counters)
            counter.resetCounter();
        //pop a 'r u sure ? (you'll lose unsaved change)' window
    }

    public void selectSkills()
    {
        Melodies.Melody[] innateMelodies = new Melodies.Melody[4];
        Bard.Instrument[] instruments = new Bard.Instrument[2];
        int nMelody = 0;
        int nInstrument = 0;
        for(int i = 0; i < slots.Length; i++) 
        {
            if (slots[i].InnateSkillSlot)
            {
                innateMelodies[nMelody] = slots[i].transform.GetComponentInChildren<MelodyBehaviour>().melody;
                nMelody++;
            }

            if (slots[i].InstrumentSlot)
            {
                instruments[nInstrument] = slots[i].transform.GetComponentInChildren<InstrumentBehaviour>().instrument;
                nInstrument++;
            }
        }

        transform.GetComponentInParent<GuildeManagerBehaviour>().SetTheodore(innateMelodies, instruments);
        //save the skills inside the gameManager (+ open a 'r u sure u want those skills' window ?)
        //not sure if we need to do this function here tho, might be a good idea to do it in the gameManager itself and call it OnClick()
    }
}

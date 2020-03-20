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

    private void Awake()
    {
        doneButton = transform.GetChild(transform.childCount - 1).gameObject;
        doneButton.GetComponent<Button>().interactable = false;
        slots = transform.GetComponentsInChildren<SlotBehaviour>();
        counters = transform.GetComponentsInChildren<Counter>();
    }

    void Start()
    {
        GameObject.Find("Instruments Panel").SetActive(false);
    }
    
    public void resetSelectedSkill(int nSkills){ SelectedSkill -= nSkills; }

    public void resetTheodoreMenu()
    {
        if (transform.GetComponentInParent<GuildeManagerBehaviour>().menuValid[2] && SelectedSkill >= 12)
            return;

        foreach (SlotBehaviour slot in slots)
            slot.resetSlotted();
        foreach (Counter counter in counters)
            counter.resetCounter();
        
        transform.GetComponentInParent<GuildeManagerBehaviour>().resetTheodore();
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
    }
}

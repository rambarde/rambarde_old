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

        //TO DO//
        //if (GameManager.instruments != null)
        //{
        //    int nInnate = 0;
        //    int nInstruments = 1; //instrument 0 = base instrument
        //    for (int i = 0; i < slots.Length; i++)
        //    {
        //        if (slots[i].InnateSkillSlot)
        //        {
        //            slots[i].transform.GetChild(0).GetComponent<MelodyBehaviour>().melody = GameManager.instruments[0].melodies[nInnate];
        //            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instruments[0].melodies[nInnate].sprite;
        //            slots[i].transform.GetChild(0).GetComponent<Image>().color = GameManager.instruments[0].melodies[nInnate].color;
        //            slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
        //            slots[i].Slotted = true;
        //            nInnate++;
        //        }

        //        if (slots[i].InstrumentSlot)
        //        {
        //            slots[i].transform.GetChild(0).GetComponent<InstrumentBehaviour>().instrument = GameManager.instruments[nInstruments];
        //            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instruments[nInstruments].sprite;
        //            slots[i].transform.GetChild(0).GetComponent<Image>().color = GameManager.instruments[nInstruments].color;
        //            slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
        //            slots[i].Slotted = true;
        //            nInstruments++;
        //        }
        //    }
        //}
        //TO DO//
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
        List<Bard.Instrument> instruments = new List<Bard.Instrument>();
        int nMelody = 0;
        for(int i = 0; i < slots.Length; i++) 
        {
            if (slots[i].InnateSkillSlot)
            {
                innateMelodies[nMelody] = slots[i].transform.GetComponentInChildren<MelodyBehaviour>().melody;
                nMelody++;
            }

            if (slots[i].InstrumentSlot)
                instruments.Add(slots[i].transform.GetComponentInChildren<InstrumentBehaviour>().instrument);
        }
        transform.GetComponentInParent<GuildeManagerBehaviour>().SetTheodore(innateMelodies, instruments);
    }
}

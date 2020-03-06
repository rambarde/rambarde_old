using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotBehaviour : 
    MonoBehaviour
{
    public int skillTier;
    public bool innateSkillSlot;
    public bool instrumentSkillSlot;
    public bool instrumentSlot;

    private GameObject slotted;
    private GameObject canvas;
    private Vector2 originalImageSize;

    public bool skillSlotted;

    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas").gameObject;
        slotted = transform.GetChild(0).gameObject;
        skillSlotted = false;
    }

    public bool isSlotted() { return skillSlotted; }
    public void setSlotted(bool isSlotted) { skillSlotted = isSlotted; }

    public void resetSlotSkill()
    {
        setSlotted(false);
        GameObject slottedSkill = transform.GetChild(0).gameObject;
        slottedSkill.GetComponent<SkillUI>().unEquip(skillTier);
        slottedSkill.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        slottedSkill.GetComponent<Image>().sprite = null;
        slottedSkill.GetComponent<Image>().enabled = false;
    }

    public void resetSlotInstrument()
    {
        setSlotted(false);
        GameObject slottedInstrument = transform.GetChild(0).gameObject;
        slottedInstrument.GetComponent<InstrumentUI>().unEquip();
        slottedInstrument.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        slottedInstrument.GetComponent<Image>().sprite = null;
        slottedInstrument.GetComponent<Image>().enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotBehaviour : 
    MonoBehaviour
{
    [SerializeField]
    private bool _innateSkillSlot;
    public bool InnateSkillSlot { get { return _innateSkillSlot; } set { _innateSkillSlot = value; } }
    [SerializeField]
    private bool _instrumentSkillSlot;
    public bool InstrumentSkillSlot { get { return _instrumentSkillSlot; } set { _instrumentSkillSlot = value; } }
    [SerializeField]
    private bool _instrumentSlot;
    public bool InstrumentSlot { get { return _instrumentSlot; } set { _instrumentSlot = value; } }
    public bool Slotted { get; set; }

    void Start()
    {
        Slotted = false;
    }

    public void resetSlotted()
    {
        if (Slotted)
        {
            Slotted = false;
            GameObject slotted = transform.GetChild(0).gameObject;
            slotted.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slotted.GetComponent<Image>().sprite = null;
            slotted.GetComponent<Image>().enabled = false;

            if (slotted.GetComponent<InstrumentBehaviour>() != null)
            {
                slotted.GetComponent<InstrumentBehaviour>().instrument = null;
                slotted.GetComponent<InstrumentBehaviour>().IsClickable = false;
            }

            if (slotted.GetComponent<MelodyBehaviour>() != null)
            {
                slotted.GetComponent<MelodyBehaviour>().melody = null;
                slotted.GetComponent<MelodyBehaviour>().IsClickable = false;
                transform.GetComponentInParent<TheodoreMenuManager>().resetSelectedSkill(1);
            }
        }
    }
}

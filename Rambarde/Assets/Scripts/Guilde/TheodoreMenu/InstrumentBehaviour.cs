using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InstrumentBehaviour :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    private Tooltip instrumentTooltip;
    private RectTransform canvasRectTransform;
    private RectTransform tooltipRectTransform;
    public Bard.Instrument instrument;

    private GameObject[] instrumentSlots;
    private GameObject[] instrumentSkillSlots;
    private GameObject[] melodiesInstrument;
    GameObject slot;
    GameObject counter;
    public bool isClickable;

    void Awake()
    {
        if (isClickable)
        {
            GetComponent<Image>().color = instrument.color;
            GetComponent<Image>().sprite = instrument.sprite;

            GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
            instrumentSlots = new GameObject[2];
            instrumentSkillSlots = new GameObject[8];
            int j = 0;
            int k = 0;

            for (int i = 0; i < slots.Length; i++)
            {
                GameObject slot = slots[i];
                if (slot.GetComponent<SlotBehaviour>() != null && slot.GetComponent<SlotBehaviour>().instrumentSlot)
                {
                    instrumentSlots[j] = slot;
                    j += 1;
                }
                if (slot.GetComponent<SlotBehaviour>() != null && slot.GetComponent<SlotBehaviour>().instrumentSkillSlot)
                {
                    instrumentSkillSlots[k] = slot;
                    k += 1;
                }
            }
            melodiesInstrument = new GameObject[4];

            for (int i = 0; i < transform.parent.GetChild(1).childCount; i++)
                melodiesInstrument[i] = transform.parent.GetChild(1).GetChild(i).gameObject;
        }
    }

    void Start()
    {
        if (GameObject.FindWithTag("Tooltip") != null)
        {
            instrumentTooltip = GameObject.FindWithTag("Tooltip").GetComponent<Tooltip>();
            tooltipRectTransform = GameObject.FindWithTag("Tooltip").GetComponent<RectTransform>() as RectTransform;
        }
        canvasRectTransform = GameObject.FindWithTag("TheodoreMenu").GetComponent<RectTransform>() as RectTransform;

        if (isClickable)
            for (int i= 0;i < melodiesInstrument.Length; i++)
            {
                Melodies.Melody melody = instrument.melodies[i];
                melodiesInstrument[i].GetComponent<MelodyBehaviour>().melody = melody;
                Color melodyColor = melody.color;
                melodyColor.r = 1;
                melodyColor.g = 1;
                melodyColor.b = 1;
                melodyColor.a = 1;

                melodiesInstrument[i].GetComponent<Image>().sprite = melody.sprite;
                melodiesInstrument[i].GetComponent<Image>().color = melodyColor;
                melodiesInstrument[i].GetComponent<Image>().enabled = true;
            }

    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (instrumentTooltip != null)
        {
            instrumentTooltip.setObject(gameObject);
            instrumentTooltip.Activate(true);

            if (canvasRectTransform == null)
                return;

            Vector3[] worldCorners = new Vector3[4];
            GetComponent<RectTransform>().GetWorldCorners(worldCorners);

            Vector2 localRectTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform,
                                                                    new Vector2((worldCorners[3].x + worldCorners[0].x) / 2.0f, worldCorners[0].y),
                                                                    pointerEventData.enterEventCamera,
                                                                    out localRectTransform);

            Vector2 newTooltipPos = new Vector2(localRectTransform.x, localRectTransform.y);
            if (localRectTransform.y - tooltipRectTransform.sizeDelta.y < -canvasRectTransform.sizeDelta.y / 2)
                newTooltipPos.y += tooltipRectTransform.sizeDelta.y + GetComponent<RectTransform>().sizeDelta.y;

            if (localRectTransform.x - tooltipRectTransform.sizeDelta.x / 2 < -canvasRectTransform.sizeDelta.x / 2)
                newTooltipPos.x = tooltipRectTransform.sizeDelta.x / 2 - canvasRectTransform.sizeDelta.x / 2;

            if (localRectTransform.x + tooltipRectTransform.sizeDelta.x / 2 > canvasRectTransform.sizeDelta.x / 2)
                newTooltipPos.x = canvasRectTransform.sizeDelta.x / 2 - tooltipRectTransform.sizeDelta.x / 2;

            tooltipRectTransform.localPosition = newTooltipPos;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (instrumentTooltip != null)
            instrumentTooltip.Activate(false);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!isClickable)
            return;

        counter = GameObject.Find("Instruments counter");

        if (findSlot(instrumentSlots) == -1)
            return;

        slot = instrumentSlots[findSlot(instrumentSlots)];

        GameObject slottedSkill = slot.transform.GetChild(0).gameObject;
        slottedSkill.GetComponent<InstrumentBehaviour>().instrument = instrument;
        slottedSkill.GetComponent<Image>().color = GetComponent<Image>().color;
        slottedSkill.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        slottedSkill.GetComponent<Image>().enabled = true;

        slot.GetComponent<SlotBehaviour>().setSlotted(true);

        counter.GetComponent<Counter>().increment();

        foreach (Melodies.Melody melody in instrument.melodies)
            displayMelody(instrumentSkillSlots, melody);

        isClickable = false; 
        GameObject.Find("Reset Instruments").GetComponent<Button>().onClick.AddListener(buttonReset);
    }

    void buttonReset() { isClickable = true; }
    public void setClickable(bool m_bool) { this.isClickable = m_bool; }

    int findSlot(GameObject[] slotList)
    {
        for (int i = 0; i < slotList.Length; i++)
            if (!slotList[i].GetComponent<SlotBehaviour>().isSlotted())
                return i;
        return -1;
    }

    private void displayMelody(GameObject[] slotList, Melodies.Melody melody) 
    {
        slot = slotList[findSlot(slotList)];
        Color melodyColor = melody.color;
        melodyColor.r = 1;
        melodyColor.g = 1;
        melodyColor.b = 1;
        melodyColor.a = 1;

        GameObject slottedSkill = slot.transform.GetChild(0).gameObject;
        slottedSkill.GetComponent<MelodyBehaviour>().melody = melody;
        slottedSkill.GetComponent<MelodyBehaviour>().setClickable(false);
        slottedSkill.GetComponent<Image>().sprite = melody.sprite;
        slottedSkill.GetComponent<Image>().color = melodyColor;
        slottedSkill.GetComponent<Image>().enabled = true;

        slot.GetComponent<SlotBehaviour>().setSlotted(true);
    }

}

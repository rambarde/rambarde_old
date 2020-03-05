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
    //IBeginDragHandler, 
    //IDragHandler, 
    //IEndDragHandler
{
    private Tooltip instrumentTooltip;
    private GameObject canvas;
    private GameObject tooltip;
    private RectTransform canvasRectTransform;
    private RectTransform tooltipRectTransform;

    private GameObject drag;
    private RectTransform dragTransform;
    private Image dragImage;
    private Sprite instrumentSprite;
    private Color instrumentColor;

    private GameObject[] instrumentSlots;
    private GameObject[] instrumentSkillSlots;
    GameObject slot;
    GameObject counter;

    void Start()
    {
        if (GameObject.FindWithTag("Tooltip") != null)
        {
            tooltip = GameObject.FindWithTag("Tooltip");
            instrumentTooltip = tooltip.GetComponent<Tooltip>();
            tooltipRectTransform = tooltip.GetComponent<RectTransform>() as RectTransform;
        }
        canvas = GameObject.FindWithTag("Canvas");
        canvasRectTransform = GameObject.FindWithTag("Canvas").GetComponent<RectTransform>() as RectTransform;

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
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(!pointerEventData.dragging)
        {
            if(instrumentTooltip!=null)
            {
                instrumentTooltip.instrument = GetComponent<InstrumentUI>();
                instrumentTooltip.skill = null;
                instrumentTooltip.Activated();

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
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (instrumentTooltip != null)
            instrumentTooltip.DeActivated();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!GetComponent<InstrumentUI>().isClickable)
            return;

        counter = GameObject.Find("Instruments counter");

        if (findSlot(instrumentSlots) == -1)
            return;

        slot = instrumentSlots[findSlot(instrumentSlots)];

        GameObject slottedSkill = slot.transform.GetChild(0).gameObject;
        slottedSkill.GetComponent<InstrumentUI>().equip(GetComponent<InstrumentUI>());
        slottedSkill.GetComponent<Image>().color = GetComponent<Image>().color;
        slottedSkill.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        slottedSkill.GetComponent<Image>().enabled = true;

        slot.GetComponent<SlotBehaviour>().setSlotted(true);

        counter.GetComponent<Counter>().increment();

        List<SkillUI> skillList = new List<SkillUI>();
        skillList.Add(GetComponent<InstrumentUI>().skill1);
        skillList.Add(GetComponent<InstrumentUI>().skill2);
        skillList.Add(GetComponent<InstrumentUI>().skill3);
        skillList.Add(GetComponent<InstrumentUI>().skill4);

        foreach (SkillUI skill in skillList)
            displaySkill(skill);

        GetComponent<InstrumentUI>().setClickable(false);

        GameObject.Find("Reset Instruments").GetComponent<Button>().onClick.AddListener(buttonReset);
    }
       
    void buttonReset() { GetComponent<InstrumentUI>().setClickable(true); }

    int findSlot(GameObject[] slotList)
    {
        for(int i = 0; i < slotList.Length; i++)
            if (!slotList[i].GetComponent<SlotBehaviour>().isSlotted())
                return i;
        return -1;
    }

    private void displaySkill(SkillUI skill)
    {
        slot = instrumentSkillSlots[findSlot(instrumentSkillSlots)];

        GameObject slottedSkill = slot.transform.GetChild(0).gameObject;
        slottedSkill.GetComponent<SkillUI>().equip(skill);
        slottedSkill.GetComponent<Image>().color = skill.GetComponent<Image>().color;
        slottedSkill.GetComponent<Image>().sprite = skill.GetComponent<Image>().sprite;
        slottedSkill.GetComponent<Image>().enabled = true;

        slot.GetComponent<SlotBehaviour>().setSlotted(true);
    }

    //public void OnBeginDrag(PointerEventData pointerEventData)
    //{
    //    if (GetComponent<InstrumentUI>().isDraggable)
    //    {
    //        drag = new GameObject();
    //        drag.AddComponent<CanvasRenderer>();
    //        dragTransform = drag.AddComponent<RectTransform>();
    //        dragImage = drag.AddComponent<Image>();
    //        drag.transform.SetParent(canvas.transform);
    //        drag.transform.SetAsLastSibling();
    //        dragImage.raycastTarget = false;

    //        /*skillTier = GetComponent<Skill>().skillTier;*/
    //        instrumentSprite = GetComponent<Image>().sprite;
    //        instrumentColor = GetComponent<Image>().color;

    //        dragTransform.pivot = new Vector2(0.5f, 0.5f);
    //        dragTransform.localScale = new Vector3(1, 1, 1);
    //        dragTransform.sizeDelta = new Vector2(150, 150);
    //        dragImage.sprite = instrumentSprite;
    //        dragImage.color = instrumentColor;

    //        if (transform.parent.CompareTag("Slot"))
    //        {
    //            GetComponent<Image>().enabled = false;
    //        }
    //    }
    //}

    //public void OnDrag(PointerEventData pointerEventData)
    //{
    //    if (GetComponent<InstrumentUI>().isDraggable)
    //    {
    //        Vector2 pointerPosition;
    //        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out pointerPosition);

    //        dragTransform.localPosition = pointerPosition;
    //    }
    //}

    //public void OnEndDrag(PointerEventData pointerEventData)
    //{
    //    if (GetComponent<InstrumentUI>().isDraggable)
    //    {
    //        Destroy(drag);
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillBehaviour: 
    MonoBehaviour,
    IPointerEnterHandler, 
    IPointerExitHandler,
    IPointerClickHandler
{
    private GameObject canvas;
    private GameObject tooltip;
    private RectTransform canvasRectTransform;
    private RectTransform tooltipRectTransform;
    private Tooltip skillTooltip;

    private GameObject[] slottedSkills;
    GameObject slot;
    GameObject counter;
    Button resetTier;

    void Start()
    {
        if(GameObject.FindWithTag("Tooltip")!=null)
        {
            tooltip = GameObject.FindWithTag("Tooltip");
            skillTooltip = tooltip.GetComponent<Tooltip>();
            tooltipRectTransform = tooltip.GetComponent<RectTransform>() as RectTransform;
        }
        canvas = GameObject.FindWithTag("Canvas");
        canvasRectTransform = GameObject.FindWithTag("Canvas").GetComponent<RectTransform>() as RectTransform;

        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        slottedSkills = new GameObject[4];
        int j = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            GameObject slot = slots[i];
            if (slot.GetComponent<SlotBehaviour>() != null && slot.GetComponent<SlotBehaviour>().innateSkillSlot)
            {
                slottedSkills[j] = slot;
                j += 1;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (!pointerEventData.dragging)
        {
            if (skillTooltip != null)
            {
                skillTooltip.skill = GetComponent<SkillUI>();
                skillTooltip.instrument = null;
                skillTooltip.Activated();

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
        if (skillTooltip != null)
            skillTooltip.DeActivated();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!GetComponent<SkillUI>().isClickable)
            return;
        int tier = GetComponent<SkillUI>().skillTier;
        switch (tier)
        {
            case 1:
                counter = GameObject.Find("Tier 1 counter");
                resetTier = GameObject.Find("Reset Tier 1").GetComponent<Button>();
                if (!slottedSkills[0].GetComponent<SlotBehaviour>().isSlotted())
                    slot = slottedSkills[0];
                else if (!slottedSkills[1].GetComponent<SlotBehaviour>().isSlotted())
                    slot = slottedSkills[1];
                else
                    return;
            break;

            case 2:
                counter = GameObject.Find("Tier 2 counter");
                resetTier = GameObject.Find("Reset Tier 2").GetComponent<Button>();
                if (!slottedSkills[2].GetComponent<SlotBehaviour>().isSlotted())
                    slot = slottedSkills[2];
                else
                    return;
            break;

            case 3:
                counter = GameObject.Find("Tier 3 counter");
                resetTier = GameObject.Find("Reset Tier 2").GetComponent<Button>();
                if (!slottedSkills[3].GetComponent<SlotBehaviour>().isSlotted())
                    slot = slottedSkills[3];
                else
                    return;
            break;
        }
        
        GameObject slottedSkill = slot.transform.GetChild(0).gameObject;
        slottedSkill.GetComponent<SkillUI>().equip(GetComponent<SkillUI>());
        slottedSkill.GetComponent<Image>().color = GetComponent<Image>().color;
        slottedSkill.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        slottedSkill.GetComponent<Image>().enabled = true;

        slot.GetComponent<SlotBehaviour>().setSlotted(true);

        counter.GetComponent<Counter>().increment();

        GetComponent<SkillUI>().setClickable(false);
        resetTier.onClick.AddListener(buttonReset);
    }

    void buttonReset() { GetComponent<SkillUI>().setClickable(true); }
}

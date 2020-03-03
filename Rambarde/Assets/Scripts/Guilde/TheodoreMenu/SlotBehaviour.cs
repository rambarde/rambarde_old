using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotBehaviour : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int skillTier;

    //private GameObject dragged;
    private GameObject slotted;
    private GameObject canvas;
    private GameObject temp;

    private SkillUI draggedSkill;
    private SkillUI slottedSkill;

    private InstrumentUI draggedInstrument;
    private InstrumentUI slottedInstrument;

    private Image draggedImage;
    private Image slottedImage;

    private Vector2 originalImageSize;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas").gameObject;
        slotted = transform.GetChild(0).gameObject;

        if (slotted.GetComponent<SkillUI>() != null)
        {
            slottedSkill = slotted.GetComponent<SkillUI>();
            slottedInstrument = null;
        }

        if (slotted.GetComponent<InstrumentUI>() != null)
        {
            slottedInstrument = slotted.GetComponent<InstrumentUI>();
            slottedSkill = null;
        }
    }

    public void OnDrop(PointerEventData pointerEventData)
    {
        if (pointerEventData.dragging && pointerEventData.pointerDrag != null)
        {
            Color spriteColor = draggedImage.color;
            spriteColor.a = 1f;
            slottedImage.color = spriteColor;
            slottedImage.sprite = draggedImage.sprite;

            if (pointerEventData.pointerDrag.GetComponent<SkillUI>() != null && slottedSkill != null)
            {
                if (draggedSkill.skillTier != skillTier)
                    return;
                slottedSkill.equip(draggedSkill);
            }

            if (pointerEventData.pointerDrag.GetComponent<InstrumentUI>() != null && slottedInstrument != null)
            {
                slottedInstrument.equip(draggedInstrument);
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.dragging && pointerEventData.pointerDrag != null)
        {
            draggedImage = pointerEventData.pointerDrag.GetComponent<Image>();
            temp = canvas.transform.GetChild(canvas.transform.childCount - 1).gameObject;
            originalImageSize = temp.GetComponent<RectTransform>().sizeDelta;

            slottedImage = slotted.GetComponent<Image>();

            Color spriteColor = draggedImage.color;
            spriteColor.a = 150f / 255f;
            slottedImage.color = spriteColor;
            slottedImage.sprite = draggedImage.sprite;

            if (pointerEventData.pointerDrag.GetComponent<SkillUI>() != null && slottedSkill != null)
            {
                draggedSkill = pointerEventData.pointerDrag.GetComponent<SkillUI>();
                if (draggedSkill.skillTier != skillTier)
                    return;
                slottedImage.enabled = true;
                canvas.transform.GetChild(canvas.transform.childCount - 1).GetComponent<RectTransform>().sizeDelta = slotted.GetComponent<RectTransform>().sizeDelta;
            }

            if (pointerEventData.pointerDrag.GetComponent<InstrumentUI>() != null && slottedInstrument != null)
            {
                draggedInstrument = pointerEventData.pointerDrag.GetComponent<InstrumentUI>();  
                slottedImage.enabled = true;
                canvas.transform.GetChild(canvas.transform.childCount - 1).GetComponent<RectTransform>().sizeDelta = slotted.GetComponent<RectTransform>().sizeDelta;
            }    
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.dragging && pointerEventData.pointerDrag != null)
        {
            if (pointerEventData.pointerDrag.GetComponent<SkillUI>() != null && slottedSkill != null)
            {
                if (draggedSkill.skillTier != skillTier)
                    return;
                slottedImage.enabled = false;
                canvas.transform.GetChild(canvas.transform.childCount - 1).GetComponent<RectTransform>().sizeDelta = originalImageSize;
            }

            if (pointerEventData.pointerDrag.GetComponent<InstrumentUI>() != null && slottedInstrument != null)
            {
                slottedImage.enabled = false;
                canvas.transform.GetChild(canvas.transform.childCount - 1).GetComponent<RectTransform>().sizeDelta = originalImageSize;
            }
        }
    }
}

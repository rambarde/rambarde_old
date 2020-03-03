using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillBehaviour: 
    MonoBehaviour,
    /*AbstractSkill<Skill>,*/
    IPointerEnterHandler, 
    IPointerExitHandler,
    IDragHandler, 
    IBeginDragHandler, 
    IEndDragHandler
{ 
    private GameObject canvas;
    private GameObject tooltip;
    private RectTransform canvasRectTransform;
    private RectTransform tooltipRectTransform;
    private Tooltip skillTooltip;

    private GameObject drag;
    private RectTransform dragTransform;
    private Image dragImage;
    private int skillTier;
    private Sprite skillSprite;
    private Color skillColor;
    
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

    //called before a drag is started; create the drag image and update shit
    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (GetComponent<SkillUI>().isDraggable)
        {
            drag = new GameObject();
            drag.AddComponent<CanvasRenderer>();
            dragTransform = drag.AddComponent<RectTransform>();
            dragImage = drag.AddComponent<Image>();
            drag.transform.SetParent(canvas.transform);
            drag.transform.SetAsLastSibling();
            dragImage.raycastTarget = false;

            skillTier = GetComponent<SkillUI>().skillTier;
            skillSprite = GetComponent<Image>().sprite;
            skillColor = GetComponent<Image>().color;

            dragTransform.pivot = new Vector2(0.5f, 0.5f);
            dragTransform.localScale = new Vector3(1, 1, 1);
            dragTransform.sizeDelta = new Vector2(150, 150);
            dragImage.sprite = skillSprite;
            dragImage.color = skillColor;

            if (transform.parent.CompareTag("Slot"))
            {
                GetComponent<Image>().enabled = false;
            }
        }
    }

    //update drag image position to mouse current position; called every time cursor is moved
    public void OnDrag(PointerEventData pointerEventData)
    {
        if(GetComponent<SkillUI>().isDraggable)
        {
            Vector2 pointerPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out pointerPosition);

            dragTransform.localPosition = pointerPosition;
        }
        
    }

    //called after a drag is complete; 
    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (GetComponent<SkillUI>().isDraggable)
        {
            Destroy(drag);
        }
    }
}

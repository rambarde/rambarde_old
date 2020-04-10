using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillBehaviour : 
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public Skills.Skill skill;
    RectTransform canvasRectTransform;
    RectTransform tooltipRectTransform;
    Tooltip tooltip;

    void Start()
    {
        if (GameObject.FindWithTag("Tooltip") != null)
        {
            tooltip = GameObject.FindWithTag("Tooltip").GetComponent<Tooltip>();
            tooltipRectTransform = GameObject.FindWithTag("Tooltip").GetComponent<RectTransform>() as RectTransform;
            canvasRectTransform = tooltipRectTransform.parent.GetComponent<RectTransform>() as RectTransform;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (tooltip != null)
        {
            tooltip.setObject(gameObject);
            tooltip.Activate(true);

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

    public void OnPointerExit(PointerEventData poinerEventData)
    {
        if (tooltip != null)
            tooltip.Activate(false);
    }
}

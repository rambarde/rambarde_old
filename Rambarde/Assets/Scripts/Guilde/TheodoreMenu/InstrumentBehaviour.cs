using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InstrumentBehaviour : 
    MonoBehaviour, 
    IPointerEnterHandler, 
    IPointerExitHandler, 
    IBeginDragHandler, 
    IDragHandler, 
    IEndDragHandler
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

        //Instruments skills/Innate skills stockage: database ? nb fini => à la mano ? 
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(!pointerEventData.dragging)
        {
            if(instrumentTooltip!=null)
            {
                instrumentTooltip.instrument = GetComponent<Instrument>();
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

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (GetComponent<Instrument>().isDraggable)
        {
            drag = new GameObject();
            drag.AddComponent<CanvasRenderer>();
            dragTransform = drag.AddComponent<RectTransform>();
            dragImage = drag.AddComponent<Image>();
            drag.transform.SetParent(canvas.transform);
            drag.transform.SetAsLastSibling();
            dragImage.raycastTarget = false;

            /*skillTier = GetComponent<Skill>().skillTier;*/
            instrumentSprite = GetComponent<Image>().sprite;
            instrumentColor = GetComponent<Image>().color;

            dragTransform.pivot = new Vector2(0.5f, 0.5f);
            dragTransform.localScale = new Vector3(1, 1, 1);
            dragTransform.sizeDelta = new Vector2(150, 150);
            dragImage.sprite = instrumentSprite;
            dragImage.color = instrumentColor;

            if (transform.parent.CompareTag("Slot"))
            {
                GetComponent<Image>().enabled = false;
            }
        }
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (GetComponent<Instrument>().isDraggable)
        {
            Vector2 pointerPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out pointerPosition);

            dragTransform.localPosition = pointerPosition;
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (GetComponent<Instrument>().isDraggable)
        {
            Destroy(drag);
        }
    }
}

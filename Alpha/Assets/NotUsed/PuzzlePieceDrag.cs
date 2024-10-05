using UnityEngine;
using UnityEngine.EventSystems;

public class DragWithBoundaries : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;
    private Vector2 offset;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out offset);
        offset = rectTransform.anchoredPosition - offset;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            Vector2 newPosition = localPointerPosition + offset;
            if (IsWithinBounds(newPosition))
            {
                rectTransform.anchoredPosition = newPosition;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Optionally, snap to nearest position or leave as is
    }

    private bool IsWithinBounds(Vector2 position)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 minPosition = new Vector2(canvasRect.rect.xMin, canvasRect.rect.yMin);
        Vector2 maxPosition = new Vector2(canvasRect.rect.xMax, canvasRect.rect.yMax);

        return position.x >= minPosition.x && position.x <= maxPosition.x &&
               position.y >= minPosition.y && position.y <= maxPosition.y;
    }
}

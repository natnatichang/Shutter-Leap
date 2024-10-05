using UnityEngine;
using UnityEngine.EventSystems;

public class SnapToLayer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;

    public RectTransform grayVersion; // Reference to the gray version of this puzzle piece
    public float snapDistance = 50f; // Distance within which snapping occurs

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Vector2.Distance(rectTransform.anchoredPosition, grayVersion.anchoredPosition) < snapDistance)
        {
            rectTransform.anchoredPosition = grayVersion.anchoredPosition;
            // Optionally disable further dragging
           //  GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}

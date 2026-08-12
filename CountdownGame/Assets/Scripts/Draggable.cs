using UnityEngine;
using UnityEngine.EventSystems;
using SoundControl;
using Unity.VisualScripting;

public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler 
{
    public Vector2 distance;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnMouseDown()
    {
        SoundEffectManager.Play("sfx_click");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
     
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerPosition = Camera.main.ScreenToWorldPoint( eventData.position);
        Vector2 newObjectPosition = pointerPosition - distance;
        transform.position = newObjectPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        distance = Camera.main.ScreenToWorldPoint(eventData.position) - transform.position;
    }
}

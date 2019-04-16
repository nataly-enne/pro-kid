using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public int maxChildren;
    public bool full = false;

    public GameObject slotPrefab;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        
        if (d != null && transform.childCount < maxChildren)
        {
            d.placeholderParent = transform;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null && d.placeholderParent == transform)
        {
            d.placeholderParent = d.originalParent;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
        {
            if (transform.childCount > maxChildren)
            {
                Destroy(d.placeholder);
                Destroy(d.gameObject);
            }
            else
            {
                Destroy(d.placeholder);
                d.originalParent = transform;
            }
        }
    }
}

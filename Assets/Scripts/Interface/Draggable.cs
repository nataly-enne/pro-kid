using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Transform originalParent = null;
    public Transform placeholderParent = null;
    public GameObject placeholderPrefab = null;
    public GameObject placeholder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent.name.Contains("Pool"))
        {
            GameObject clone = Instantiate(gameObject) as GameObject;
            clone.name = transform.name;
            clone.transform.SetParent(transform.parent);
            clone.transform.SetSiblingIndex(transform.GetSiblingIndex());
        }

        placeholder = Instantiate(placeholderPrefab);
        placeholder.name = "Placeholder";

        placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        originalParent = transform.parent;
        placeholderParent = originalParent;
        transform.SetParent(transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (placeholderParent.name.Contains("Pool"))
            placeholder.SetActive(false);
        else
            placeholder.SetActive(true);

        if (placeholder.GetComponent<Placeholder>() != null)
            placeholder.GetComponent<Placeholder>().UpdateStatus();

        transform.position = eventData.position;

        if (placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.SetParent(placeholderParent);
        }

        int newSiblingIndex = placeholderParent.childCount;

        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;

                break;
            }
        }

        if (placeholderParent.name.Contains("For Queue"))
        {
            if (newSiblingIndex != 0)
                placeholder.transform.SetSiblingIndex(newSiblingIndex);
        }
        else
        {
            placeholder.transform.SetSiblingIndex(newSiblingIndex);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeholder);

        if (placeholderParent.name.Contains("Pool"))
            Destroy(gameObject);
    }
}

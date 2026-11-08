using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public int expectedShapeID = 1; // L'ID attendu par cet emplacement

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Draggable item = eventData.pointerDrag.GetComponent<Draggable>();
            if (item != null && item.shapeID == expectedShapeID)
            {
                item.transform.SetParent(transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                item.parentAfterDrag = transform;
                item.GetComponent<CanvasGroup>().blocksRaycasts = true;

                // On empêche le déplacement de l'objet
                item.enabled = false;

                Debug.Log("Bonne forme placée !");
            }
        }
    }
}
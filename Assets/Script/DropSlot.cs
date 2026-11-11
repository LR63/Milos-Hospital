using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public int expectedShapeID = 1;
    [HideInInspector] public MiniGame_DragAndDrop gameLogic;

    public void OnDrop(PointerEventData eventData)
    {
        Draggable item = eventData.pointerDrag.GetComponent<Draggable>();
        if (item != null && item.shapeID == expectedShapeID)
        {
            item.transform.SetParent(transform);
            item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            item.parentAfterDrag = transform;
            item.GetComponent<CanvasGroup>().blocksRaycasts = true;

            item.enabled = false;
            item.GetComponent<Image>().color = new Color(0, 255, 0, 1f);

            if (gameLogic != null)
            {
                gameLogic.OnItemPlacedCorrectly();
            }
        }
    }
}
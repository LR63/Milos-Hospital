using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// Classe pour associer un Sprite d'item avec un Sprite de slot
[System.Serializable]
public class ItemData
{
    public Sprite itemSprite;
    public Sprite slotSprite;
}

public class MiniGame_DragAndDrop : MonoBehaviour
{
    [Header("Liste des Objets")]
    public List<ItemData> allItems; 

    [Header("Configuration")]
    public int itemsToGenerate = 5;
    public float scaleMultiplier = 5f;
    public float padding = 10f;

    [Header("Prefabs")]
    public GameObject slotPrefab;
    public GameObject itemPrefab;

    [Header("Conteneurs")]
    public RectTransform slotsContainer;
    public RectTransform itemsContainer;

    public MinigameManager manager;
    private int itemsPlacedCorrectly = 0;

    // Appelée par "OnGameStart" de MinigameManager.cs
    public void InitMinigame()
    {
        itemsPlacedCorrectly = 0;

        foreach (Transform child in slotsContainer) Destroy(child.gameObject);
        foreach (Transform child in itemsContainer) Destroy(child.gameObject);

        // On choisit les items à générer aléatoirement
        List<ItemData> shuffledItems = allItems.OrderBy(x => Random.value).ToList();
        List<ItemData> randomItems = shuffledItems.Take(itemsToGenerate).ToList();

        List<Rect> placedSlots = new();
        List<Rect> placedItems = new();

        // Génération des slots et des items
        for (int item = 0; item < randomItems.Count; item++)
        {
            int currentID = item;

            // --- Slot ---
            GameObject newSlot = Instantiate(slotPrefab, slotsContainer);
            newSlot.name = "Slot_" + item;

            Image slotImg = newSlot.GetComponent<Image>();
            slotImg.sprite = randomItems[item].slotSprite;
            slotImg.color = Color.white;
            slotImg.SetNativeSize();

            RectTransform slotRect = newSlot.GetComponent<RectTransform>();
            slotRect.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1f);

            DropSlot dropScript = newSlot.GetComponent<DropSlot>();
            dropScript.expectedShapeID = currentID;
            dropScript.gameLogic = this;

            // --- Item ---
            GameObject newItem = Instantiate(itemPrefab, itemsContainer);
            newItem.name = "Item_" + item;

            Image itemImg = newItem.GetComponent<Image>();
            itemImg.sprite = randomItems[item].itemSprite;
            itemImg.SetNativeSize();

            RectTransform itemRect = newItem.GetComponent<RectTransform>();
            itemRect.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1f);
            newItem.GetComponent<Draggable>().shapeID = currentID;

            SetRandomPosition(slotRect, slotsContainer, placedSlots);
            SetRandomPosition(itemRect, itemsContainer, placedItems);
        }
    }

    public void OnItemPlacedCorrectly()
    {
        itemsPlacedCorrectly++;
        if (itemsPlacedCorrectly >= itemsToGenerate)
        {
            manager.WinGame();
        }
    }

    void SetRandomPosition(RectTransform rectTransform, RectTransform container, List<Rect> placedRects)
    {
        // Calcul de la taile de l'objet
        float realWidth = rectTransform.rect.width * rectTransform.localScale.x;
        float realHeight = rectTransform.rect.height * rectTransform.localScale.y;

        // On calcule le centre de l'objet
        float halfW = realWidth / 2f;
        float halfH = realHeight / 2f;

        // Limite de la zone
        float minX = container.rect.xMin + halfW;
        float maxX = container.rect.xMax - halfW;
        float minY = container.rect.yMin + halfH;
        float maxY = container.rect.yMax - halfH;

        int maxAttempts = 100; // Limite du nombre de tentatives de placement

        bool positionFound = false;
        float randomX = 0f, randomY = 0f;
        Rect newRect = new();

        // Boucle pour trouver une position libre
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            randomX = Random.Range(minX, maxX);
            randomY = Random.Range(minY, maxY);

            // On crée une "zone" autour de l'objet
            newRect = new Rect(
                randomX - halfW - (padding / 2f),
                randomY - halfH - (padding / 2f),
                realWidth + padding,
                realHeight + padding
            );

            // Vérifie si cette zone rentre en collision avec un objet déjà placé
            bool overlaps = false;
            foreach (Rect placed in placedRects)
            {
                if (newRect.Overlaps(placed))
                {
                    overlaps = true;
                    break;
                }
            }

            if (!overlaps)
            {
                positionFound = true;
                break; // On a trouvé une place, on sort de la boucle
            }
        }

        // Cas où aucune position n'a été trouvée après le nombre maximum de tentatives
        if (!positionFound)
        {
            Debug.LogWarning("Manque de place, chevauchement");
        }

        rectTransform.anchoredPosition = new Vector2(randomX, randomY);
        placedRects.Add(newRect);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupInventorySlot : MonoBehaviour
{
    private RectTransform rectTransform;

    public InventoryItem InventoryItem { get; private set; }
    private OnInventoryPicked onInventoryPicked;

    private bool removed;
    private Vector3 targetPosition;
    private Button button;

    public delegate void OnInventoryPicked(InventoryItem item);

    public void Create(InventoryItem item, OnInventoryPicked onInventoryPicked) {
        this.onInventoryPicked = onInventoryPicked;
        this.rectTransform = GetComponent<RectTransform>();
        this.targetPosition = new Vector3(
            -rectTransform.rect.width,
            0,
            0
        );

        var image = transform.Find("Image").GetComponent<Image>();
        var text = transform.Find("Text").GetComponent<Text>();

        this.InventoryItem = item;
        image.sprite = item.Sprite();
        text.text = item.Title;

        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(OnClicked);
    }

    public void RemoveSlot() {
        removed = true;

        this.button.onClick.RemoveListener(OnClicked);
        this.targetPosition = new Vector3(
            -this.targetPosition.x,
            this.targetPosition.y,
            this.targetPosition.z
        );
    }

    public void MoveTo(Vector3 position) {
        if (removed) {
            return;
        }
        targetPosition = position;
    }

    private void Update()
    {
        rectTransform.localPosition = Vector3.MoveTowards(
            rectTransform.localPosition,
            targetPosition,
            400 * Time.unscaledDeltaTime
        );
        if (removed && rectTransform.localPosition == targetPosition) {
            Object.Destroy(this.gameObject);
        }
    }

    public void OnClicked() {
        if (removed) {
            return;
        }
        this.onInventoryPicked.Invoke(this.InventoryItem);
    }
}

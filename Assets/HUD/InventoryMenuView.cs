using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryMenuView : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    public InventoryContainer inventory;

    public GameObject InventoryMenuSlotPrefab;

    readonly private List<InventoryMenuSlot> slots = new List<InventoryMenuSlot>();

    RectTransform panel;

    void OnEnable() {
        this.panel = transform.Find("Panel").GetComponent<RectTransform>();

        inventory.OnInventoryAdded += OnInventoryAdded;
        inventory.OnInventoryRemoved += OnInventoryRemoved;
    }

    void OnDisable() {
        inventory.OnInventoryAdded -= OnInventoryAdded;
        inventory.OnInventoryRemoved -= OnInventoryRemoved;
    }

    private void OnInventoryAdded(InventoryItem item) {
        var slot = Instantiate(InventoryMenuSlotPrefab, panel.transform).GetComponent<InventoryMenuSlot>();
        slot.Create(item);

        slots.Add(slot);

        Realign();
    }

    private void OnInventoryRemoved(InventoryItem item) {
        
    }

    private void Realign() {
        for (var i  = 0; i < slots.Count; i++) {
            var t = slots[i].GetComponent<RectTransform>();
            t.localPosition = new Vector3(
                -i * 48,
                0,
                0
            );
        }
    }

    private bool isPanelOpened;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPanelOpened = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPanelOpened = false;
    }

    void Update() {
        var targetScale = isPanelOpened ? 1 : 0;
        panel.localScale = Vector3.MoveTowards(
            panel.localScale, new Vector3(targetScale, targetScale, 1), Time.unscaledDeltaTime * 10
        );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InventoryContainer))]
public class InventoryPickuper : MonoBehaviour
{
    public RectTransform PickupMenuContainer;
    public GameObject SlotPrefab;

    private InventoryContainer inventory;

    private readonly List<PickupInventorySlot> nearestItems = new List<PickupInventorySlot>();
    
    void Start()
    {
        this.inventory = GetComponent<InventoryContainer>();
    }

    void OnTriggerEnter(Collider trigger)
    {
        var item = trigger.GetComponent<InventoryItem>();
        if (null == item) {
            return;
        }

        var slot = Instantiate(SlotPrefab, PickupMenuContainer.transform).GetComponent<PickupInventorySlot>();
        slot.Create(item, OnInventoryPicked);

        nearestItems.Add(slot);
        Realign();
    }

    void OnTriggerExit(Collider trigger) {
        var item = trigger.GetComponent<InventoryItem>();
        if (null == item)
        {
            return;
        }
        RemoveSlot(item);
    }

    private void RemoveSlot(InventoryItem item) {
        var slot = nearestItems.Find(x => x.InventoryItem == item);
        if (null == slot)
        {
            return;
        }
        nearestItems.Remove(slot);
        slot.RemoveSlot();

        Realign();
    }

    private void OnInventoryPicked(InventoryItem item) {
        RemoveSlot(item);
        this.inventory.Add(item);
    }

    private void Realign() {
        var size = nearestItems.Count;
        var offset = ((size - 1) * 48) * 0.5f;
        
        for (var i = 0; i < size; i++)  {
            nearestItems[i].MoveTo(
                new Vector3(
                    0,
                    i * 48 - offset,
                    0
                )
            );
        }
    }
}

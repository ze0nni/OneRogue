using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryContainer))]
public class InventoryPickuper : MonoBehaviour
{
    private InventoryContainer inventory;

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
        inventory.Add(item);
    }
}

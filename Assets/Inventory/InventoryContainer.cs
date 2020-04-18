using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
    public delegate void OnInventoryChanged(InventoryItem item);

    readonly List<InventoryItem> items = new List<InventoryItem>();
    public IReadOnlyList<InventoryItem> Items { get => items; }

    public event OnInventoryChanged OnInventoryAdded;
    public event OnInventoryChanged OnInventoryRemoved;

    public void Add(InventoryItem item) {
        item.gameObject.SetActive(false);
        item.transform.SetParent(this.transform);
        items.Add(item);

        OnInventoryAdded?.Invoke(item);
    }
}

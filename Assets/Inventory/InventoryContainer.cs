using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
    public void Add(InventoryItem item) {
        item.gameObject.SetActive(false);
        item.transform.SetParent(this.transform);
    }
}

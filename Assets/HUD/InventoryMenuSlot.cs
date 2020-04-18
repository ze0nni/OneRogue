using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuSlot : MonoBehaviour
{
    public InventoryItem item { get; private set; }

    internal void Create(InventoryItem item)
    {
        this.item = item;

        var image = this.transform.Find("Image").GetComponent<Image>();
        image.sprite = item.Sprite();
    }
}

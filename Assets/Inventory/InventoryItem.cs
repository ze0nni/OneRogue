using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public string Title;

    public Sprite Sprite() {
        var face = transform.Find("Face");
        if (null == face) {
            Debug.LogWarning("Face not found for:" + gameObject.name);
            return null;
        }
        var sprite = face.GetComponent<SpriteRenderer>();
        if (null == sprite) {
            Debug.LogWarning("Face<SpriteRenderer> not found for:" + gameObject.name);
            return null;
        }
        return sprite.sprite;
    }
}

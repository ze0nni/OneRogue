using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScreen : MonoBehaviour
{
    public Image left;
    public Image right;

    private Canvas casnvas;
    private RectTransform casnvasTransform;

    void Start() {
        this.casnvas = GetComponent<Canvas>();
        this.casnvasTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        var canvasSize = casnvasTransform.rect;

        left.rectTransform.localPosition = new Vector3(-canvasSize.width * 0.25f, 0.5f, 0);
        right.rectTransform.localPosition = new Vector3(canvasSize.width * 0.25f, 0.5f, 0);

        left.rectTransform.sizeDelta = new Vector3(canvasSize.width * 0.5f, canvasSize.height);
        right.rectTransform.sizeDelta = new Vector3(canvasSize.width * 0.5f, canvasSize.height);
    }
}
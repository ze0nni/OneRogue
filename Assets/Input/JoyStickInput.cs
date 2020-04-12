using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickInput : MonoBehaviour
{
    public Player player;

    private Image image;
    private Image stick;
    private float stickOffset;

    void OnEnable()
    {
        this.image = this.GetComponent<Image>();
        this.stick = this.transform.Find("Stick").GetComponent<Image>();

        this.stickOffset = (image.rectTransform.rect.width - stick.rectTransform.rect.width) / 2;
    }
    void Update()
    {
        var Horizontal = Input.GetAxis("Horizontal");
        var Vertical = Input.GetAxis("Vertical");

        stick.transform.localPosition = new Vector3(
            Horizontal * stickOffset,
            Vertical * stickOffset,
            0
        );

        player.SetHorisontalAxis(Horizontal);
        player.SetVerticalAxis(Vertical);
    }
}

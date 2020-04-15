using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private float updateSpeed = 0.25f;
    private Image bar;
    private Image subBar;

    void Start()
    {
        bar = transform.Find("bar").GetComponent<Image>();
        subBar = transform.Find("subbar").GetComponent<Image>();
    }

    void Update()
    {
        var scale = subBar.transform.localScale.x;
        var targetScale = bar.transform.localScale.x;
        if (scale == targetScale)
        {
            return;
        }
        var delta = updateSpeed * Time.deltaTime;
        subBar.transform.localScale = new Vector3(
            Mathf.MoveTowards(scale, targetScale, delta),
            1,
            1
        );
    }

    public void UpdateBar(float scale, bool fast)
    {

        bar.transform.localScale = new Vector3(
            scale,
            1,
            1
        );
        if (fast)
        {
            subBar.transform.localScale = new Vector3(
            scale,
            1,
            1
        );
        }
    }

    public void UpdateBar(float points, float max, bool fast)
    {
        if (0 == max)
        {
            UpdateBar(0, fast);
        }
        else
        {
            UpdateBar(points / max, fast);
        }
    }
}
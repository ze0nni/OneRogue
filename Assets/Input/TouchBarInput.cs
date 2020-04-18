using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class OnCoordChanged : UnityEvent<float>
{
}

public class TouchBarInput : MonoBehaviour,
    IPointerDownHandler
{
    public bool YInvert = true;
    public float Sensetive = 1f;

    private float sensScale;

    private bool active;
    private Vector2 startPosition;
    private int touchFinger;
    private Vector2 startInputPos;

    private float xAngle;
    private float yAngle;

    public OnCoordChanged OnReleativeCoordX;
    public OnCoordChanged OnReleativeCoordY;
    public OnCoordChanged OnDeltaCoordX;
    public OnCoordChanged OnDeltaCoordY;

    void Start() {
        this.sensScale = (Mathf.PI * 2) / Screen.width;
    }

    public void BeginTouch() {
        if (this.active)
        {
            return;
        }

        foreach (var t in Input.touches)
        {
            if (TouchPhase.Began == t.phase)
            {
                this.active = true;
                this.touchFinger = t.fingerId;
                this.startPosition = t.position;

                return;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BeginTouch();
    }

    public bool GetTouch(int touchFinger, ref Touch touch)
    {
        var count = Input.touchCount;
        for (var i = 0; i < count; i++)
        {
            var t = Input.GetTouch(i);
            if (t.fingerId == touchFinger)
            {
                touch = t;
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        if (false == active) {
            return;
        }

        Touch touch = new Touch();
        if (false == GetTouch(this.touchFinger, ref touch)
            || TouchPhase.Ended == touch.phase
            || TouchPhase.Canceled == touch.phase
        ) {
            this.active = false;

            OnReleativeCoordX.Invoke(0);
            OnReleativeCoordY.Invoke(0);
            OnDeltaCoordX.Invoke(0);
            OnDeltaCoordY.Invoke(0);

            return;
        }
        var offset = touch.deltaPosition * sensScale;
        var releative = (touch.position - this.startPosition) * sensScale * Sensetive;

        OnReleativeCoordX.Invoke(releative.x);
        OnReleativeCoordY.Invoke(YInvert ? -releative.y : releative.y);
        OnDeltaCoordX.Invoke(offset.x);
        OnDeltaCoordY.Invoke(YInvert ? -offset.y : offset.y);
    }
}

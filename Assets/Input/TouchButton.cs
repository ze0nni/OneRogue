using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class OnButtonChanged : UnityEvent<bool>
{
}

public class TouchButton : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    public OnButtonChanged OnButtonChanged;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnButtonChanged.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonChanged.Invoke(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

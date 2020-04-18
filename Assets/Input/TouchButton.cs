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
    public UnityEvent OnButtonDown;
    public UnityEvent OnButtonUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnButtonDown.Invoke();
        OnButtonChanged.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonChanged.Invoke(false);
        OnButtonUp.Invoke();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

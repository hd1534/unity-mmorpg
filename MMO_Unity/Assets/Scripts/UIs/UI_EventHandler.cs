using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler {
    
    public Action<PointerEventData> onDregHandler = null;
    public Action<PointerEventData> onClickHandler = null;
    
    public void OnDrag(PointerEventData eventData) {
        onDregHandler?.Invoke(eventData);
    }

    public void OnPointerClick(PointerEventData eventData) {
        onClickHandler?.Invoke(eventData);
    }
}

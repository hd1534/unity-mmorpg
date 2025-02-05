using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager {
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
    
    bool _pressed = false;

    // Update is called once per frame
    public void OnUpdate() {
        // ui 를 클릭시
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        
        if (Input.anyKey && KeyAction != null) {
            KeyAction.Invoke();
        }

        if (MouseAction != null) {
            if (Input.GetMouseButton(0)) {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else {
                if (_pressed) {
                    MouseAction.Invoke(Define.MouseEvent.Click);
                }
                _pressed = false;
            }
        }
    }
}
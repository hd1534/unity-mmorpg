using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager {
    private int _order = 0;
    
    UI_Scene _scene = null;
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

    public GameObject Root => GameObject.Find("@UI_Root") ?? new GameObject { name = "@UI_Root" };

    public void SetCanvas(GameObject go, bool sort = true) {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort) {
            canvas.sortingOrder = _order++;
        }
        else {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene {
        name ??= typeof(T).Name;

        GameObject go = Manager.Resource.Instantiate($"UIs/Scenes/{name}", Root.transform);
        T scene = go.GetOrAddComponent<T>();
        _scene = scene;
        
        return scene;
    }

    public T MakeSubItem<T>(string name = null, Transform parent = null) where T : UI_Base {
        name ??= typeof(T).Name;

        GameObject go = Manager.Resource.Instantiate($"UIs/SubItems/{name}", parent ?? Root.transform);
        return go.GetOrAddComponent<T>();
    }


    public T ShowPopupUI<T>(string name = null) where T : UI_Popup {
        name ??= typeof(T).Name;

        GameObject go = Manager.Resource.Instantiate($"UIs/Popups/{name}", Root.transform);
        T popup = go.GetOrAddComponent<T>();
        _popupStack.Push(popup);
        
        return popup;
    }

    public void ClosePopupUI(UI_Popup popup) {
        if (_popupStack.Peek() != popup) {
            Debug.Log($"Popup NotFounded: {popup.name}");
            return;
        }
        
        ClosePopupUI();
    }

    public void ClosePopupUI() {
        if(_popupStack.Count == 0) return;

        UI_Popup _popup = _popupStack.Pop();
        Manager.Resource.Destroy(_popup.gameObject);
    }

    public void CloseAllPopupUI() {
        while(_popupStack.Count == 0) {
            ClosePopupUI();
        }
    }
}

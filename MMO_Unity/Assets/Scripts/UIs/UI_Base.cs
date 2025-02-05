using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public abstract class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    protected void Bind<T>(Type type) where T : UnityEngine.Object {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < objects.Length; i++) {
            if (typeof(T) == typeof(GameObject)) {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }
        }
    }

    protected Button GetButton(int idx) {
        return Get<Button>(idx);
    }
    protected TextMeshProUGUI GetText(int idx) {
        return Get<TextMeshProUGUI>(idx);
    }
    protected Image GetImage(int idx) {
        return Get<Image>(idx);
    }

    public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click) {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type) {
            case Define.UIEvent.Click:
                evt.onClickHandler -= action;
                evt.onClickHandler += action;
                break;
            case Define.UIEvent.Drag :
                evt.onDregHandler -= action;
                evt.onDregHandler += action;
                break;
        }
    }
    protected T Get<T>(int idx) where T : Object {
        Object[] objects;
        if (_objects.TryGetValue(typeof(T), out objects) == false) {
            return null;
        }
          
        return objects[idx] as T;
    }

}

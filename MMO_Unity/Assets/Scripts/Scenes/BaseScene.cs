using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

public abstract class BaseScene : MonoBehaviour {
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;
    
    private void Awake() {
          Init();
    }

    protected virtual void Init() {
        Object eventSystem = GameObject.FindFirstObjectByType(typeof(EventSystem));
        if (eventSystem is null) {
            Manager.Resource.Instantiate("UIs/EventSystem").name = "@EventSystem";
        }
    }

    public abstract void Clear();
}

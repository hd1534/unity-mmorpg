using UnityEngine;

public class Util : MonoBehaviour {
    public static T GetOrAddComponent<T>(GameObject go) where T : Component {
        return go.GetComponent<T>() ?? go.AddComponent<T>();
    }
    
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false) {
        return FindChild<Transform>(go, name, recursive)?.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : Object {
        if (go == null) {
            return null;
        }


        if (recursive) {
            foreach (T component in go.GetComponentsInChildren<T>()) {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        else {
            for (int i = 0; i < go.transform.childCount; i++) {
                Transform transform = go.transform.GetChild(i);
                
                if (string.IsNullOrEmpty(name) || transform.name == name) {
                    T component = transform.GetComponent<T>();
                
                    if (component != null) {
                        return component;
                    }
                }
            }
        }


        return null;
    }
}

using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class 
    ResourceManager
{
    public T Load<T>(string path) where T : Object {
        if (typeof(T) == typeof(GameObject)) {
            string name = path.Split('/').Last();
            Debug.Log(name);

            GameObject go = Manager.Pool.GetOriginal(name);
            if (go) {
                return go as T;
            }
        }
        
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null) {
        GameObject original = Load<GameObject>($"Prefabs/{path}");

        if (original  == null) {
            Debug.Log($"Prefabs/{path} not found");
            return null;
        }

        if (original.GetComponent<Poolable>()) {
            return Manager.Pool.Pop(original, parent).gameObject;
        }
        
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        
        return go;
    }

    public void Destroy(GameObject go) {
        if (go == null) {
            return;
        }
        
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable) {
            Manager.Pool.Push(poolable);
            return;
        }
        
        Object.Destroy(go);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class PoolManager {
    class Pool {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }
        
        Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5) {
            Original = original;
            Root = new GameObject($"{original.name}_Root").transform;

            for (int i = 0; i < count; i++) {
                Push( Create());
            }
        }

        Poolable Create() {
            GameObject go = GameObject.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable) {
            if (poolable == null) {
                return;
            }
            
            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;
            
            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent) {
            Poolable poolable;

            if (_poolStack.Count > 0) {
                poolable = _poolStack.Pop();
            }
            else {
                poolable = Create();
            }

                // DontDestroyOnLoad 회피용
            if (parent == null) {
                poolable.transform.parent = Manager.Scene.CurrentScene.transform;
            }
            
            poolable.transform.parent = parent;
            poolable.IsUsing = true;
            poolable.gameObject.SetActive(true);
            return poolable;
        }
    }
    
    private Transform _root;
    Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    public void Init() {
        if (!_root) {
            _root = new GameObject("@Pool_Root").transform;
            Object.DontDestroyOnLoad(_root);
        }
        
    }

    public void CreatePool(GameObject original, int count = 5) {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;
        
        _pools.Add(original.name, pool);
    }

    public void Push(Poolable poolable) {
        string name = poolable.gameObject.name;
        if (_pools.ContainsKey(name) is false) {
            GameObject.Destroy(poolable.gameObject);
            return;
        }
        
        _pools[name].Push(poolable);
        
    }

    public Poolable Pop(GameObject origin, Transform parent = null) {
        if (_pools.ContainsKey(origin.name) is false) {
            CreatePool(origin);
        }
        
        return _pools[origin.name].Pop(parent);
    }

    public GameObject GetOriginal(string name) {
        if (_pools.ContainsKey(name) is false) {
            return null;
        }
        
        return _pools[name].Original;
    }
}

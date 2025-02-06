using UnityEngine;

public class Manager : MonoBehaviour {
    private static Manager _instance;

    public static Manager instance {
        get {
            Init();
            return _instance;
        }
    }
    
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    
    public static InputManager Input { get {return instance._input;} }
    public static PoolManager Pool { get { return instance._pool; } }
    public static ResourceManager Resource { get { return instance._resource; } }
    public static SceneManagerEx Scene { get { return instance._scene; } }
    public static SoundManager Sound { get { return instance._sound; } }
    public static UIManager UI { get { return instance._ui; } }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        Init();
    }

    // Update is called once per frame
    private void Update() {
        _input.OnUpdate();
    }

    private static void Init() {
        if (_instance == null) {
            GameObject go = GameObject.Find("@Manager");
            if (go == null) {
                go = new GameObject { name = "@Manager" };
                go.AddComponent<Manager>();
            }

            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Manager>();
            
            _instance._pool.Init();
            _instance._sound.Init();
        }
    }
}
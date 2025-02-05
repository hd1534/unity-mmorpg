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
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    public static InputManager Input { get {return instance._input;} }
    public static ResourceManager Resource { get { return instance._resource; } }
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
            var go = GameObject.Find("@Manager");
            if (go == null) {
                go = new GameObject { name = "@Manager" };
                go.AddComponent<Manager>();
            }

            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Manager>();
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx{

    public BaseScene CurrentScene { get {return GameObject.FindFirstObjectByType<BaseScene>();} }
    
    public void LoadScene(Define.Scene type) {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type) {
        return Enum.GetName(typeof(Define.Scene), type);
    }
}
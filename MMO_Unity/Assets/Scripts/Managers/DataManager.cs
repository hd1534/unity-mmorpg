using System;
using System.Collections.Generic;
using UnityEngine;


public interface ILoader<Key, Value> {
    Dictionary<Key, Value> MakeDict();
}

public class DataManager  {
    public Dictionary<int, Stat> StatDict { get; protected set; } = new Dictionary<int, Stat>();

    public void Init() {
        StatDict = LoadJson<StatData, int, Stat>("StatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value> {
        TextAsset textAsset = Manager.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}

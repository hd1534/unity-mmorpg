using System;
using TMPro;
using UnityEngine;

public class UI_Inven_item : UI_Base {
    private string _name;
    enum GameObjects {
        ItemIcon,
        ItemName,
    }
    
    private void Start() {
        Init();
    }

    public override void Init() {
        Bind<GameObject>(typeof(GameObjects));

        Debug.Log($"{_name}'s Init called");
        Get<GameObject>((int)GameObjects.ItemName).GetComponent<TextMeshProUGUI>().text = _name;
        
        Get<GameObject>((int)GameObjects.ItemIcon).AddUIEvent(data => Debug.Log($"{_name} clicked"));
    }

    public void SetInfo(string name) {
        _name = name;

        Debug.Log($"{_name}'s SetInfo called");
    }
}

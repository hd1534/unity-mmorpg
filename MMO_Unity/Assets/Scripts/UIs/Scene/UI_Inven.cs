using System;
using UnityEngine;

public class UI_Inven : UI_Scene {

    enum GameObjects {
        GridPanel,
    }

    private void Start() {
        Init();
    }

    public override void Init() {
        base.Init();
        
        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform) {
            Manager.Resource.Destroy(child.gameObject);
        }

        for (int i = 0; i < 8; i++) {
            GameObject item = Manager.Resource.Instantiate("UIs/Scenes/UI_Inven_Item", gridPanel.transform);

            UI_Inven_item invenItem = Util.GetOrAddComponent<UI_Inven_item>(item);
            invenItem.SetInfo($"item {i}");
        }
    }
}
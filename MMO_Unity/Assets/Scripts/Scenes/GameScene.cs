using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene {
    protected override void Init() {
        base.Init();
        
        SceneType = Define.Scene.Game;
        Manager.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int,Stat> dict = Manager.Data.StatDict;
    }

    public override void Clear() {
        
    }
}

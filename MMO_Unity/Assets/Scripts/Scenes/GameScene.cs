using UnityEngine;

public class GameScene : BaseScene {
    protected override void Init() {
        base.Init();
        
        SceneType = Define.Scene.Game;
        Manager.UI.ShowSceneUI<UI_Inven>();

    }

    public override void Clear() {
        
    }
}

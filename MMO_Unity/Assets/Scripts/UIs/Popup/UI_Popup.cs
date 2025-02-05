using UnityEngine;

public class UI_Popup : UI_Base
{
    public override void Init() {
        throw new System.NotImplementedException();
    }

    public virtual void ClosePopUI() {
        Manager.UI.ClosePopupUI(this);
    }
}

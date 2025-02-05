using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    int _score = 10;

    enum Buttons {
        PointButton,
    }

    enum Texts {
        PointText,
        ScoreText,
    }

    enum Images {
        ItemIcon,
    }
    
    void Start() {
        Init();
    }

    public override void Init() {
        base.Init();
        
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        
        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);
        
        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        BindIEvent(go, data => go.transform.position = data.position, Define.UIEvent.Drag);
    }

    void OnButtonClicked(PointerEventData eventData) {
        _score++;
        GetText((int)Texts.ScoreText).text = $"점수: {_score}";
    }
}

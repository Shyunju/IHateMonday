using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lobby : UI_Scene
{
    #region Enums
    enum Texts
    {
        TitleText,
    }
    enum Buttons
    {
        StartButton,
    }
    // ���� ������Ʈ -> GameObjects
    // �̹��� -> Images
    #endregion

    void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        #region Bind
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        //BindImage(typeof(Images));
        //BindObject(typeof(GameObjects));
        #endregion

        GetText((int)Texts.TitleText).text = "�������� ���ڳ�?";
        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickedStartButton);

        return true;
    }

    void OnClickedStartButton()
    {
        //Managers.Sound.Play("ClickBtnEff"); 
        Managers.Scene.ChangeScene(Define.Scene.DungeonScene);
    }
}
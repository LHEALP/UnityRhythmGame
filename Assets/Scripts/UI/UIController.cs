using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    static UIController instance;
    public static UIController Instance
    {
        get { return instance; }
    }

    public Func<string, UIActor> find;
    Dictionary<string, UIActor> uiObjectDic = new Dictionary<string, UIActor>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Init()
    {
        find = FindUI;

        UIObject[] objs = FindObjectsOfType<UIObject>();
        foreach (UIObject obj in objs)
        {
            uiObjectDic.Add(obj.Name, new UIActor(obj, null));
        }

        uiObjectDic["UI_S_SelectMode"].action = GameManager.Instance.ChangeMode;

        uiObjectDic["UI_G_Judgement"].action = Score.Instance.Ani;
        uiObjectDic["UI_G_Combo"].action = Score.Instance.Ani;

        //uiObjectDic["UI_E_ProgressBar"].action = Editor.Instance.Progress;
        uiObjectDic["UI_E_Play"].action = Editor.Instance.Play;
        uiObjectDic["UI_E_Stop"].action = Editor.Instance.Stop;
    }

    public UIActor FindUI(string uiName)
    {
        UIActor actor = uiObjectDic[uiName];
        if (actor.action != null)
            actor.action.Invoke(actor.uiObject);
        return actor;
    }

    public UIActor GetUI(string uiName)
    {
        UIActor actor = uiObjectDic[uiName];
        return actor;
    }
}

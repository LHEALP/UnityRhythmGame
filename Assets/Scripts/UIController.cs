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

    public Action<string> findAction;
    delegate void uiDelegate();
    Dictionary<string, UIActor> uiObjectDic = new Dictionary<string, UIActor>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        findAction = FindUI;

        UIObject[] objs = FindObjectsOfType<UIObject>();
        foreach (UIObject obj in objs)
        {
            uiObjectDic.Add(obj.Name, new UIActor(obj, null));
        }

        uiObjectDic["Btn_Play"].action = Play;
    }

    void FindUI(string uiName)
    {
        UIActor actor = uiObjectDic[uiName];
        if (actor.action != null)
            actor.action.Invoke(actor.uiObject);
    }

    void Play(UIObject uiObject)
    {
        Debug.Log("PlayAction");
    }
}

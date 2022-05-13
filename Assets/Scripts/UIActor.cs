using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActor
{
    public UIObject uiObject;
    public Action<UIObject> action;

    public UIActor(UIObject uiObject, Action<UIObject> action = null)
    {
        this.uiObject = uiObject;
        this.action = action;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActor
{
    public delegate void uiDele();
    public UIObject uiObject;
    public uiDele uiDelegate;

    public UIActor(UIObject uiObject, uiDele uiDelegate = null)
    {
        this.uiObject = uiObject;
        this.uiDelegate = uiDelegate;
    }
}

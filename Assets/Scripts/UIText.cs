using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIText : UIObject
{
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        Debug.Log(Name);
    }

    public void SetText(string _text)
    {
        text.text = _text;
    }
}

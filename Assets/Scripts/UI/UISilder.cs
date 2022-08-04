using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISilder : UIObject
{
    public Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        //slider.onValueChanged.AddListener(OnValue);
    }

    public void OnValue(float value)
    {
        UIController.Instance.find.Invoke(Name);
    }
}

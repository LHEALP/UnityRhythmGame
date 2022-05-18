using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour
{
    public string Name { get; set; }
    public RectTransform rect;

    private void Awake()
    {
        Name = transform.name;
        rect = GetComponent<RectTransform>();
    }
}

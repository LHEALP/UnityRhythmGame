using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour
{
    public string Name { get; set; }

    private void Awake()
    {
        Name = transform.name;
    }
}

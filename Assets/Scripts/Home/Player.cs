using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public string ClassName { set; get; }

    public int PlayerClass { set; get; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}

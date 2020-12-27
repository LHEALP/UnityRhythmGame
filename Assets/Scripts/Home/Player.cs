using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public string ClassName { set; get; }

    public int PlayerClass { set; get; }


    public bool isEditMode = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}

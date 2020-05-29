using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Sheet sheet;
    SpriteRenderer background;
    

    void Start()
    {
        sheet = GameObject.Find("Sheet").GetComponent<Sheet>();
        background = gameObject.GetComponent<SpriteRenderer>();
        //Debug.Log(sheet.Title + "/" + sheet.ImageFileName);

        background.sprite = Resources.Load<Sprite>(sheet.Title + "/" + sheet.ImageFileName);

    }

}

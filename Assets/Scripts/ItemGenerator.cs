using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemGenerator : MonoBehaviour
{
    static ItemGenerator instance;
    public static ItemGenerator Instance
    {
        get
        {
            return instance;
        }
    }

    List<GameObject> items = new List<GameObject>();
    public GameObject item;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Init()
    {
        Image cover = item.transform.GetChild(0).GetComponent<Image>();
        TextMeshProUGUI level = item.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI title = item.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI artist = item.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        foreach (var sheet in GameManager.Instance.sheets)
        {
            cover.sprite = sheet.Value.img;
            level.text = "Normal";
            title.text = sheet.Value.title;
            artist.text = sheet.Value.artist;

            GameObject go = Instantiate(item, transform.GetChild(0));
            go.name = sheet.Value.title;
            items.Add(go);
        }
    }
}

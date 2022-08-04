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

    int posX = 0;

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
            level.text = "";
            title.text = sheet.Value.title;
            artist.text = sheet.Value.artist;

            GameObject go = Instantiate(item, transform);
            go.name = sheet.Value.title;
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.anchoredPosition3D = new Vector3(posX, 0f, 0f);
            items.Add(go);

            posX += 1920;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    static ItemController instance;
    public static ItemController Instance
    {
        get
        {
            return instance;
        }
    }

    public RectTransform rect;
    public RectTransform dest;

    public int page = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        dest.anchoredPosition3D = new Vector3(1920f, 0f, 0f);
    }

    public void Move(float dir)
    {
        page = Mathf.Clamp(page + (int)dir, 0, GameManager.Instance.sheets.Count - 1);

        dest.anchoredPosition3D = Vector3.right * page * -1 * 1920;
        StartCoroutine(AniPreset.Instance.IEAniMoveToTarget(rect, dest, 4f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public int index;
    bool isActive;
    GameObject[] lines = new GameObject[64];
    
    void Start()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = transform.GetChild(i).gameObject;
            lines[i].name = $"Line_{i}";
            lines[i].SetActive(false);

            lines[i].transform.localPosition = new Vector3(0f, i * 0.25f, 0f);
        }

        for (int i = 0 ; i < lines.Length; i++)
        {
            if (i % 16 == 0)
            {
                lines[i].SetActive(true);
            }
        }

        EditorController.Instance.GridSnapListener -= ChangeSnap;
        EditorController.Instance.GridSnapListener += ChangeSnap;
    }

    void Update()
    {
        // 자기 차례와 멀면 비활성화 상태
        int currentBar = Editor.Instance.currentBar;
        if (index >= currentBar - 3 && index <= currentBar + 3)
        {
            isActive = true;
            ChangeSnap(Editor.Instance.Snap);
        }
        else
        {
            isActive = false;
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].SetActive(false);
            }
        }
    }

    void ChangeSnap(int snap)
    {
        if (isActive)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (i % snap == 0)
                    lines[i].SetActive(true);
                else
                    lines[i].SetActive(false);
            }
        }
    }
}

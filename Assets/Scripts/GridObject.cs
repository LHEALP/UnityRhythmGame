using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    GameObject[] lines = new GameObject[64];
    
    void Start()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = transform.GetChild(i).gameObject;
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

    void ChangeSnap(int snap)
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

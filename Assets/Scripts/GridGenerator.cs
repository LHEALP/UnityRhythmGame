using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject grid;

    readonly int gridInterval = 16;

    List<GameObject> gridList = new List<GameObject>();


    public void Init()
    {
        int barCount = (int)(AudioManager.Instance.Length * 1000 / GameManager.Instance.sheets[GameManager.Instance.title].BarPerMilliSec);

        if (gridList.Count < barCount)
        {
            int failCount = barCount - gridList.Count;
            for (int i = gridList.Count; i < failCount; i++)
            {
                GameObject obj = Instantiate(grid, transform);
                obj.SetActive(false);
                gridList.Add(obj);
            }
        }

        for (int i = 0; i < gridList.Count; i++)
        {
            GameObject obj = gridList[i];
            obj.name = $"Grid_{i}";
            obj.GetComponent<GridObject>().index = i;
            obj.transform.localPosition = Vector3.up * i * gridInterval;
            obj.SetActive(true);
        }
    }

    public void InActivate()
    {
        foreach (GameObject obj in gridList)
        {
            obj.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject grid;

    readonly int gridInterval = 16;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init()
    {
        int count = (int)(AudioManager.Instance.Length * 1000 / GameManager.Instance.sheets[GameManager.Instance.title].BarPerMilliSec);

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(grid, transform);
            obj.transform.position = Vector3.up * i * gridInterval;
        }        
    }
}

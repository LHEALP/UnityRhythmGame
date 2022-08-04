using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SheetLoader : MonoBehaviour
{
    static SheetLoader instance;
    public static SheetLoader Instance
    {
        get
        {
            return instance;
        }
    }

    public string pathSheet;
    public int sheetCount;
    public bool bLoadFinish;
    int remain;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Init()
    {
        pathSheet = $"{Application.dataPath}/Sheet";
        
        if (Directory.Exists(pathSheet))
        {
            DirectoryInfo d = new DirectoryInfo(pathSheet);
            sheetCount = remain = d.GetFiles().Length;
        }

        StartCoroutine(IELoad());
    }

    IEnumerator IELoad()
    {
        DirectoryInfo di = new DirectoryInfo(pathSheet);
        foreach (DirectoryInfo d in di.GetDirectories())
        {
            yield return Parser.Instance.IEParse(d.Name);
            GameManager.Instance.sheets.Add(d.Name, Parser.Instance.sheet);
            if (--remain <= 0)
                bLoadFinish = true;
        }
    }
}

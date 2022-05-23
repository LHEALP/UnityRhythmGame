using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniPreset : MonoBehaviour
{
    static AniPreset instance;
    public static AniPreset Instance
    {
        get { return instance; }
    }

    Dictionary<string, bool> signalDic = new Dictionary<string, bool>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Join(string uiName)
    {
        signalDic.Add(uiName, true);
    }

    public void PlayPop(string uiName, RectTransform rect)
    {
        if (signalDic[uiName])
            StartCoroutine(IEAniPop(uiName, rect));
    }

    public IEnumerator IEAniPop(string uiName, RectTransform rect)
    {
        signalDic[uiName] = false;
        Vector3 originPos = rect.anchoredPosition3D;

        float time = 0f;
        while (time < 1f)
        {
            rect.anchoredPosition3D = new Vector3(originPos.x, originPos.y + 8f * Mathf.Sin(time), originPos.z);

            time += Time.deltaTime * 12;
            yield return null;
        }
        rect.anchoredPosition3D = originPos;
        signalDic[uiName] = true;
    }
}

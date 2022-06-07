using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : MonoBehaviour
{
    Judgement judgement;

    public GameObject judgeLine;
    SpriteRenderer sr;
    UIText text;

    Coroutine coPopup;

    // Start is called before the first frame update
    void Start()
    {
        judgement = FindObjectOfType<Judgement>();
        sr = judgeLine.GetComponent<SpriteRenderer>();
        sr.color = sr.color = new Color(1, 1, 1, 0);
    }

    public void Down()
    {
        judgement.judgeTimeFromUserSetting -= 25;
        judgeLine.transform.position += Vector3.down * 0.025f;

        text = UIController.Instance.FindUI("UI_G_SyncTime").uiObject as UIText;

        int time = Mathf.Abs(judgement.judgeTimeFromUserSetting);
        string txt = $"{time} ms";
        if (judgement.judgeTimeFromUserSetting < 0)
            txt = $"{time} ms SLOW";
        else if (judgement.judgeTimeFromUserSetting > 0)
            txt = $"{time} ms FAST";

        text.SetText(txt);
        text.GetComponent<RectTransform>().anchoredPosition3D += Vector3.down * 2.5f;

        if (coPopup != null)
            StopCoroutine(coPopup);
        coPopup = StartCoroutine(IEPopup());
    }

    public void Up()
    {
        judgement.judgeTimeFromUserSetting += 25;
        judgeLine.transform.position += Vector3.up * 0.025f;

        text = UIController.Instance.FindUI("UI_G_SyncTime").uiObject as UIText;

        int time = Mathf.Abs(judgement.judgeTimeFromUserSetting);
        string txt = $"{time} ms";
        if (judgement.judgeTimeFromUserSetting < 0)
            txt = $"{time} ms SLOW";
        else if (judgement.judgeTimeFromUserSetting > 0)
            txt = $"{time} ms FAST";

        text.SetText(txt);
        text.GetComponent<RectTransform>().anchoredPosition3D += Vector3.up * 2.5f;

        if (coPopup != null)
            StopCoroutine(coPopup);
        coPopup = StartCoroutine(IEPopup());
    }

    IEnumerator IEPopup()
    {
        sr.color = new Color(1, 1, 1, 0);
        text.SetColor(sr.color);
        float time = 0f;
        float speed = 4f;
        while (time < 1f)
        {
            sr.color = new Color(1, 1, 1, time);
            text.SetColor(sr.color);

            time += Time.deltaTime * speed;
            yield return null;
        }
        sr.color = new Color(1, 1, 1, 1);
        text.SetColor(sr.color);
        yield return new WaitForSeconds(1f);

        time = 0f;
        while (time < 1f)
        {
            sr.color = new Color(1, 1, 1, 1 - time);
            text.SetColor(sr.color);

            time += Time.deltaTime * speed;
            yield return null;
        }
        sr.color = new Color(1, 1, 1, 0);
        text.SetColor(sr.color);
    }
}

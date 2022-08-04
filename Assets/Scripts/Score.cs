using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ScoreData
{
    public int great;
    public int good;
    public int miss;
    public int fastMiss; // 빨리 입력해서 미스
    public int longMiss; // 롱노트 완성 실패, miss 카운트는 하지 않음

    public string[] judgeText;
    public Color[] judgeColor;
    public JudgeType judge;
    public int combo;
    public int score 
    { 
        get
        {
            return (great * 500) + (good * 200);
        }
        set
        {
            score = value;
        }
    }
}

public class Score : MonoBehaviour
{
    static Score instance;
    public static Score Instance
    {
        get { return instance; }
    }

    public ScoreData data;

    UIText uiJudgement;
    UIText uiCombo;
    UIText uiScore;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Init()
    {
        uiJudgement = UIController.Instance.FindUI("UI_G_Judgement").uiObject as UIText;
        uiCombo = UIController.Instance.FindUI("UI_G_Combo").uiObject as UIText;
        uiScore = UIController.Instance.FindUI("UI_G_Score").uiObject as UIText;

        AniPreset.Instance.Join(uiJudgement.Name);
        AniPreset.Instance.Join(uiCombo.Name);
        AniPreset.Instance.Join(uiScore.Name);
    }

    public void Clear()
    {
        data = new ScoreData();
        data.judgeText = Enum.GetNames(typeof(JudgeType));
        data.judgeColor = new Color[3] { Color.blue, Color.yellow, Color.red };
        uiJudgement.SetText("");
        uiCombo.SetText("");
        uiScore.SetText("0");
    }

    public void SetScore()
    {
        uiJudgement.SetText(data.judgeText[(int)data.judge]);
        uiJudgement.SetColor(data.judgeColor[(int)data.judge]);
        uiCombo.SetText($"{data.combo}");
        uiScore.SetText($"{data.score}");

        AniPreset.Instance.PlayPop(uiJudgement.Name, uiJudgement.rect);
        AniPreset.Instance.PlayPop(uiCombo.Name, uiCombo.rect);
        //UIController.Instance.find.Invoke(uiJudgement.Name);
        //UIController.Instance.find.Invoke(uiCombo.Name);
    }

    public void Ani(UIObject uiObject)
    {
        //AniPreset.Instance.PlayPop(uiObject.Name, uiObject.rect);
    }
}
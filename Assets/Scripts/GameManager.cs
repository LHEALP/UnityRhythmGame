using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public Sheet sheet;
    public Score score;
    float speed = 1.0f;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = Mathf.Clamp(value, 1.0f, 5.0f);
        }
    }

    public List<GameObject> canvases = new List<GameObject>();
    enum Canvas
    {
        Title,
        Select,
        SFX,
        GameBGA,
        Game,
        Result,
    }
    CanvasGroup sfxFade;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        score = new Score();

        foreach (GameObject go in canvases)
        {
            go.SetActive(true);
        }
        sfxFade = canvases[(int)Canvas.SFX].GetComponent<CanvasGroup>();
        sfxFade.alpha = 1f;

        UIController.Instance.Init();
        score.Init();

        StartCoroutine(IETitle());
    }


    // 리스트에서 프리뷰하기 위해 아이템을 한 번 눌렀을 때
    public void Select()
    {
        // UI에 앨범이미지, 음악등 미리보기

        // 리스트UI에서 클릭되면 데이터 받아와서 Insert에 AudioClip 넘겨줘서 바인딩
        //AudioManager.Instance.Insert();
        //AudioManager.Instance.Play();

        // 아무튼 앨범이미지 바인딩 등
    }

    // 리스트에서 플레이하기 위해 아이템을 두 번 눌렀을 때(게임 시작)
    public void Play()
    {
        StartCoroutine(IEInitPlay());
    }

    IEnumerator IETitle()
    {
        // UIObject들이 자기자신을 캐싱할때까지 여유를 주고 비활성화
        yield return new WaitForSeconds(2f);
        canvases[(int)Canvas.Game].SetActive(false);
        canvases[(int)Canvas.GameBGA].SetActive(false);
        canvases[(int)Canvas.Result].SetActive(false);
        canvases[(int)Canvas.Select].SetActive(false);

        // 화면 페이드 인
        yield return StartCoroutine(AniPreset.Instance.IEAniFade(sfxFade, false, 1f));

        // 타이틀 인트로 재생
        canvases[(int)Canvas.Title].GetComponent<Animation>().Play();
        yield return new WaitForSeconds(5f);

        // 화면 페이드 아웃
        yield return StartCoroutine(AniPreset.Instance.IEAniFade(sfxFade, true, 2f));
        canvases[(int)Canvas.Title].SetActive(false);

        // 선택화면(미구현), 일단 플레이로 바로
        Select();
        Play();
    }

    IEnumerator IEInitPlay()
    {
        // 화면 페이드 아웃
        //yield return StartCoroutine(AniPreset.Instance.IEAniFade(sfxFade, true, 2f));

        // Sheet 파싱
        yield return Parser.Instance.IEParse("Heart Shaker");
        sheet.Init();

        // Audio 삽입
        AudioManager.Instance.Insert(sheet.clip);

        // Game UI 켜기
        canvases[(int)Canvas.Game].SetActive(true);

        // BGA 켜기
        canvases[(int)Canvas.GameBGA].SetActive(true);

        // 판정 초기화
        FindObjectOfType<Judgement>().Init();

        // 점수 초기화
        score.Clear();

        // 화면 페이드 인
        yield return StartCoroutine(AniPreset.Instance.IEAniFade(sfxFade, false, 2f));

        // Audio 재생
        AudioManager.Instance.Play();

        // Note 생성
        NoteGenerator.Instance.StartGen();

        // End 알리미
        StartCoroutine(IEEndPlay());
    }

    // 게임 끝
    IEnumerator IEEndPlay()
    {
        while (true)
        {
            if (!AudioManager.Instance.IsPlaying())
            {
                break;
            }
            yield return new WaitForSeconds(1f);
        }

        // 화면 페이드 아웃
        yield return StartCoroutine(AniPreset.Instance.IEAniFade(sfxFade, true, 2f));
        canvases[(int)Canvas.Game].SetActive(false);
        canvases[(int)Canvas.GameBGA].SetActive(false);
        canvases[(int)Canvas.Result].SetActive(true);

        UIText rscore = UIController.Instance.FindUI("UI_R_Score").uiObject as UIText;
        UIText rgreat = UIController.Instance.FindUI("UI_R_Great").uiObject as UIText;
        UIText rgood = UIController.Instance.FindUI("UI_R_Good").uiObject as UIText;
        UIText rmiss = UIController.Instance.FindUI("UI_R_Miss").uiObject as UIText;

        rscore.SetText(score.data.score.ToString());
        rgreat.SetText(score.data.great.ToString());
        rgood.SetText(score.data.good.ToString());
        rmiss.SetText(score.data.miss.ToString());

        UIImage rBG = UIController.Instance.FindUI("UI_R_BG").uiObject as UIImage;
        rBG.SetSprite(sheet.img);

        // 화면 페이드 인
        yield return StartCoroutine(AniPreset.Instance.IEAniFade(sfxFade, false, 2f));

    }
}

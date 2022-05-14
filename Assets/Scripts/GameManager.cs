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

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        score = new Score();
        UIController.Instance.Init();
        score.Init();

        Select();
        Play();
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

    IEnumerator IEInitPlay()
    {
        // 화면 페이드 아웃

        // 파싱, 생성 등
        yield return Parser.Instance.IEParse("Heart Shaker");
        sheet.Init();
        AudioManager.Instance.Insert(sheet.clip);
        Judgement judgement = FindObjectOfType<Judgement>();
        judgement.Init();
        NoteGenerator.Instance.StartGen();
        //NoteGenerator.Instance.Gen(sheet);

        // 화면 페이드 인

        // 게임 재생
        AudioManager.Instance.Play();
        // 노트 하강
    }
}

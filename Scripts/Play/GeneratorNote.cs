using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GeneratorNote : MonoBehaviour
{
    //노트 생성 스크립트입니다.
    SheetParser sheetParser;
    BeatBar beatBar;
    Sync sync;
    Transform startPos;
    Sheet sheet;

    public GameObject notePrefab;
    public GameObject BeatBarPrefab;

    float bpm;
    float beat;
    float bit;

    // 노트간격
    float noteCorrectRate = 0.001f; // 악보 시간이 밀리세컨드 단위 - 좌표생성을 위한 보정값

    // 노트 및 바 미리 연산
    float notePosY;
    float noteStartPosY;

    public bool isGenFin = false;
    
    void Start()
    {
        beatBar = GameObject.Find("BeatBar").GetComponent<BeatBar>();
        sync = GameObject.Find("Sync").GetComponent<Sync>();
        sheet = GameObject.Find("Sheet").GetComponent<Sheet>();
        startPos = GameObject.Find("StartPos").GetComponent<Transform>();
        notePosY = sync.scrollSpeed;
        noteStartPosY = sync.scrollSpeed * 3.0f;
    }

    void Update()
    {

        if (isGenFin.Equals(false))
        {
            StartCoroutine(GenNote());
            //StartCoroutine(GenBeatBar());
            isGenFin = true;
        }
    }


    // 노트생성
    IEnumerator GenNote()
    {
        foreach(float noteTime in sheet.noteList1)
            Instantiate(notePrefab, new Vector3(2.3f, noteStartPosY + sync.offset +  notePosY * (noteTime * noteCorrectRate)), Quaternion.identity);
        foreach (float noteTime in sheet.noteList2)
            Instantiate(notePrefab, new Vector3(2.96f, noteStartPosY + sync.offset +  notePosY * (noteTime * noteCorrectRate)), Quaternion.identity);
        foreach (float noteTime in sheet.noteList3)
            Instantiate(notePrefab, new Vector3(3.62f, noteStartPosY + sync.offset +  notePosY * (noteTime * noteCorrectRate)), Quaternion.identity);
        foreach (float noteTime in sheet.noteList4)
            Instantiate(notePrefab, new Vector3(4.28f, noteStartPosY + sync.offset +  notePosY * (noteTime * noteCorrectRate)), Quaternion.identity);

        yield return new WaitForEndOfFrame();
    }
    IEnumerator GenBeatBar()
    {
        Instantiate(BeatBarPrefab, new Vector3(4, sync.offset + sync.userSpeedRate * notePosY), Quaternion.identity);
        
        for (int i = 1; i <= beatBar.barCnt; i++  )
        {
            Instantiate(BeatBarPrefab, new Vector3(4, 1 + sync.offset +  (sync.userSpeedRate * notePosY * i * sync.barPerSec)), Quaternion.identity);
        }

        yield return new WaitForEndOfFrame();

    }

}

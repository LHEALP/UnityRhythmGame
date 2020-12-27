using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GeneratorNote : MonoBehaviour
{
    //노트 생성 스크립트입니다.
    Sync sync;
    Transform startPos;
    Sheet sheet;

    public GameObject notePrefab;

    // 노트간격
    float noteCorrectRate = 0.001f; // 악보 시간이 밀리세컨드 단위 - 좌표생성을 위한 보정값

    // 노트 및 바 미리 연산
    float notePosY;
    float noteStartPosY;
    float offset;

    public bool isGenFin = false;
    
    void Start()
    {
        sync = GameObject.Find("Sync").GetComponent<Sync>();
        sheet = GameObject.Find("Sheet").GetComponent<Sheet>();
        startPos = GameObject.Find("StartPos").GetComponent<Transform>();
        notePosY = sync.scrollSpeed;
        noteStartPosY = sync.scrollSpeed * 3.0f;
        offset = sync.offset;
    }

    void Update()
    {
        if (isGenFin.Equals(false))
        {
            GenNote();
            isGenFin = true;
        }
    }

    // 노트생성
    void GenNote()
    {
        foreach(float noteTime in sheet.noteList1)
            Instantiate(notePrefab, new Vector3(2.3f, noteStartPosY + offset +  notePosY * (noteTime * noteCorrectRate)), Quaternion.identity);
        foreach (float noteTime in sheet.noteList2)
            Instantiate(notePrefab, new Vector3(2.96f, noteStartPosY + offset +  notePosY * (noteTime * noteCorrectRate)), Quaternion.identity);
        foreach (float noteTime in sheet.noteList3)
            Instantiate(notePrefab, new Vector3(3.62f, noteStartPosY + offset +  notePosY * (noteTime * noteCorrectRate)), Quaternion.identity);
        foreach (float noteTime in sheet.noteList4)
            Instantiate(notePrefab, new Vector3(4.28f, noteStartPosY + offset +  notePosY * (noteTime * noteCorrectRate)), Quaternion.identity);
    }
}

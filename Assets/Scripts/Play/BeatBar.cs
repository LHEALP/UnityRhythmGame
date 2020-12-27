using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BeatBar : MonoBehaviour
{
    // 바의 정보를 담은 스크립트입니다.
    Player player;
    Sheet sheet;
    Sync sync;
    Transform judgebar;
    Vector3 judgePos;

    float speed; // 기본배속
    public float barCnt; //바갯수

    void Start()
    {
        player = FindObjectOfType<Player>();

        if (!player.isEditMode)
        {
            judgebar = GameObject.Find("JudgeBar").GetComponent<Transform>();
            judgePos = judgebar.transform.position;
        }
        sheet = GameObject.Find("Sheet").GetComponent<Sheet>();
        sync = GameObject.Find("Sync").GetComponent<Sync>();
        barCnt = sheet.BarCnt;
        speed = sync.scrollSpeed * sync.userSpeedRate;
    }

    void Update()
    {
        StartCoroutine(MoveBeatBar());
    }

    // 바 하락
    IEnumerator MoveBeatBar()
    {
        if (transform.position.y > judgePos.y)
        {
            transform.Translate(Vector3.down * speed* Time.smoothDeltaTime);
        }
        else
        {
            gameObject.SetActive(false);
        }

        yield return null;
    }
}

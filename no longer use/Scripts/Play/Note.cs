using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Note : MonoBehaviour
{
    // 노트 정보를 담은 스크립트입니다.
    Sync sync;
    Transform judgebar;
    Vector3 judgePos;
    Transform destoryPos;
    Vector3 desPos;


    // 숏노트, 롱노트
    float speed;


    // 판정 및 점수
    float distance;
    public int score;

    void Start()
    {
        judgebar = GameObject.Find("JudgeBar").GetComponent<Transform>();
        judgePos = judgebar.transform.position;
        destoryPos = GameObject.Find("DestroyNote").GetComponent<Transform>();
        desPos = destoryPos.transform.position;
        sync = GameObject.Find("Sync").GetComponent<Sync>();
        speed = sync.scrollSpeed * sync.userSpeedRate;
    }

    void Update()
    {
        StartCoroutine(MoveNote());
    }

    // 노트 하락
    IEnumerator MoveNote()
    {
        if (transform.position.y > desPos.y)
        {
            transform.Translate(Vector3.down * speed * Time.smoothDeltaTime);
        }
        else
        {
            gameObject.SetActive(false);
        }

        yield return null;
    }

    public void ChangeNoteSpeed(int key)
    {
        if(key.Equals(0))
        {
            transform.position = new Vector3(transform.position.x , transform.position.y * 1.1f);
            speed *= 1.1f;
            Mathf.Floor(speed);
        }
        else if(key.Equals(1))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y / 1.1f);
            speed /= 1.1f;
            Mathf.Floor(speed);
        }
    }


    // 싱크 확인용 메소드 -> Sync.cs
    public void RotateNote()
    { 
            transform.Rotate(Vector3.back * 45f);
    }
}

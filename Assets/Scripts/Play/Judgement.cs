using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    Sheet sheet;
    Score score;
    SongManager songManager;

    float currentTime;      // 현재 음악샘플값

    // 숏
    float currentNoteTime1;
    float currentNoteTime2;
    float currentNoteTime3;
    float currentNoteTime4;

    // 판정 배율(frequency값에 기반)
    float greatRate = 3025f;
    float goodRate = 7050f;
    float missRate = 16100f;

    int laneNum;

    Queue<float> judgeLane1 = new Queue<float>();
    Queue<float> judgeLane2 = new Queue<float>();
    Queue<float> judgeLane3 = new Queue<float>();
    Queue<float> judgeLane4 = new Queue<float>();

    void Start()
    {
        sheet = GameObject.Find("Sheet").GetComponent<Sheet>();
        SetQueue();
        songManager = GameObject.Find("SongSelect").GetComponent<SongManager>();
        score = GameObject.Find("Score").GetComponent<Score>();
    }

    void Update()
    {
        // 입력 외 판정(반복문에서 계속 체킹)
        currentTime = songManager.music.timeSamples;

        // 이하 미스판정(판정선 넘어갔을시)
        // 1번 레인. 큐에서 받아온 노트타임을 샘플로 변환
        if (judgeLane1.Count > 0) // 먼저 끝에 도착한 큐가 발생하면 다른큐가 동작하지 않는 현상 발생하므로,카운트 세서 중단시켜야함
        {
            currentNoteTime1 = judgeLane1.Peek();
            currentNoteTime1 = currentNoteTime1 * 0.001f * songManager.music.clip.frequency;

            if (currentNoteTime1 + missRate <= currentTime)
            {
                judgeLane1.Dequeue();
                score.ProcessScore(0);
            }
        }
        
        // 2번 레인
        if (judgeLane2.Count > 0)
        {
            currentNoteTime2 = judgeLane2.Peek();
            currentNoteTime2 = currentNoteTime2 * 0.001f * songManager.music.clip.frequency;

            if (currentNoteTime2 + missRate <= currentTime)
            {
                judgeLane2.Dequeue();
                score.ProcessScore(0);
            }
        }

        // 3번레인
        if (judgeLane3.Count > 0)
        {
            currentNoteTime3 = judgeLane3.Peek();
            currentNoteTime3 = currentNoteTime3 * 0.001f * songManager.music.clip.frequency;

            if (currentNoteTime3 + missRate <= currentTime)
            {
                judgeLane3.Dequeue();
                score.ProcessScore(0);
            }
        }

        // 4번 레인
        if (judgeLane4.Count > 0)
        {
            currentNoteTime4 = judgeLane4.Peek();
            currentNoteTime4 = currentNoteTime4 * 0.001f * songManager.music.clip.frequency;

            if (currentNoteTime4 + missRate <= currentTime)
            {
                judgeLane4.Dequeue();
                score.ProcessScore(0);
            }
        }
    }

    // 잘돌아가긴하는데 여러 상황을 만들어야함
    // 예를들면 간접미스 범위 어디까지.

    public void JudgeNote(int laneNum)
    {
        this.laneNum = laneNum;

        // 큐.Peek() 을 사용해, 일단 첫번째값을 제거하지않고 반환받고, 조건에 맞으면 데큐

        if (laneNum.Equals(1))
        {
            // 간접미스(다른 레인에 노트가 없는데 같이 누른경우) 나중에 하자...

            // 노트 체킹 범위
            if (currentNoteTime1 > currentTime - missRate && currentNoteTime1 < currentTime + missRate)
            {
                if (currentNoteTime1 > currentTime - goodRate && currentNoteTime1 < currentTime + goodRate)
                {
                    // 그레잇판정
                    if (currentNoteTime1 > currentTime - greatRate && currentNoteTime1 < currentTime + greatRate)
                    {
                        judgeLane1.Dequeue();
                        score.ProcessScore(2);
                    }
                    // 굿판정
                    else
                    {
                        judgeLane1.Dequeue();
                        score.ProcessScore(1);
                    }
                }
                // 너무 빨리 입력했을때 미스처리
                else
                {
                    judgeLane1.Dequeue();
                    score.ProcessScore(0);
                }
            }
        }
        if (laneNum.Equals(2))
        {
            if (currentNoteTime2 > currentTime - missRate && currentNoteTime2 < currentTime + missRate)
            {
                if (currentNoteTime2 > currentTime - goodRate && currentNoteTime2 < currentTime + goodRate)
                {
                    // 그레잇판정
                    if (currentNoteTime2 > currentTime - greatRate && currentNoteTime2 < currentTime + greatRate)
                    {
                        judgeLane2.Dequeue();
                        score.ProcessScore(2);
                    }
                    // 굿판정
                    else
                    {
                        judgeLane2.Dequeue();
                        score.ProcessScore(1);
                    }
                }
                // 너무 빨리 입력했을때 미스처리
                else
                {
                    judgeLane2.Dequeue();
                    score.ProcessScore(0);
                }
            }
        }
        if (laneNum.Equals(3))
        {
            if (currentNoteTime3 > currentTime - missRate && currentNoteTime3 < currentTime + missRate)
            {
                if (currentNoteTime3 > currentTime - goodRate && currentNoteTime3 < currentTime + goodRate)
                {
                    // 그레잇판정
                    if (currentNoteTime3 > currentTime - greatRate && currentNoteTime3 < currentTime + greatRate)
                    {
                        judgeLane3.Dequeue();
                        score.ProcessScore(2);
                    }
                    // 굿판정
                    else
                    {
                        judgeLane3.Dequeue();
                        score.ProcessScore(1);
                    }
                }
                // 너무 빨리 입력했을때 미스처리
                else
                {
                    judgeLane3.Dequeue();
                    score.ProcessScore(0);
                }
            }
        }
        if (laneNum.Equals(4))
        {
            if (currentNoteTime4 > currentTime - missRate && currentNoteTime4 < currentTime + missRate)
            {
                if (currentNoteTime4 > currentTime - goodRate && currentNoteTime4 < currentTime + goodRate)
                {
                    // 그레잇판정
                    if (currentNoteTime4 > currentTime - greatRate && currentNoteTime4 < currentTime + greatRate)
                    {
                        judgeLane4.Dequeue();
                        score.ProcessScore(2);
                    }
                    // 굿판정
                    else
                    {
                        judgeLane4.Dequeue();
                        score.ProcessScore(1);
                    }
                }
                // 너무 빨리 입력했을때 미스처리
                else
                {
                    judgeLane4.Dequeue();
                    score.ProcessScore(0);
                }
            }
        }
    }

    // 각 레인의 노트 시간들을 큐에 저장
    void SetQueue()
    {
        // 숏
        foreach (float noteTime in sheet.noteList1)
            judgeLane1.Enqueue(noteTime);
        foreach (float noteTime in sheet.noteList2)
            judgeLane2.Enqueue(noteTime);
        foreach (float noteTime in sheet.noteList3)
            judgeLane3.Enqueue(noteTime);
        foreach (float noteTime in sheet.noteList4)
            judgeLane4.Enqueue(noteTime);
    }
    
}

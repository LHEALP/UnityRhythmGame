using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Score
{
    public int great;
    public int good;
    public int miss;
    public int fastMiss; // 빨리 입력해서 미스
    public int longMiss; // 롱노트 완성 실패, miss 카운트는 하지 않음

    public int combo;
    public int score;

    public int Calculate()
    {
        score = (great * 500) + (good * 200);
        return score;
    }
}
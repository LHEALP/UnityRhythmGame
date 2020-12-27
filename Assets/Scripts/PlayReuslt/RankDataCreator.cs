using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class RankDataCreator : MonoBehaviour
{
    ScoreDataCreator scoreDataCreator;
    Player player;
    Score score;

    string strClassScore;
    string strSongScore;

    int className;
    int num;
    
    List<int> classScore = new List<int>();
    List<int> classRank = new List<int>();
    List<int> songScore = new List<int>();
    List<int> songRank = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        scoreDataCreator = GameObject.Find("ScoreDataCreator").GetComponent<ScoreDataCreator>();
        player = GameObject.Find("Player").GetComponent<Player>();
        score = GameObject.Find("Score").GetComponent<Score>();

        className = player.PlayerClass;
        strClassScore = scoreDataCreator.StrClassScore;
        strSongScore = scoreDataCreator.StrSongScore;

        SetClassRank();
        SetSongRank();

        num = className;
    }


    // 학과 랭크 설정
    public void SetClassRank()
    {
        List<int> tClassScore = new List<int>();

        string[] classScoreSplit;
        int rank;

        classScoreSplit = strClassScore.Split(',');
        

        for(int i=0; i<9; i++)
        {
            classScore.Add(Convert.ToInt32(classScoreSplit[i]));
        }

        tClassScore = classScore.ToList();

        tClassScore.Sort();
        tClassScore.Reverse();

        for(int i=0; i<9; i++)
        {
            rank = tClassScore.IndexOf(classScore[i]) + 1;
            classRank.Add(rank);
        }
    }
    public string GetClassRank()
    {
        string rank;
        rank = classRank[className].ToString();

        return rank;
    }

    public void SetSongRank()
    {
        List<int> tSongScore = new List<int>();

        string[] songScoreSplit;
        int rank;

        songScoreSplit = strSongScore.Split(',');

        for (int i = 0; i < 9; i++)
        {
            songScore.Add(Convert.ToInt32(songScoreSplit[i]));
        }

        tSongScore = songScore.ToList();

        tSongScore.Sort();
        tSongScore.Reverse();

        for (int i = 0; i < 9; i++)
        {
            rank = tSongScore.IndexOf(songScore[i]) + 1;
            songRank.Add(rank);
        }
    }

    public string GetSongRank()
    {
        string rank;
        rank = songRank[className].ToString();

        return rank;
    }

    public int mySongScore { private set; get; }
    public int nextSongScore { private set; get; }
    public int sumSongScore { private set; get; }

    public int myClassScore { private set; get; }
    public int nextClassScore { private set; get; }
    public int sumClassScore { private set; get; }

    public void GetNextSongRank()
    {
        mySongScore = songScore[num];

        int first;
        first = songRank.IndexOf(1);

        nextSongScore = songScore[first];
        //Debug.Log(nextSongScore);
        //Debug.Log(mySongScore);
        sumSongScore = nextSongScore - mySongScore;
    }
    


    public void GetNextClassRank()
    {
        myClassScore = classScore[num];

        int first;
        first = classRank.IndexOf(1);

        nextClassScore = classScore[first];

        sumClassScore = nextClassScore - myClassScore;
    }
}

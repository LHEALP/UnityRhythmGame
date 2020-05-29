using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
// Enum Boxing 회피
using Unity.Collections.LowLevel.Unsafe;

public class ScoreDataCreator : MonoBehaviour
{
    Player player;
    Score score;
    Sheet sheet;

    TextAsset textAsset;

    public string StrSongScore { private set; get; }
    public string StrClassScore { private set; get; }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        score = GameObject.Find("Score").GetComponent<Score>();
        sheet = GameObject.Find("Sheet").GetComponent<Sheet>();

        SetClassScoreData(player.PlayerClass);
        SetSongScoreData(sheet.Title, player.PlayerClass);
    }

    // 학과 점수 저장
    public void SetClassScoreData(int className)
    {
        List<int> tScoreList = new List<int>();

        // 새로운 점수를 만들기 위해 기존의 데이터를 불러옴
        textAsset = Resources.Load("dt/Score") as TextAsset;

        // 파일이 있다면
        if (textAsset != null)
        {
            StringReader strReader;
            strReader = new StringReader(textAsset.text);
            string scoreText; string[] scoreTextSplit;
            scoreText = strReader.ReadLine();

            scoreTextSplit = scoreText.Split(',');

            // 기존 스코어와 추가된 스코어 합산
            int tScore;
            tScore = Convert.ToInt32(scoreTextSplit[className]);
            tScore += score.SongScore;

            scoreTextSplit[className] = tScore.ToString();

            // 바뀐 하나의 학과 점수를 제외하고 그대로 다시 기존 데이터를 입력
            StrClassScore = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                scoreTextSplit[0], scoreTextSplit[1], scoreTextSplit[2], scoreTextSplit[3], scoreTextSplit[4],
                scoreTextSplit[5], scoreTextSplit[6], scoreTextSplit[7], scoreTextSplit[8]
                );
        }
        // 파일이 없다면
        else
        {
            for (int i = 0; i < 9; i++)
            {
                tScoreList.Add(0);
            }
            tScoreList[className] = score.SongScore;
            StrClassScore = string.Join(",", tScoreList.ToArray());
        }
        

        FileStream file = new FileStream(Application.dataPath + "/Resources/dt/Score.txt", FileMode.Create);

        StreamWriter writer = new StreamWriter(file, System.Text.Encoding.Unicode);
        writer.WriteLine(StrClassScore);
        writer.Close();
    }

    // 노래별 점수 저장
    public void SetSongScoreData(string songName, int className)
    {

        List<int> tScoreList = new List<int>();

        textAsset = Resources.Load(songName + "/" + songName + "_ScoreData") as TextAsset;

        // 파일이 있다면
        if (textAsset != null)
        {
            StringReader strReader;
            strReader = new StringReader(textAsset.text);
            string scoreText; string[] scoreTextSplit;
            scoreText = strReader.ReadLine();

            scoreTextSplit = scoreText.Split(',');

            // 기존 스코어와 새로운 스코어와의 비교
            int tScore;
            tScore = Convert.ToInt32(scoreTextSplit[className]);

            int maxScore = 0;

            // 기존보다 높다면 바꾸기, 아니라면 그대로
            if (score.SongScore > tScore)
                maxScore = score.SongScore;
            else
                scoreTextSplit[className] = tScore.ToString();

            scoreTextSplit[className] = maxScore.ToString();

            // 바뀐 하나의 학과 점수를 제외하고 그대로 다시 기존 데이터를 입력
            StrSongScore = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                scoreTextSplit[0], scoreTextSplit[1], scoreTextSplit[2], scoreTextSplit[3], scoreTextSplit[4],
                scoreTextSplit[5], scoreTextSplit[6], scoreTextSplit[7], scoreTextSplit[8]
                );
        }
        // 파일이 없다면
        else
        {
            for (int i = 0; i < 9; i++)
            {
                tScoreList.Add(0);
            }
            tScoreList[className] = score.SongScore;
            StrSongScore = string.Join(",", tScoreList.ToArray());
        }

        FileStream file = new FileStream(Application.dataPath + "/Resources/" + songName + "/" + songName + "_ScoreData.txt", FileMode.Create);

        StreamWriter writer = new StreamWriter(file, System.Text.Encoding.Unicode);
        writer.WriteLine(StrSongScore);
        writer.Close();
    }
}

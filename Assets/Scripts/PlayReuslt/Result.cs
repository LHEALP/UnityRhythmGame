using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    Score score;
    Sheet sheet;
    RankDataCreator rankDataCreator;

    Image spriteSongImg;
    Text textSongName;
    Text textSongSheet;
    Text textSongLevel;
    Text textSongJudge;
    Text textSongCombo;
    Text textSongScore;

    /*
    Text textSongRank; Image songWater; Text textSongPercent; Text textSongRankScore;

    Text textClassRank; Image ClassWater; Text textClassPercent; Text textClassRankScore;
    Text textClassName;

    string smySongScore, snextSongScore;
    string smyClassScore, snextClassScore;
    float mySongScore, nextSongScore;
    float myClassScore, nextClassScore;

    float songPer, classPer;*/


    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Score>();
        sheet = GameObject.Find("Sheet").GetComponent<Sheet>();
        //rankDataCreator = GameObject.Find("RankDataCreator").GetComponent<RankDataCreator>();


        spriteSongImg = GameObject.Find("SongImg").GetComponent<Image>();
        textSongName = GameObject.Find("SongName").GetComponent<Text>();
        textSongSheet = GameObject.Find("SongSheet").GetComponent<Text>();
        textSongLevel = GameObject.Find("SongLevel").GetComponent<Text>();
        textSongJudge = GameObject.Find("SongJudge").GetComponent<Text>();
        textSongCombo = GameObject.Find("SongCombo").GetComponent<Text>();
        textSongScore = GameObject.Find("SongScore").GetComponent<Text>();

        //textSongRank = GameObject.Find("SongRank").GetComponent<Text>();
        //songWater = GameObject.Find("SongWater").GetComponent<Image>();
        //textSongPercent = GameObject.Find("SongPercent").GetComponent<Text>();
        //textSongRankScore = GameObject.Find("SongRankScore").GetComponent<Text>();

        //textClassRank = GameObject.Find("ClassRank").GetComponent<Text>();
        //textClassName = GameObject.Find("ClassName").GetComponent<Text>();
        //ClassWater = GameObject.Find("ClassWater").GetComponent<Image>();
        //textClassPercent = GameObject.Find("ClassPercent").GetComponent<Text>();
        //textClassRankScore = GameObject.Find("ClassRankScore").GetComponent<Text>();

        spriteSongImg.sprite = Resources.Load<Sprite>(sheet.Title + "/" + sheet.ImageFileName) as Sprite;
        

        textSongName.text = sheet.Title;
        textSongSheet.text = "sheet by " + sheet.SheetBy;
        textSongLevel.text = sheet.Difficult;

        // 판정부 문자열 연산 최적화
        StringBuilder judge = new StringBuilder();
        judge.Append("GREAT   ");
        judge.Append(score.GreatCnt.ToString());
        judge.Append("\n");
        judge.Append("GOOD    ");
        judge.Append(score.GoodCnt.ToString());
        judge.Append("\n");
        judge.Append("MISS       ");
        judge.Append(score.MissCnt.ToString());
        textSongJudge.text = judge.ToString();

        textSongCombo.text = "MAX Combo  " + score.MaxCombo.ToString();
        textSongScore.text = "점수   " + score.SongScore.ToString();

    }

    void Update()
    {
        /*
        smySongScore = rankDataCreator.mySongScore.ToString();
        snextSongScore = rankDataCreator.nextSongScore.ToString();
        smyClassScore = rankDataCreator.myClassScore.ToString();
        snextClassScore = rankDataCreator.nextClassScore.ToString();


        Single.TryParse(smySongScore, out mySongScore);
        Single.TryParse(snextSongScore, out nextSongScore);
        Single.TryParse(smyClassScore, out myClassScore);
        Single.TryParse(snextClassScore, out nextClassScore);

        rankDataCreator.GetNextSongRank();
        rankDataCreator.GetNextClassRank();

        textSongRank.text = "순위   " + rankDataCreator.GetSongRank() + "등";
        if (rankDataCreator.sumSongScore.Equals(0)) //0이면 1등
        {
            textSongRankScore.text = "순위를 유지해보세요!";
            songWater.fillAmount = 1f;
            textSongPercent.text = rankDataCreator.mySongScore + " / " + rankDataCreator.mySongScore + "   " + "100%";
        }
        else
        {
            textSongRankScore.text = "1등까지 " + rankDataCreator.sumSongScore + " 만큼 남았습니다";
            songWater.fillAmount = mySongScore / nextSongScore;
            songPer = songWater.fillAmount * 100f;
            textSongPercent.text = rankDataCreator.mySongScore + " / " + rankDataCreator.nextSongScore + "   " + songPer.ToString("N2") + "%";
        }

        textClassRank.text = "학과 순위" + rankDataCreator.GetClassRank() + "등";
        if (rankDataCreator.sumClassScore.Equals(0))
        {
            textClassRankScore.text = "순위를 유지해보세요!";
            ClassWater.fillAmount = 1f;
            textClassPercent.text = rankDataCreator.myClassScore + " / " + rankDataCreator.myClassScore + "   " + "100%";
        }
        else
        {
            textClassRankScore.text = "1등까지 " + rankDataCreator.sumClassScore + " 만큼 남았습니다";
            ClassWater.fillAmount = myClassScore / nextClassScore;
            classPer = ClassWater.fillAmount * 100f;
            textClassPercent.text = rankDataCreator.myClassScore + " / " + rankDataCreator.nextClassScore + "   " + classPer.ToString("N2") + "%";
        }*/

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            SelectSong();
        }
    }


    void SelectSong()
    {
        GameObject sheet = GameObject.Find("Sheet");
        GameObject score = GameObject.Find("Score");
        GameObject songSelect = GameObject.Find("SongSelect");
        Destroy(sheet);
        Destroy(score);
        Destroy(songSelect);

        SceneManager.LoadScene("SongSelect");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SheetParser : MonoBehaviour
{
    // sheet를 파싱하는 스크립트 입니다.

    FileInfo fileName = null;
    StreamReader reader = null;

    // 안드
    TextAsset textAsset;
    StringReader strReader;

    Sheet sheet;
    Note note;
    BeatBar beatBar;
    SongManager songManager;

    //string path;
    string sheetText;
    string songName;
    string[] textSplit;

    bool isFirstNote = true;


    void Awake()
    {
        sheet = GameObject.Find("Sheet").GetComponent<Sheet>();
        songManager = GameObject.Find("SongSelect").GetComponent<SongManager>();
        
        textSplit = null;
        sheetText = "";
        songName = songManager.songName;
        //path = Application.persistentDataPath;
        //path = "Assets/Songs/";
        //fileName = new FileInfo(path + songName + ".txt");

        textAsset = Resources.Load(songName + "/" + songName + "_data") as TextAsset;
        strReader = new StringReader(textAsset.text);

        /*
        if (fileName != null)
            reader = fileName.OpenText();
        else
            Debug.Log("File error");
            */
        ParseSheet();
    }
    
    // 불러온 텍스트를 한줄씩 읽어, 원하는 부분 잘라내어 저장
    public void ParseSheet()
    {
        while(sheetText != null)
        //while(!reader.EndOfStream)
        {
            //sheetText = reader.ReadLine();
            sheetText = strReader.ReadLine();
            textSplit = sheetText.Split('=');

            if (textSplit[0].Equals("AudioFileName"))
                sheet.AudioFileName = textSplit[1];
            else if (textSplit[0].Equals("AudioViewTime"))
                sheet.AudioViewTime = textSplit[1];
            else if (textSplit[0].Equals("ImageFileName"))
                sheet.ImageFileName = textSplit[1];
            else if (textSplit[0].Equals("BPM"))
                sheet.Bpm = Single.Parse(textSplit[1]);
            else if (textSplit[0].Equals("Offset"))
                sheet.Offset = Single.Parse(textSplit[1]);
            else if (textSplit[0].Equals("Beat"))
                sheet.Beat = Int32.Parse(textSplit[1]);
            else if (textSplit[0].Equals("Bit"))
                sheet.Bit = Int32.Parse(textSplit[1]);
            else if (textSplit[0].Equals("Bar"))
                sheet.BarCnt = Int32.Parse(textSplit[1]);
            else if (textSplit[0].Equals("Title"))
                sheet.Title = textSplit[1];
            else if (textSplit[0].Equals("Artist"))
                sheet.Artist = textSplit[1];
            else if (textSplit[0].Equals("Source"))
                sheet.Source = textSplit[1];
            else if (textSplit[0].Equals("Sheet"))
                sheet.SheetBy = textSplit[1];
            else if (textSplit[0].Equals("Difficult"))
                sheet.Difficult = textSplit[1];
            else if (sheetText.Equals("[NoteInfo]"))
            {
                while (sheetText != null)
                //while (!reader.EndOfStream)
                {
                    //sheetText = reader.ReadLine();
                    sheetText = strReader.ReadLine();
                    textSplit = sheetText.Split(',');

                    int laneNumber;
                    Int32.TryParse(textSplit[0], out laneNumber);
                    if (laneNumber.Equals(64))
                        laneNumber = 1;
                    else if (laneNumber.Equals(192))
                        laneNumber = 2;
                    else if (laneNumber.Equals(320))
                        laneNumber = 3;
                    else if (laneNumber.Equals(448))
                        laneNumber = 4;

                    float noteTime;
                    Single.TryParse(textSplit[2], out noteTime);

                    int noteType;
                    Int32.TryParse(textSplit[3], out noteType);

                    int longNoteTime;
                    Int32.TryParse(textSplit[5], out longNoteTime);

                    sheet.SetNote(laneNumber, noteTime, noteType, longNoteTime);

                    if (isFirstNote.Equals(true))
                    {
                        sheet.FirstNoteTime = noteTime;
                        isFirstNote = false;
                    }
                }
            }
        }
        reader.Close();
    }

}



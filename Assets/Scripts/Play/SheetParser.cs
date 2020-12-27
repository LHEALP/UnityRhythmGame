using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SheetParser : MonoBehaviour
{
    TextAsset textAsset;
    StringReader strReader;

    Sheet sheet;
    SongManager songManager;

    string sheetText = "";
    string songName;
    string[] textSplit;

    void Awake()
    {
        sheet = GameObject.Find("Sheet").GetComponent<Sheet>();
        songManager = GameObject.Find("SongSelect").GetComponent<SongManager>();
     
        songName = songManager.songName;
        textAsset = Resources.Load(songName + "/" + songName + "_data") as TextAsset;
        strReader = new StringReader(textAsset.text);

        ParseSheet();
    }
    
    // 불러온 텍스트를 한줄씩 읽어, 원하는 부분 잘라내어 저장
    public void ParseSheet()
    {
        int version = 1;
        int laneNumber = 1;
        float noteTime = 1;

        while(sheetText != null)
        {
            sheetText = strReader.ReadLine();
            textSplit = sheetText.Split('=');

            if (textSplit[0] == "SheetVersion0")       version = 0;
            else if (textSplit[0] == "AudioFileName")  sheet.AudioFileName = textSplit[1];
            else if (textSplit[0] == "AudioViewTime")  sheet.AudioViewTime = textSplit[1];
            else if (textSplit[0] == "ImageFileName")  sheet.ImageFileName = textSplit[1];
            else if (textSplit[0] == "BPM")            sheet.Bpm = float.Parse(textSplit[1]);
            else if (textSplit[0] == "Offset")         sheet.Offset = float.Parse(textSplit[1]);
            else if (textSplit[0] == "Beat")           sheet.Beat = int.Parse(textSplit[1]);
            else if (textSplit[0] == "Bit")            sheet.Bit = int.Parse(textSplit[1]);
            else if (textSplit[0] == "Bar")            sheet.BarCnt = int.Parse(textSplit[1]);
            else if (textSplit[0] == "Title")          sheet.Title = textSplit[1];
            else if (textSplit[0] == "Artist")         sheet.Artist = textSplit[1];
            else if (textSplit[0] == "Source")         sheet.Source = textSplit[1];
            else if (textSplit[0] == "Sheet")          sheet.SheetBy = textSplit[1];
            else if (textSplit[0] == "Difficult")      sheet.Difficult = textSplit[1];
            else if (sheetText == "[NoteInfo]")
            {
                while ((sheetText = strReader.ReadLine()) != null)
                {
                    textSplit = sheetText.Split(',');

                    if (version == 0)
                    {
                        int.TryParse(textSplit[0], out laneNumber);
                        float.TryParse(textSplit[2], out noteTime);

                        if (laneNumber == 64) laneNumber = 1;
                        else if (laneNumber == 192) laneNumber = 2;
                        else if (laneNumber == 320) laneNumber = 3;
                        else if (laneNumber == 448) laneNumber = 4;
                    }
                    else
                    {
                        int.TryParse(textSplit[1], out laneNumber);
                        float.TryParse(textSplit[0], out noteTime);
                    }

                    sheet.SetNote(laneNumber, noteTime);
                }
            }
        }
    }

}



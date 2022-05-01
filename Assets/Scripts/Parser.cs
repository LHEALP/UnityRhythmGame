using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Parser
{
    static Parser instance;
    public static Parser Instance
    {
        get
        {
            if (instance == null)
                instance = new Parser();
            return instance;
        }
    }

    enum Step
    {
        Description,
        Audio,
        Note,
    }
    Step currentStep = Step.Description;

    public Sheet Parse(string title)
    {
        Sheet sheet = new Sheet();
        string readLine = string.Empty;

        using (StreamReader sr = new StreamReader($"{Application.dataPath}/Sheet/{title}/{title}.sheet"))
        {
            readLine = sr.ReadLine();

            while (readLine != null)
            {
                if (readLine.StartsWith("[Description]"))
                {
                    currentStep = Step.Description;
                    readLine = sr.ReadLine();
                }
                else if (readLine.StartsWith("[Audio]"))
                {
                    currentStep = Step.Audio;
                    readLine = sr.ReadLine();
                }
                else if (readLine.StartsWith("[Note]"))
                {
                    currentStep = Step.Note;
                    readLine = sr.ReadLine();
                }

                if (currentStep == Step.Description)
                {
                    if (readLine.StartsWith("Title"))
                        sheet.title = readLine.Split(':')[1].Trim();
                    else if (readLine.StartsWith("Artist"))
                        sheet.artist = readLine.Split(':')[1].Trim();
                }
                else if (currentStep == Step.Audio)
                {
                    if (readLine.StartsWith("BPM"))
                        sheet.bpm = int.Parse(readLine.Split(':')[1].Trim());
                    else if (readLine.StartsWith("Offset"))
                        sheet.offset = int.Parse(readLine.Split(':')[1].Trim());
                }
                else if (currentStep == Step.Note)
                {
                    int time = int.Parse(readLine.Split(',')[0].Trim());
                    int type = int.Parse(readLine.Split(',')[1].Trim());
                    int line = int.Parse(readLine.Split(',')[2].Trim());
                    sheet.notes.Add(new Note(time, type, line));
                }

                readLine = sr.ReadLine();
            }
        }

        return sheet;
    }

}

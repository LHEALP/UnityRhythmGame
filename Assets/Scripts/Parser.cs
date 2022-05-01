using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Parser : MonoBehaviour
{
    Sheet sheet = new Sheet();
    enum Step
    {
        Description,
        Note,
    }
    Step currentStep = Step.Description;

    void Start()
    {
        Parse();

        Debug.Log(sheet.title);
        Debug.Log(sheet.artist);
        foreach (Note note in sheet.notes)
        {
            Debug.Log($"{note.time}, {note.type}, {note.line}");
        }
    }

    void Parse()
    {
        string readLine = string.Empty;

        using (StreamReader sr = new StreamReader($"{Application.dataPath}/BUNGEE.sheet"))
        {
            readLine = sr.ReadLine();

            while (readLine != null)
            {
                if (readLine.StartsWith("[Description]"))
                {
                    currentStep = Step.Description;
                    readLine = sr.ReadLine();
                }
                else if (readLine.StartsWith("[Note]"))
                {
                    currentStep = Step.Note;
                    readLine = sr.ReadLine();
                }

                if (currentStep == Step.Description)
                {
                    if (readLine.StartsWith("title"))
                        sheet.title = readLine.Split(':')[1].Trim();
                    else if (readLine.StartsWith("artist"))
                        sheet.artist = readLine.Split(':')[1].Trim();
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
    }

}

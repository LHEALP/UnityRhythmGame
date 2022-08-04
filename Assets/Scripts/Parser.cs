using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;

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

    public Sheet sheet;
    string basePath = $"{Application.dataPath}/Sheet";

    public AudioClip clip;
    public Sprite img;

    public IEnumerator IEParse(string title)
    {
        sheet = new Sheet();
        string readLine = string.Empty;

        using (StreamReader sr = new StreamReader($"{basePath}/{title}/{title}.sheet"))
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
                    else if (readLine.StartsWith("Signature"))
                    {
                        string[] s = readLine.Split(':');
                        s = s[1].Split('/');
                        int[] sign = { int.Parse(s[0].Trim()), int.Parse(s[1].Trim()) };
                        sheet.signature = sign;
                    }
                }
                else if (currentStep == Step.Note)
                {
                    if (string.IsNullOrEmpty(readLine))
                        break;

                    string[] s = readLine.Split(',');
                    int time = int.Parse(s[0].Trim());
                    int type = int.Parse(s[1].Trim());
                    int line = int.Parse(s[2].Trim());
                    int tail = -1;
                    if (s.Length > 3)
                        tail = int.Parse(readLine.Split(',')[3].Trim());
                    sheet.notes.Add(new Note(time, type, line, tail));
                }

                readLine = sr.ReadLine();
            }
        }

        yield return IEGetClip(title);
        yield return IEGetImg(title);

        sheet.clip = clip;
        sheet.img = img;
    }

    public IEnumerator IEGetClip(string title)
    {
        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip($"{basePath}/{title}/{title}.mp3", AudioType.MPEG))
        {
            yield return request.SendWebRequest();
            clip = DownloadHandlerAudioClip.GetContent(request);
            clip.name = title;
        }
    }

    public IEnumerator IEGetImg(string title)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture($"{basePath}/{title}/{title}.jpg"))
        {
            yield return request.SendWebRequest();
            Texture2D t = DownloadHandlerTexture.GetContent(request);
            img = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
            img.name = title;
        }
    }
}

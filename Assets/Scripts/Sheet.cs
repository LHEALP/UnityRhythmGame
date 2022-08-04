using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType
{
    Short = 0,
    Long = 1,
}

public struct Note
{
    public int time;
    public int type;
    public int line;
    public int tail;

    public Note(int time, int type, int line, int tail)
    {
        this.time = time;
        this.type = type;
        this.line = line;
        this.tail = tail;
    }
}

public class Sheet
{
    // [Description]
    public string title;
    public string artist;

    // [Audio]
    public int bpm;
    public int offset;
    public int[] signature;

    // [Note]
    public List<Note> notes = new List<Note>();


    public AudioClip clip;
    public Sprite img;
    
    public float BarPerSec { get; private set; }
    public float BeatPerSec { get; private set; }

    public int BarPerMilliSec { get; private set; }
    public int BeatPerMilliSec { get; private set; }

    public void Init()
    {
        BarPerMilliSec = (int)(signature[0] / (bpm / 60f) * 1000);
        BeatPerMilliSec = BarPerMilliSec / 64;

        BarPerSec = BarPerMilliSec * 0.001f;
        BeatPerSec = BarPerMilliSec / 64f;
    }
}
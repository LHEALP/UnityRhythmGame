using System.Collections;
using System.Collections.Generic;

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
    public string title;
    public string artist;

    public int bpm;
    public int offset;

    public List<Note> notes = new List<Note>();
}
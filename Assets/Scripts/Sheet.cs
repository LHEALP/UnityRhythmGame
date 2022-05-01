using System.Collections;
using System.Collections.Generic;

public struct Note
{
    public int time;
    public int type;
    public int line;

    public Note(int time, int type, int line)
    {
        this.time = time;
        this.type = type;
        this.line = line;
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
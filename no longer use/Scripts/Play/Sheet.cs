using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheet : MonoBehaviour
{
    // sheet의 정보를 담은 스크립트 입니다.

    // [SheetInfo]
    public string AudioFileName { set; get; }
    public string AudioViewTime { set; get; }
    public string ImageFileName { set; get; }
    public float Bpm { set; get; }
    public float Offset { set; get; }
    public int Beat { set; get; }
    public int Bit { set; get; }
    public int BarCnt { set; get; }

    // [ContentInfo]
    public string Title { set; get; }
    public string Artist { set; get; }
    public string Source { set; get; }
    public string SheetBy { set; get; }
    public string Difficult { set; get; }

    // [NoteInfo]
    public float X { set; get; }
    public float NoteTime { set; get; }
    public float NoteType { set; get; }
    public float LongNoteTime { set; get; }

    public float FirstNoteTime { set; get; }


    public List<float> noteList1 = new List<float>();
    public List<float> noteList2 = new List<float>();
    public List<float> noteList3 = new List<float>();
    public List<float> noteList4 = new List<float>();

    /*
    public List<float> longNoteList1 = new List<float>();
    public List<float> longNoteList2 = new List<float>();
    public List<float> longNoteList3 = new List<float>();
    public List<float> longNoteList4 = new List<float>();*/

    int laneNumber;
    float noteTime;
    int noteType;
    int longNoteTime;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void SetNote(int laneNumber, float noteTime, int noteType, int longNoteTime)
    {
        this.laneNumber = laneNumber;
        this.noteTime = noteTime;
        this.noteType = noteType;
        this.longNoteTime = longNoteTime;

        //숏노트
        if (laneNumber.Equals(1))
            noteList1.Add(noteTime);
        else if (laneNumber.Equals(2))
            noteList2.Add(noteTime);
        else if (laneNumber.Equals(3))
            noteList3.Add(noteTime);
        else if (laneNumber.Equals(4))
            noteList4.Add(noteTime);

        // 롱노트
        /*
        if(noteType.Equals(128))
        {
            if (laneNumber.Equals(1))
                longNoteList1.Add(longNoteTime);
            else if (laneNumber.Equals(2))
                longNoteList2.Add(longNoteTime);
            else if (laneNumber.Equals(3))
                longNoteList3.Add(longNoteTime);
            else if (laneNumber.Equals(4))
                longNoteList4.Add(longNoteTime);
        }*/
    }

    void showInfo()
    {
        Debug.Log(AudioFileName);
        Debug.Log(Title);
        Debug.Log(Artist);
        Debug.Log(Difficult);
        Debug.Log(Bpm);
    }
}

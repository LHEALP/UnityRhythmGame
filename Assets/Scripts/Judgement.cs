using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : MonoBehaviour
{
    // 노트시간을 알아야함
    // 롱노트는 어떻게?

    readonly int miss = 400;
    readonly int good = 300;
    readonly int great = 200;

    List<Queue<Note>> notes = new List<Queue<Note>>();
    Queue<Note> note1 = new Queue<Note>();
    Queue<Note> note2 = new Queue<Note>();
    Queue<Note> note3 = new Queue<Note>();
    Queue<Note> note4 = new Queue<Note>();

    int curruntTime = 0;

    public void Init()
    {
        foreach (var note in GameManager.Instance.sheet.notes)
        {
            if (note.line == 1)
                note1.Enqueue(note);
            else if (note.line == 2)
                note2.Enqueue(note);
            else if (note.line == 3)
                note3.Enqueue(note);
            else
                note4.Enqueue(note);
        }
        notes.Add(note1);
        notes.Add(note2);
        notes.Add(note3);
        notes.Add(note4);

        StartCoroutine(IECheckMiss());
    }

    public void Judge(int line)
    {
        Note note = notes[line].Peek();

        //if (note.type == (int)NoteType.Short)
        //{
        int judgeTime = curruntTime - note.time;
        if (judgeTime < miss && judgeTime > -miss)
        {
            if (judgeTime < good && judgeTime > -good)
            {
                if (judgeTime < great && judgeTime > -great)
                    Debug.Log("Great");
                else
                    Debug.Log("Good");
            }
            else
            {
                Debug.Log("빨라서 Miss");
            }
            notes[line].Dequeue();
        }


        //}
        //else if (note.type == (int)NoteType.Long)
        //{

        //}
    }

    IEnumerator IECheckMiss()
    {
        while (true)
        {
            curruntTime = (int)AudioManager.Instance.GetMilliSec();

            for (int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i].Peek();
                int judgeTime = note.time - curruntTime;
                if (judgeTime < -miss)
                {
                    Debug.Log("안쳐서 miss");
                    notes[i].Dequeue();
                }
            }

            yield return null;
        }
    }
}

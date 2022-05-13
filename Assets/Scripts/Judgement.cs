using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JudgeType
{
    Great,
    Good,
    Miss
}

public class Judgement : MonoBehaviour
{
    // 노트시간을 알아야함
    // 롱노트는 어떻게?

    readonly int miss = 600;
    readonly int good = 400;
    readonly int great = 250;

    List<Queue<Note>> notes = new List<Queue<Note>>();
    Queue<Note> note1 = new Queue<Note>();
    Queue<Note> note2 = new Queue<Note>();
    Queue<Note> note3 = new Queue<Note>();
    Queue<Note> note4 = new Queue<Note>();

    int[] longNoteCheck = new int[4] { 0, 0, 0, 0 };

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
        if (notes[line].Count <= 0)
            return;

        Note note = notes[line].Peek();
        int judgeTime = curruntTime - note.time;

        if (judgeTime < miss && judgeTime > -miss)
        {
            if (judgeTime < good && judgeTime > -good)
            {
                if (judgeTime < great && judgeTime > -great)
                {
                    GameManager.Instance.score.data.great++;
                    GameManager.Instance.score.data.judge = JudgeType.Great;
                }
                else
                {
                    GameManager.Instance.score.data.good++;
                    GameManager.Instance.score.data.judge = JudgeType.Good;
                }
                GameManager.Instance.score.data.combo++;
            }
            else
            {
                GameManager.Instance.score.data.fastMiss++;
                GameManager.Instance.score.data.judge = JudgeType.Miss;
                GameManager.Instance.score.data.combo = 0;
            }
            GameManager.Instance.score.SetScore();
        }

        if (note.type == (int)NoteType.Short)
        {
            notes[line].Dequeue();
        }
        else if (note.type == (int)NoteType.Long)
        {
            longNoteCheck[line] = 1;
        }
    }
    
    public void CheckLongNote(int line)
    {
        if (notes[line].Count <= 0)
            return;

        Note note = notes[line].Peek();
        if (note.type != (int)NoteType.Long)
            return;

        int judgeTime = curruntTime - note.tail;
        if (judgeTime < good && judgeTime > -good)
        {
            if (judgeTime < great && judgeTime > -great)
            {
                GameManager.Instance.score.data.great++;
                GameManager.Instance.score.data.judge = JudgeType.Great;
                GameManager.Instance.score.data.combo++;
            }
            else
            {
                GameManager.Instance.score.data.longMiss++;
            }
            GameManager.Instance.score.SetScore();
            longNoteCheck[line] = 0;
            notes[line].Dequeue();
        }
    }

    IEnumerator IECheckMiss()
    {
        while (true)
        {
            curruntTime = (int)AudioManager.Instance.GetMilliSec();

            for (int i = 0; i < notes.Count; i++)
            {
                if (notes[i].Count <= 0)
                    break;
                Note note = notes[i].Peek();
                int judgeTime = note.time - curruntTime;

                if (note.type == (int)NoteType.Long)
                {
                    if (longNoteCheck[note.line - 1] == 0) // Head가 판정처리가 안된 경우
                    {
                        if (judgeTime < -miss)
                        {
                            GameManager.Instance.score.data.miss++;
                            GameManager.Instance.score.data.judge = JudgeType.Miss;
                            GameManager.Instance.score.data.combo = 0;
                            GameManager.Instance.score.SetScore();
                            notes[i].Dequeue();
                        }
                    }
                }
                else
                {
                    if (judgeTime < -miss)
                    {
                        GameManager.Instance.score.data.miss++;
                        GameManager.Instance.score.data.judge = JudgeType.Miss;
                        GameManager.Instance.score.data.combo = 0;
                        GameManager.Instance.score.SetScore();
                        notes[i].Dequeue();
                    }
                }
            }

            yield return null;
        }
    }
}

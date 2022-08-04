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
    /// <summary>
    /// User에 의해 조정된 판정 타이밍
    /// </summary>
    public int judgeTimeFromUserSetting = 0;

    Coroutine coCheckMiss;

    public void Init()
    {
        foreach (var note in notes)
        {
            note.Clear();
        }
        notes.Clear();

        foreach (var note in GameManager.Instance.sheets[GameManager.Instance.title].notes)
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

        if (coCheckMiss != null)
        {
            StopCoroutine(coCheckMiss);
        }
        coCheckMiss = StartCoroutine(IECheckMiss());
    }

    public void Judge(int line)
    {
        if (notes[line].Count <= 0 || !AudioManager.Instance.IsPlaying())
            return;

        Note note = notes[line].Peek();
        int judgeTime = curruntTime - note.time + judgeTimeFromUserSetting;

        if (judgeTime < miss && judgeTime > -miss)
        {
            if (judgeTime < good && judgeTime > -good)
            {
                if (judgeTime < great && judgeTime > -great)
                {
                    Score.Instance.data.great++;
                    Score.Instance.data.judge = JudgeType.Great;
                }
                else
                {
                    Score.Instance.data.good++;
                    Score.Instance.data.judge = JudgeType.Good;
                }
                Score.Instance.data.combo++;
            }
            else
            {
                Score.Instance.data.fastMiss++;
                Score.Instance.data.judge = JudgeType.Miss;
                Score.Instance.data.combo = 0;
            }
            Score.Instance.SetScore();
            JudgeEffect.Instance.OnEffect(line);

            if (note.type == (int)NoteType.Short)
            {
                notes[line].Dequeue();
            }
            else if (note.type == (int)NoteType.Long)
            {
                longNoteCheck[line] = 1;
            }
        }
    }
    
    public void CheckLongNote(int line)
    {
        if (notes[line].Count <= 0)
            return;

        Note note = notes[line].Peek();
        if (note.type != (int)NoteType.Long)
            return;

        int judgeTime = curruntTime - note.tail + judgeTimeFromUserSetting;
        if (judgeTime < good && judgeTime > -good)
        {
            if (judgeTime < great && judgeTime > -great)
            {
                Score.Instance.data.great++;
                Score.Instance.data.judge = JudgeType.Great;
                Score.Instance.data.combo++;
            }
            else
            {
                Score.Instance.data.longMiss++;
            }
            Score.Instance.SetScore();
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
                int judgeTime = note.time - curruntTime + judgeTimeFromUserSetting;

                if (note.type == (int)NoteType.Long)
                {
                    if (longNoteCheck[note.line - 1] == 0) // Head가 판정처리가 안된 경우
                    {
                        if (judgeTime < -miss)
                        {
                            Score.Instance.data.miss++;
                            Score.Instance.data.judge = JudgeType.Miss;
                            Score.Instance.data.combo = 0;
                            Score.Instance.SetScore();
                            notes[i].Dequeue();
                        }
                    }
                }
                else
                {
                    if (judgeTime < -miss)
                    {
                        Score.Instance.data.miss++;
                        Score.Instance.data.judge = JudgeType.Miss;
                        Score.Instance.data.combo = 0;
                        Score.Instance.SetScore();
                        notes[i].Dequeue();
                    }
                }
            }

            yield return null;
        }
    }
}

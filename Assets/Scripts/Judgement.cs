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

    #region 테스트코드

    GUIStyle style = new GUIStyle();

    private void OnGUI()
    {
        style.fontSize = 28;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(50, 200, 800, 100), $"GREAT : {GameManager.Instance.score.great}, GOOD : {GameManager.Instance.score.good}, MISS : {GameManager.Instance.score.miss}", style);
        GUI.Label(new Rect(50, 350, 800, 100), $"Combo : {GameManager.Instance.score.combo}, longFaill : {GameManager.Instance.score.longMiss}", style);
    }
    #endregion

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
                    GameManager.Instance.score.great++;
                }
                else
                {
                    GameManager.Instance.score.good++;
                }
                GameManager.Instance.score.combo++;
            }
            else
            {
                GameManager.Instance.score.fastMiss++;
                GameManager.Instance.score.combo = 0;
            }
        }

        if (note.type == (int)NoteType.Short)
        {
            notes[line].Dequeue();
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
                GameManager.Instance.score.great++;
                GameManager.Instance.score.combo++;
            }
            else
            {
                GameManager.Instance.score.longMiss++;
            }
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
                if (judgeTime < -miss)
                {
                    GameManager.Instance.score.miss++;
                    GameManager.Instance.score.combo = 0;
                    notes[i].Dequeue();
                }
            }

            yield return null;
        }
    }
}

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
    int smiss = 0;
    int sgood = 0;
    int sgreat = 0;
    int longc = 0;
    int longf = 0;

    GUIStyle style = new GUIStyle();

    private void OnGUI()
    {
        style.fontSize = 28;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(50, 200, 800, 100), $"GREAT : {sgreat}, GOOD : {sgood}, MISS : {smiss}", style);
        GUI.Label(new Rect(50, 350, 800, 100), $"롱성공 : {longc}, 롱실패 : {longf}", style);
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
                    sgreat++;
                    Debug.Log("Great");
                }
                else
                {
                    sgood++;
                    Debug.Log("Good");
                }
            }
            else
            {
                smiss++;
                Debug.Log("빨라서 Miss");
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
                longc++;
                Debug.Log("롱노트 완성");
                sgreat++; // Head 판정 따라가도록 변경해야함
            }
            else
            {
                longf++;
                Debug.Log("롱노트 빨리 떼어버림");
                // 테일 미스인데 실제 카운팅은 안함
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
                Note note = notes[i].Peek();
                int judgeTime = note.time - curruntTime;
                if (judgeTime < -miss)
                {
                    smiss++;
                    Debug.Log("안쳐서 miss");
                    notes[i].Dequeue();
                }
            }

            yield return null;
        }
    }
}

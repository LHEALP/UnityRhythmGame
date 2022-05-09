using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NoteGenerator : MonoBehaviour
{
    static NoteGenerator instance;
    public static NoteGenerator Instance
    {
        get
        {
            return instance;
        }
    }

    public GameObject parent;
    public GameObject notePrefab;
    public Material lineRendererMaterial;

    enum NoteType
    {
        Short = 0,
        Long = 1,
    }

    readonly float[] linePos = { -1.5f, -0.5f, 0.5f, 1.5f };
    readonly float defaultInterval = 0.01f; // 1배속 기준점 (1마디 전체가 화면에 그려지는 정도를 정의)

    IObjectPool<NoteShort> poolShort;
    public IObjectPool<NoteShort> PoolShort
    {
        get
        {
            if (poolShort == null)
            {
                poolShort = new ObjectPool<NoteShort>(CreatePooledShort, defaultCapacity:256);
            }
            return poolShort; 
        }
    }
    NoteShort CreatePooledShort()
    {
        GameObject note = Instantiate(notePrefab, parent.transform);
        note.AddComponent<NoteShort>();
        return note.GetComponent<NoteShort>();
    }

    IObjectPool<NoteLong> poolLong;
    public IObjectPool<NoteLong> PoolLong
    {
        get
        {
            if (poolLong == null)
            {
                poolLong = new ObjectPool<NoteLong>(CreatePooledLong, defaultCapacity:64);
            }
            return poolLong;
        }
    }
    NoteLong CreatePooledLong()
    {
        GameObject note = new GameObject("NoteLong");
        note.transform.parent = parent.transform;
        GameObject head = new GameObject("head");
        head.transform.parent = note.transform;
        GameObject tail = new GameObject("tail");
        tail.transform.parent = note.transform;

        head.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = head.GetComponent<LineRenderer>();
        lineRenderer.material = lineRendererMaterial;
        lineRenderer.sortingOrder = 3;
        lineRenderer.widthMultiplier = 0.8f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(new Vector3[] { head.transform.position, tail.transform.position });

        note.AddComponent<NoteLong>();
        return note.GetComponent<NoteLong>();
    }

    int currentBar = 3; // 최초 플레이 시 3마디 먼저 생성
    int next = 0;
    int prev = 0;
    List<NoteObject> releaseList = new List<NoteObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(25, 25, 100, 30), PoolShort.CountInactive.ToString());
        GUI.Label(new Rect(25, 75, 100, 30), PoolLong.CountInactive.ToString());
    }

    public void StartGen()
    {
        StartCoroutine(IEGenTimer(1.4f));
        StartCoroutine(IEReleaseTimer(0.5f));
    }

    public void Gen()
    {
        List<Note> notes = GameManager.Instance.sheet.notes;
        List<Note> reconNotes = new List<Note>();

        for (; next < notes.Count; next++)
        {
            if (notes[next].time > currentBar * 1428) // 여기 마디시간계산 바꿔야함 168 BPM / 60 = 2.8, 4박자 / 2.8 = 1.428 = 1마디 시간
            {
                break;
            }
        }
        for (int j = prev; j < next; j++)
        {
            reconNotes.Add(notes[j]);
        }
        prev = next;

        foreach (Note note in reconNotes)
        {
            NoteObject noteObject = null;
            switch (note.type)
            {
                case (int)NoteType.Short:
                    noteObject = PoolShort.Get();
                    noteObject.SetPosition(new Vector3[] { new Vector3(linePos[note.line - 1], (note.time - AudioManager.Instance.GetTime() * 1000) * defaultInterval, 0f) });
                    break;
                case (int)NoteType.Long:
                    noteObject = PoolLong.Get();
                    noteObject.SetPosition(new Vector3[]                     // 포지션은 노트 시간 - 현재 음악 시간
                    { 
                        new Vector3(linePos[note.line - 1], (note.time - AudioManager.Instance.GetTime() * 1000) * defaultInterval, 0f),
                        new Vector3(linePos[note.line - 1], (note.tail - AudioManager.Instance.GetTime() * 1000) * defaultInterval, 0f)
                    });
                    break;
                default:
                    break;
            }
            noteObject.life = true;
            noteObject.gameObject.SetActive(true);
            noteObject.Move();
            releaseList.Add(noteObject);
        }
    }

    public void Release()
    {
        List<NoteObject> reconNotes = new List<NoteObject>();
        foreach (NoteObject note in releaseList)
        {
            if (!note.life)
            {
                if (note is NoteShort)
                    PoolShort.Release(note as NoteShort);
                else
                    PoolLong.Release(note as NoteLong);

                note.gameObject.SetActive(false);
            }
            else
            {
                reconNotes.Add(note);
            }
        }
        releaseList.Clear();
        releaseList.AddRange(reconNotes);
    }

    IEnumerator IEGenTimer(float interval)
    {
        while (true)
        {
            Gen();
            yield return new WaitForSeconds(interval);
            currentBar++;
        }
    }

    IEnumerator IEReleaseTimer(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Release();
        }
    }

#if (LegacyGen)
    List<NoteObject> noteList =  new List<NoteObject>();

    /// <summary>
    /// 노트를 한 번에 모두 생성하는 방식
    /// </summary>
    /// <param name="sheet"></param>
    public void Gen(Sheet sheet)
    {
        foreach (Note note in sheet.notes)
        {
            int line = note.line - 1;
            if (note.type == (int)NoteType.Short)
            {
                GameObject obj = Instantiate(notePrefab, new Vector3(linePos[line], note.time * defaultInterval, 0f), Quaternion.identity, parent.transform);
                obj.AddComponent<NoteShort>();
                noteList.Add(obj.GetComponent<NoteObject>());
            }
            else
            {
                // Head and Tail
                GameObject obj = new GameObject("NoteLong");
                obj.transform.parent = parent.transform;
                GameObject head = Instantiate(notePrefab, new Vector3(linePos[line], note.tail * defaultInterval, 0f), Quaternion.identity, obj.transform);
                GameObject tail = Instantiate(notePrefab, new Vector3(linePos[line], note.time * defaultInterval, 0f), Quaternion.identity, obj.transform);

                head.AddComponent<LineRenderer>();
                LineRenderer lineRenderer = head.GetComponent<LineRenderer>();
                lineRenderer.material = lineRendererMaterial;
                lineRenderer.sortingOrder = 3;
                lineRenderer.widthMultiplier = 0.8f;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPositions(new Vector3[] { head.transform.position, tail.transform.position });

                obj.AddComponent<NoteLong>(); // 호출시점 주의
                noteList.Add(obj.GetComponent<NoteObject>());
            }
        }

        Drop();
    }

    public void Drop()
    {
        foreach (NoteObject noteObject in noteList)
            noteObject.Move();
    }
#endif
}

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

    readonly float[] linePos = { -1.5f, -0.5f, 0.5f, 1.5f };
    readonly float defaultInterval = 0.005f; // 1배속 기준점 (1마디 전체가 화면에 그려지는 정도를 정의)
    public float Interval { get; private set; }

    IObjectPool<NoteShort> poolShort;
    public IObjectPool<NoteShort> PoolShort
    {
        get
        {
            if (poolShort == null)
            {
                poolShort = new ObjectPool<NoteShort>(CreatePooledShort, defaultCapacity: 256);
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
                poolLong = new ObjectPool<NoteLong>(CreatePooledLong, defaultCapacity: 64);
            }
            return poolLong;
        }
    }
    NoteLong CreatePooledLong()
    {
        GameObject note = new GameObject("NoteLong");
        note.transform.parent = parent.transform;
        
        GameObject head = Instantiate(notePrefab);
        head.name = "head";
        head.transform.parent = note.transform;

        GameObject tail = Instantiate(notePrefab);
        tail.transform.parent = note.transform;
        tail.name = "tail";

        GameObject line = new GameObject("line");
        line.transform.parent = note.transform;

        line.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.material = lineRendererMaterial;
        lineRenderer.sortingOrder = 3;
        lineRenderer.widthMultiplier = 0.8f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = false;

        note.AddComponent<NoteLong>();
        return note.GetComponent<NoteLong>();
    }

    int currentBar = 3; // 최초 플레이 시 3마디 먼저 생성
    int next = 0;
    int prev = 0;
    List<NoteObject> toReleaseList = new List<NoteObject>();

    Coroutine coGenTimer;
    Coroutine coReleaseTimer;
    Coroutine coInterpolate;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // 풀링 기반 생성 (게임 플레이 시 사용)
    public void StartGen()
    {
        Interval = defaultInterval * GameManager.Instance.Speed;
        coGenTimer = StartCoroutine(IEGenTimer(GameManager.Instance.sheets[GameManager.Instance.title].BarPerMilliSec * 0.001f)); // 음악의 1마디 시간마다 생성할 노트 오브젝트 탐색
        coReleaseTimer = StartCoroutine(IEReleaseTimer(GameManager.Instance.sheets[GameManager.Instance.title].BarPerMilliSec * 0.001f * 0.5f)); // 1마디 시간의 절반 주기로 해제할 노트 오브젝트 탐색
        coInterpolate = StartCoroutine(IEInterpolate(0.25f, 3.5f));
    }

    // 한 번에 다 생성 (에디팅할때 사용)
    public void GenAll()
    {
        Gen2();
    }

    public void StopGen()
    {
        if (coGenTimer != null)
        {
            StopCoroutine(coGenTimer);
            coGenTimer = null;
        }
        if (coReleaseTimer != null)
        {
            StopCoroutine(coReleaseTimer);
            coReleaseTimer = null;
        }
        foreach (NoteObject note in toReleaseList)
        {
            note.gameObject.SetActive(false);
        }

        toReleaseList.Clear();
        currentBar = 3;
        next = 0;
        prev = 0;
    }

    void Gen()
    {
        List<Note> notes = GameManager.Instance.sheets[GameManager.Instance.title].notes;
        List<Note> reconNotes = new List<Note>();

        for (; next < notes.Count; next++)
        {
            if (notes[next].time > currentBar * GameManager.Instance.sheets[GameManager.Instance.title].BarPerMilliSec)
            {
                break;
            }
        }
        for (int j = prev; j < next; j++)
        {
            reconNotes.Add(notes[j]);
        }
        prev = next;

        float currentTime = AudioManager.Instance.GetMilliSec();
        float noteSpeed = Interval * 1000;
        foreach (Note note in reconNotes)
        {
            NoteObject noteObject = null;

            switch (note.type)
            {
                case (int)NoteType.Short:
                    noteObject = PoolShort.Get();
                    noteObject.SetPosition(new Vector3[] { new Vector3(linePos[note.line - 1], (note.time - currentTime) * Interval, 0f) });
                    break;
                case (int)NoteType.Long:
                    noteObject = PoolLong.Get();
                    noteObject.SetPosition(new Vector3[] // 포지션은 노트 시간 - 현재 음악 시간
                    {
                        new Vector3(linePos[note.line - 1], (note.time - currentTime) * Interval, 0f),
                        new Vector3(linePos[note.line - 1], (note.tail - currentTime) * Interval, 0f)
                    });
                    break;
                default:
                    break;
            }
            noteObject.speed = noteSpeed;
            noteObject.note = note;
            noteObject.life = true;
            noteObject.gameObject.SetActive(true);
            noteObject.Move();
            toReleaseList.Add(noteObject);
        }
    }

    /// <summary>
    /// Editor Gen메소드, 노트의 이동은 노트 자신이 처리하지 않음.
    /// </summary>
    void Gen2()
    {
        Sheet sheet = GameManager.Instance.sheets[GameManager.Instance.title];

        List<Note> notes = sheet.notes;

        // (노트시간 - 오프셋) / 1비트(32비트시간값) = 노트의 위치
        // 노트의 위치 * 0.25(그리드의 32비트기준 간격) = 최종적인 노트의 위치

        foreach (Note note in notes)
        {
            NoteObject noteObject = null;

            switch (note.type)
            {
                case (int)NoteType.Short:
                    noteObject = PoolShort.Get();
                    if (note.time - sheet.offset <= 0)
                        noteObject.SetPosition(new Vector3[] { new Vector3(linePos[note.line - 1], 0f, 0f) });
                    else
                        noteObject.SetPosition(new Vector3[] { new Vector3(linePos[note.line - 1], (note.time - sheet.offset) / sheet.BeatPerMilliSec * 0.25f, 0f) });
                    break;
                case (int)NoteType.Long:
                    noteObject = PoolLong.Get();
                    if (note.time - sheet.offset <= 0)
                    {
                        noteObject.SetPosition(new Vector3[] // 포지션은 노트 시간 - 현재 음악 시간
{
                        new Vector3(linePos[note.line - 1], 0f, 0f),
                        new Vector3(linePos[note.line - 1], (note.tail - sheet.offset) / sheet.BeatPerMilliSec * 0.25f, 0f)
});
                    }
                    else
                    {
                        noteObject.SetPosition(new Vector3[] // 포지션은 노트 시간 - 현재 음악 시간
                        {
                        new Vector3(linePos[note.line - 1], (note.time - sheet.offset) / sheet.BeatPerMilliSec * 0.25f, 0f),
                        new Vector3(linePos[note.line - 1], (note.tail - sheet.offset) / sheet.BeatPerMilliSec * 0.25f, 0f)
                        });
                    }
                    break;
                default:
                    break;
            }
            noteObject.note = note;
            noteObject.life = true;
            noteObject.gameObject.SetActive(true);
            toReleaseList.Add(noteObject); // 에디팅끝나면 Release호출해서 해제해주기
        }
    }

    void Release()
    {
        List<NoteObject> reconNotes = new List<NoteObject>();
        foreach (NoteObject note in toReleaseList)
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
        toReleaseList.Clear();
        toReleaseList.AddRange(reconNotes);
    }

    public void Interpolate()
    {
        if (coInterpolate != null)
            StopCoroutine(coInterpolate);

        coInterpolate = StartCoroutine(IEInterpolate());
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

    IEnumerator IEInterpolate(float rate = 1f, float duration = 1f)
    {
        float time = 0;
        Interval = defaultInterval * GameManager.Instance.Speed;
        float noteSpeed = Interval * 1000;
        while (time < duration)
        {
            float milli = AudioManager.Instance.GetMilliSec();

            foreach (NoteObject note in toReleaseList)
            {
                note.speed = noteSpeed;
                note.Interpolate(milli, Interval);
            }
            time += rate;
            yield return new WaitForSeconds(rate);
        }
    }
}

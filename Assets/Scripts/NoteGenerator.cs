using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    int[] previousLong = { -1, -1, -1, -1 };

    readonly float[] linePos = { -1.5f, -0.5f, 0.5f, 1.5f };
    readonly float defaultInterval = 0.005f; // 1배속 기준점 (1마디 전체가 화면에 그려지는 정도를 정의)

    List<NoteObject> noteList =  new List<NoteObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
    }


    // TODO: 현재 음악의 재생시간 기준으로 3개의 마디만큼(1마디에는 32*4 만큼의 오브젝트가 들어갈 수 있다.)
    // 노트를 오브젝트풀 기반 포지션 배치(*Unity 공식 오브젝트풀링 사용)
    // TODO의 TODO: 곡의 난이도마다 1마디에 들어가는 노트의 수가 다름. 그래서 32*4만큼의 오브젝트를 항상 생성해줄 필요 없기 때문에
    // 최초 적절한 수의 오브젝트를 생성하거나, 곡마다 필요한 오브젝트의 수를 계산
    // 어 생각해보니, 전체 마디를 평균내서 생성해주는 것도 좋은 방법이 아닐까

    public void Gen(Sheet sheet)
    {
        foreach(Note note in sheet.notes)
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
                int p = previousLong[line];
                if (p != note.time && p != -1) // 이전 타임과 현재 타임과 동일하지 않다면 끝지점임
                {
                    // Head and Tail
                    GameObject obj = new GameObject("NoteLong");
                    obj.transform.parent = parent.transform;
                    GameObject head = Instantiate(notePrefab, new Vector3(linePos[line], p * defaultInterval, 0f), Quaternion.identity, obj.transform);
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

                    previousLong[line] = -1; // 롱놋이 구성됨
                }
                else
                    previousLong[line] = Mathf.Clamp(note.time, 1, note.time); // 롱놋 시작점 기록
            }
            
        }

        Drop();
    }

    public void Drop()
    {
        foreach (NoteObject noteObject in noteList)
            noteObject.Move();
    }
}

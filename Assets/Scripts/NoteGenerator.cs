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

    readonly float[] linePos = { -1.5f, -0.5f, 0.5f, 1.5f };
    readonly float defaultInterval = 0.01f;

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
            Instantiate(notePrefab, new Vector3(linePos[note.line - 1], note.time * defaultInterval, 0f), Quaternion.identity, parent.transform);
        }
    }

    public void Drop()
    {

    }
}

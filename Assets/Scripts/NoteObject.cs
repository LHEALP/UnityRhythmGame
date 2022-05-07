using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NoteObject : MonoBehaviour
{
    /// <summary>
    /// 노트 하강 속도
    /// </summary>
    public float speed;

    /// <summary>
    /// 노트 하강
    /// </summary>
    public abstract void Move();

    /// <summary>
    /// 노트 위치지정 (배속조절)
    /// </summary>
    public abstract void SetPosition();
}

public class NoteShort : NoteObject
{
    GameObject note;

    public override void Move()
    {
        Debug.Log("숏");
    }

    public override void SetPosition()
    {
        note.transform.position = Vector3.zero;
    }
}

public class NoteLong : NoteObject
{
    LineRenderer lineRenderer;
    GameObject head;
    GameObject tail;

    void Start()
    {
        head = transform.GetChild(0).gameObject;
        tail = transform.GetChild(1).gameObject;
        lineRenderer = head.GetComponent<LineRenderer>();
    }

    public override void Move()
    {
        Debug.Log("롱");
    }

    public override void SetPosition()
    {
        head.transform.position = Vector3.zero;
        tail.transform.position = Vector3.zero;

        lineRenderer.SetPositions(new Vector3[] { head.transform.position, tail.transform.position});
    }
}
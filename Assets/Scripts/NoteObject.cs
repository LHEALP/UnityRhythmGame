using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NoteObject : MonoBehaviour
{
    public bool life = false;

    public Note note = new Note();

    /// <summary>
    /// 노트 하강 속도
    /// interval에 따라 변해야함. 노트는 밀리세컨드 단위로 기록을 하고 있고 적절히 시각화하기 위해, 기본간격(defaultInterval)을 0.005 로 지정하고 있음 (이하로 지정시 현재 노트 그래픽이 겹칠 가능성 있음)
    /// 그러므로 노트가 하강하는 속도는 5가 되어야함. ex) 0.01 = 10speed, 0.001 = 1speed
    /// </summary>
    public float speed = 10f;

    /// <summary>
    /// 노트 하강
    /// </summary>
    public abstract void Move();
    public abstract IEnumerator IEMove();

    /// <summary>
    /// 노트 위치지정 (배속조절)
    /// </summary>
    public abstract void SetPosition(Vector3[] pos);

    public abstract void Interpolate(float curruntTime, float interval);
}

public class NoteShort : NoteObject
{
    public override void Move()
    {
        StartCoroutine(IEMove());
    }

    public override IEnumerator IEMove()
    {
        while (true)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            if (transform.position.y < -1f)
                life = false;

            yield return null;
        }
    }

    public override void SetPosition(Vector3[] pos)
    {
        transform.position = new Vector3(pos[0].x, pos[0].y, pos[0].z);
    }

    public override void Interpolate(float curruntTime, float interval)
    {
        if (transform.position.y > 12f)
            transform.position = new Vector3(transform.position.x, (note.time - curruntTime) * interval, transform.position.z);
    }
}

public class NoteLong : NoteObject
{
    LineRenderer lineRenderer;
    GameObject head;
    GameObject tail;

    void Awake()
    {
        head = transform.GetChild(0).gameObject;
        tail = transform.GetChild(1).gameObject;
        lineRenderer = head.GetComponent<LineRenderer>();
    }

    public override void Move()
    {
        StartCoroutine(IEMove());
    }

    public override IEnumerator IEMove()
    {
        while (true)
        {
            head.transform.position += Vector3.down * speed * Time.deltaTime;
            tail.transform.position += Vector3.down * speed * Time.deltaTime;
            lineRenderer.SetPositions(new Vector3[]{head.transform.position, tail.transform.position});
            if (tail.transform.position.y < -1f)
                life = false;

            yield return null;
        }
    }

    public override void SetPosition(Vector3[] pos)
    {
        head.transform.position = new Vector3(pos[0].x, pos[0].y, pos[0].z);
        tail.transform.position = new Vector3(pos[1].x, pos[1].y, pos[1].z);

        lineRenderer.SetPositions(new Vector3[] { head.transform.position, tail.transform.position});
    }

    public override void Interpolate(float curruntTime, float interval)
    {
        if (head.transform.position.y > 12f)
        {
            head.transform.position = new Vector3(head.transform.position.x, (note.time - curruntTime) * interval, head.transform.position.z);
            tail.transform.position = new Vector3(tail.transform.position.x, (note.tail - curruntTime) * interval, tail.transform.position.z);

            lineRenderer.SetPositions(new Vector3[] { head.transform.position, tail.transform.position });
        }
    }
}
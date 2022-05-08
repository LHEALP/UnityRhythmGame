using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NoteObject : MonoBehaviour
{
    /// <summary>
    /// 노트 하강 속도
    /// interval에 따라 변해야함. 노트는 밀리세컨드 단위로 기록을 하고 있고 적절히 시각화하기 위해, 기본간격(defaultInterval)을 0.005 로 지정하고 있음 (이하로 지정시 현재 노트 그래픽이 겹칠 가능성 있음)
    /// 그러므로 노트가 하강하는 속도는 5가 되어야함. ex) 0.01 = 10speed, 0.001 = 1speed
    /// </summary>
    public float speed = 5f;

    /// <summary>
    /// 노트 하강
    /// </summary>
    public abstract void Move();
    public abstract IEnumerator IEMove();

    /// <summary>
    /// 노트 위치지정 (배속조절)
    /// </summary>
    public abstract void SetPosition();
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
            yield return null;
        }
    }

    public override void SetPosition()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y * speed, transform.position.z);
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
            yield return null;
        }
    }

    public override void SetPosition()
    {
        head.transform.position = new Vector3(head.transform.position.x, head.transform.position.y * speed, head.transform.position.z);
        tail.transform.position = new Vector3(tail.transform.position.x, tail.transform.position.y * speed, tail.transform.position.z);

        lineRenderer.SetPositions(new Vector3[] { head.transform.position, tail.transform.position});
    }
}
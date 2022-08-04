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
    public float speed = 5f;

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

    /// <summary>
    /// Editor - Collider optimization
    /// </summary>
    public abstract void SetCollider();
    public abstract IEnumerator IECheckCollier();
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
        transform.position = new Vector3(transform.position.x, (note.time - curruntTime) * interval, transform.position.z);
    }

    public override void SetCollider()
    {
        if (GameManager.Instance.state == GameManager.GameState.Game)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            StartCoroutine(IECheckCollier());
        }
    }

    public override IEnumerator IECheckCollier()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (true)
        {
            int time = (int)transform.localPosition.y;
            int currentBar = Editor.Instance.currentBar;
            //Debug.Log(time + " @  " + (currentBar - 3) * 16 + " / " + (currentBar + 3) * 16);
            if (time >= (currentBar - 3) * 16 && time <= (currentBar + 3) * 16)
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }

            yield return wait;
        }
    }
}

public class NoteLong : NoteObject
{
    LineRenderer lineRenderer;
    public GameObject head;
    public GameObject tail;
    GameObject line;

    void Awake()
    {
        head = transform.GetChild(0).gameObject;
        tail = transform.GetChild(1).gameObject;
        line = transform.GetChild(2).gameObject;
        lineRenderer = line.GetComponent<LineRenderer>();
    }

    public override void Move()
    {
        StartCoroutine(IEMove());
    }

    public override IEnumerator IEMove()
    {
        while (true)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;

            if (tail.transform.position.y < -1f)
                life = false;

            yield return null;
        }
    }

    public override void SetPosition(Vector3[] pos)
    {
        transform.position = new Vector3(pos[0].x, pos[0].y, pos[0].z);
        head.transform.position = new Vector3(pos[0].x, pos[0].y, pos[0].z);
        tail.transform.position = new Vector3(pos[1].x, pos[1].y, pos[1].z);
        line.transform.position = head.transform.position;

        Vector3 linePos = tail.transform.position - head.transform.position;
        linePos.x = 0f;
        linePos.z = 0f;
        lineRenderer.SetPosition(1, linePos);
    }

    public override void Interpolate(float curruntTime, float interval)
    {
        transform.position = new Vector3(head.transform.position.x, (note.time - curruntTime) * interval, head.transform.position.z);
        head.transform.position = new Vector3(head.transform.position.x, (note.time - curruntTime) * interval, head.transform.position.z);
        tail.transform.position = new Vector3(tail.transform.position.x, (note.tail - curruntTime) * interval, tail.transform.position.z);
        line.transform.position = head.transform.position;

        Vector3 linePos = tail.transform.position - head.transform.position;
        linePos.x = 0f;
        linePos.z = 0f;
        lineRenderer.SetPosition(1, linePos);
    }

    public override void SetCollider()
    {
        if (GameManager.Instance.state == GameManager.GameState.Game)
        {
            head.GetComponent<BoxCollider2D>().enabled = false;
            tail.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            StartCoroutine(IECheckCollier());
        }
    }

    public override IEnumerator IECheckCollier()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (true)
        {
            int time = (int)transform.localPosition.y;
            int currentBar = Editor.Instance.currentBar;
            if (time >= (currentBar - 3) * 16 && time <= (currentBar + 3) * 16)
            {
                head.GetComponent<BoxCollider2D>().enabled = true;
                tail.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                head.GetComponent<BoxCollider2D>().enabled = false;
                tail.GetComponent<BoxCollider2D>().enabled = false;
            }

            yield return wait;
        }
    }
}
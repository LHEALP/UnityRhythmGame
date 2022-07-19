using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editor : MonoBehaviour
{
    static Editor instance;
    public static Editor Instance
    {
        get
        {
            return instance;
        }
    }

    UISilder slider = null;
    UIButton musicController = null;

    public GameObject objects;
    Coroutine coMove;

    int snap = 4;
    public int Snap
    {
        get { return snap; }
        set
        {
            snap = Mathf.Clamp(value, 1, 16);
        }
    }

    public int currentBar = 0;
    public float offsetPosition;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    float speed;
    public void Init()
    {
        slider = UIController.Instance.GetUI("UI_E_ProgressBar").uiObject as UISilder;
        musicController = UIController.Instance.GetUI("UI_E_Play").uiObject as UIButton;

        StartCoroutine(IEBarTimer());

        speed = 16 / GameManager.Instance.sheets[GameManager.Instance.title].BarPerSec;
        offsetPosition = speed * GameManager.Instance.sheets[GameManager.Instance.title].offset * 0.001f;
        objects.transform.position = offsetPosition * Vector3.up;
    }

    public void Play()
    {
        if (AudioManager.Instance.IsPlaying())
        {
            AudioManager.Instance.Pause();
            musicController.SetText(">");
            if (coMove != null)
                StopCoroutine(coMove);
        }
        else
        {
            AudioManager.Instance.Play();
            musicController.SetText("||");
            coMove = StartCoroutine(IEMove());
        }
    }

    public void Stop()
    {
        if (coMove != null)
            StopCoroutine(coMove);

        AudioManager.Instance.Stop();
        musicController.SetText(">");
    }

    IEnumerator IEBarTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (true)
        {
            currentBar = (int)(AudioManager.Instance.progressTime * 1000 / GameManager.Instance.sheets[GameManager.Instance.title].BarPerMilliSec);
            yield return wait;
        }
    }

    void Update()
    {
        float value = Mathf.Clamp(1 / AudioManager.Instance.Length * AudioManager.Instance.progressTime, 0f, 1f);
        if (slider != null)
        {
            slider.slider.value = value;
        }
    }

    IEnumerator IEMove()
    {
        while (true)
        {
            objects.transform.position += Vector3.down * Time.deltaTime * speed;
            yield return null;
        }
    }

    public void Play(UIObject uiObject)
    {
        Play();
    }

    public void Stop(UIObject uiObject)
    {
        Stop();
    }

    public void Progress()
    {
        if (slider != null)
        {
            float time = AudioManager.Instance.Length * slider.slider.value;
            AudioManager.Instance.MovePosition(time);

            // 음악 타임에 맞춰서 오브젝트스 이동
            // 한마디에 16씩 이동
            // time / 한마디 시간
            //float pos = (time / GameManager.Instance.sheets[GameManager.Instance.title].BarPerSec * 16) + (GameManager.Instance.sheets[GameManager.Instance.title].offset * speed * 0.001f);
            //Vector3 objPos = objects.transform.position;
            //objPos.y = -pos;
            //objects.transform.position = objPos;
        }
    }

    public void Progress(float snapValue)
    {
        Debug.Log(GameManager.Instance.sheets[GameManager.Instance.title].BarPerSec / snapValue);
        AudioManager.Instance.MovePosition(GameManager.Instance.sheets[GameManager.Instance.title].BarPerSec / snapValue);
    }
}

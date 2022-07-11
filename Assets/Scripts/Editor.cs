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
        StartCoroutine(IESlider());

        speed = 16 / GameManager.Instance.sheets[GameManager.Instance.title].BarPerSec;
        objects.transform.position += Vector3.up * speed * GameManager.Instance.sheets[GameManager.Instance.title].offset * 0.001f;
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

    IEnumerator IESlider()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            float value = Mathf.Clamp(1 / AudioManager.Instance.Length * AudioManager.Instance.progressTime, 0f, 1f);
            slider.OnValue(value);

            yield return wait;
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

    public void Progress(UIObject uiObject)
    {
        if (slider != null)
        {
            float time = AudioManager.Instance.Length * slider.slider.value;
            AudioManager.Instance.progressTime = time;
        }
    }
}

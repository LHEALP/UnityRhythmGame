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


    int snap = 4;
    public int Snap
    {
        get { return snap; }
        set
        {            
            snap = Mathf.Clamp(value, 4, 32);
        }
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        float value = 1 / AudioManager.Instance.Length * AudioManager.Instance.progressTime;
        if (float.IsNaN(value))
            return;

        if (slider == null)
            slider = UIController.Instance.FindUI("UI_E_ProgressBar").uiObject as UISilder;
        else
            slider.OnValue(value);
    }

    public void Play()
    {
        switch (AudioManager.Instance.state)
        {
            case AudioManager.State.Playing:
                {
                    AudioManager.Instance.Pause();
                    musicController.SetText(">");
                }
                break;
            case AudioManager.State.Paused:
                {
                    AudioManager.Instance.UnPause();
                    musicController.SetText("||");
                }
                break;
            case AudioManager.State.Unpaused:
                {
                    AudioManager.Instance.Pause();
                    musicController.SetText(">");
                }
                break;
            case AudioManager.State.Stop:
                {
                    AudioManager.Instance.Play();
                    musicController.SetText("||");
                }
                break;
            default:
                break;
        }
    }

    public void Play(UIObject uiObject)
    {
        if (musicController == null)
            musicController = uiObject as UIButton;
        else
        {
            Play();
        }
    }

    public void Stop(UIObject uiObject)
    {
        AudioManager.Instance.Stop();
        if (musicController != null)
            musicController.SetText(">");
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

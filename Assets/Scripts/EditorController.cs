using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorController : MonoBehaviour
{
    static EditorController instance;
    public static EditorController Instance
    {
        get
        {
            return instance;
        }
    }

    public bool isCtrl;
    float scrollValue;
    Coroutine coCtrl;

    InputManager inputManager;
    AudioManager audioManager;
    Editor editor;

    public Action<int> GridSnapListener;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        audioManager = AudioManager.Instance;
        editor = Editor.Instance;
    }

    void Update()
    {
        // 그리드에 레이쏴서 위치 알아내야함
        // 현재 스냅에 따라, 스냅될 위치 알아내야함
        Debug.Log(inputManager.mousePos);
    }

    // 스페이스 - 재생/일시정지( Space - Play/Puase )
    public void Space()
    {
        Editor.Instance.Play();
    }

    // 마우스좌클릭 - 노트 배치 ( Mouse leftBtn - Dispose note )
    // 마우스우클릭 - 노트 삭제 ( Mouse rightBtn - Cancel note )
    public void MouseBtn(string btnName)
    {
        if (btnName == "leftButton")
        {
            
        }
        else if (btnName == "rightButton")
        {

        }
    }

    // 마우스휠 - 음악 및 그리드 위치 이동 ( Mouse wheel - Move music and grids pos )
    public void Scroll(float value)
    {
        scrollValue = value;
    }

    // 컨트롤 + 마우스휠 - 그리드 스냅 변경 ( Ctrl + Mouse wheel - Change snap of grids )
    public void Ctrl()
    {
        if (coCtrl != null)
        {
            StopCoroutine(coCtrl);
        }
        coCtrl = StartCoroutine(IEWaitMouseWheel());
    }

    IEnumerator IEWaitMouseWheel()
    {
        while (isCtrl)
        {
            if (scrollValue > 0)
            {
                // 스냅업
                Editor.Instance.Snap /= 2;
            }
            else if (scrollValue < 0)
            {
                // 스냅다운
                Editor.Instance.Snap *= 2;
            }
            scrollValue = 0;

            GridSnapListener.Invoke(Editor.Instance.Snap);

            yield return null;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 100, 100), scrollValue.ToString());
        GUI.Label(new Rect(100, 200, 100, 100), Editor.Instance.Snap.ToString());
    }
}

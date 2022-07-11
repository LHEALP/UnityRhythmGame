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

    Camera cam;
    Vector3 worldPos;

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
        cam = Camera.main;

        inputManager = FindObjectOfType<InputManager>();
        audioManager = AudioManager.Instance;
        editor = Editor.Instance;
    }

    void Update()
    {
        // 그리드에 레이쏴서 위치 알아내야함
        // 현재 스냅에 따라, 스냅될 위치 알아내야함
        //Debug.Log(inputManager.mousePos);
        Vector3 mousePos = inputManager.mousePos;
        mousePos.z = -cam.transform.position.z;
        worldPos = cam.ScreenToWorldPoint(mousePos);
        //int layerMask = (1 << LayerMask.NameToLayer("Grid")) + (1 << LayerMask.NameToLayer("Note"));

        Debug.DrawRay(worldPos, cam.transform.forward * 2, Color.red, 0.2f);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, cam.transform.forward, 2f);
        if (hit)
        {
            int line = int.Parse(hit.transform.name.Split('_')[1]);
            int index = hit.transform.parent.GetComponent<GridObject>().index;
            if (worldPos.x < -1f && worldPos.x > -2f)
            {
                Debug.Log($"0번 레인 : {index}번 그리드 : {line} 비트");
            }
            else if (worldPos.x < 0.5f && worldPos.x > -1f)
            {
                Debug.Log($"1번 레인 : {index}번 그리드 : {line} 비트");
            }
            else if (worldPos.x < 1f && worldPos.x > 0.5f)
            {
                Debug.Log($"2번 레인 : {index}번 그리드 : {line} 비트");
            }
            else if (worldPos.x < 1.5f && worldPos.x > 1f)
            {
                Debug.Log($"3번 레인 : {index}번 그리드 : {line} 비트");
            }
        }
    }

    /// <summary>
    /// 스페이스 - 재생/일시정지( Space - Play/Puase )
    /// </summary>
    public void Space()
    {
        Editor.Instance.Play();
    }

    /// <summary>
    /// 좌클릭 - 노트 배치 ( Mouse leftBtn - Dispose note )
    /// 우클릭 - 노트 삭제 ( Mouse rightBtn - Cancel note )
    /// </summary>
    /// <param name="btnName"></param>
    public void MouseBtn(string btnName)
    {
        if (btnName == "leftButton")
        {
            
        }
        else if (btnName == "rightButton")
        {

        }
    }

    /// <summary>
    /// 마우스휠 - 음악 및 그리드 위치 이동 ( Mouse wheel - Move music and grids pos )
    /// </summary>
    /// <param name="value"></param>
    public void Scroll(float value)
    {
        scrollValue = value;
    }

    /// <summary>
    /// 컨트롤 + 마우스휠 - 그리드 스냅 변경 ( Ctrl + Mouse wheel - Change snap of grids )
    /// </summary>
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
                GridSnapListener.Invoke(Editor.Instance.Snap);
            }
            else if (scrollValue < 0)
            {
                // 스냅다운
                Editor.Instance.Snap *= 2;
                GridSnapListener.Invoke(Editor.Instance.Snap);
            }
            scrollValue = 0;

            yield return null;
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 36;
        GUI.Label(new Rect(100, 100, 100, 100), "Mouse Pos : " + inputManager.mousePos.ToString(), style);
        GUI.Label(new Rect(100, 200, 100, 100), "ScreenToWorld : " + worldPos.ToString(), style);
        GUI.Label(new Rect(100, 300, 100, 100), "CurrentBar : " + Editor.Instance.currentBar.ToString(), style);
    }
}

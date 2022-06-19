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

    public bool bCtrl;
    float scrollValue;
    Coroutine coCtrl;

    InputManager inputManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    void Update()
    {
        
        //inputManager.mousePos;
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
        while (bCtrl)
        {
            if (scrollValue >= 0)
            {
                // 스냅업
            }
            else
            {
                // 스냅다운
            }

            yield return null;
        }
    }
}

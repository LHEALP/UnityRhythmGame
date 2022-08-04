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

    public GameObject cursorPrefab;
    GameObject cursorObj;

    public bool isCtrl;
    float scrollValue;
    Coroutine coCtrl;

    Camera cam;
    Vector3 worldPos;

    InputManager inputManager;
    AudioManager audioManager;
    Editor editor;

    public Action<int> GridSnapListener;

    GameObject selectedNoteObject;
    Vector3 selectedGridPosition;
    Vector3 lastSelectedGridPosition;
    Transform headTemp;
    int selectedLine = 0;

    int longNoteMakingCount = 0;
    GameObject longNoteTemp;
    bool isDispose;

    public bool isShortNoteActive;
    public bool isLongNoteActive;

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

        cursorObj = Instantiate(cursorPrefab);
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

        // 커서 좌표
        cursorObj.transform.position = worldPos;

        Debug.DrawRay(worldPos, cam.transform.forward * 2, Color.red, 0.2f);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, cam.transform.forward, 2f);
        if (hit.transform == null)
        {
            isDispose = false;
            selectedNoteObject = null;
            return;
        }
        else if (hit.transform.CompareTag("Note"))
        {
            //Debug.Log("note");
            isDispose = false;
            selectedNoteObject = hit.transform.gameObject;
        }
        else
        {
            //Debug.Log("grid");
            int beat = int.Parse(hit.transform.name.Split('_')[1]);
            int index = hit.transform.parent.GetComponent<GridObject>().index;
            float y = hit.transform.TransformDirection(hit.transform.position).y; // Local Position To World Position

            if (worldPos.x < -1f && worldPos.x > -2f)
            {
                //Debug.Log($"0번 레인 : {index}번 그리드 : {beat} 비트");
                selectedLine = 0;
            }
            else if (worldPos.x < 0f && worldPos.x > -1f)
            {
                //Debug.Log($"1번 레인 : {index}번 그리드 : {beat} 비트");
                selectedLine = 1;
            }
            else if (worldPos.x < 1f && worldPos.x > 0f)
            {
                //Debug.Log($"2번 레인 : {index}번 그리드 : {beat} 비트");
                selectedLine = 2;
            }
            else if (worldPos.x < 2f && worldPos.x > 1f)
            {
                //Debug.Log($"3번 레인 : {index}번 그리드 : {beat} 비트");
                selectedLine = 3;
            }


            /*
             * 롱노트 배치 버그 수정 솔루션
                헤드를 찍고 스크롤을 올리거나 내리고
                테일을 찍으면 헤드가 따라올라오거나 내려감..
             */
            if (longNoteMakingCount == 0)
                headTemp = hit.transform; // head 기억해놨다가 활용


            selectedGridPosition = new Vector3(NoteGenerator.Instance.linePos[selectedLine], y, 0f);
            cursorObj.transform.position = selectedGridPosition;

            isDispose = true;
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
            if (selectedNoteObject != null)
            {
                Debug.Log("노트가 이미 존재합니다");
            }
            if (isDispose)
            {
                if (isLongNoteActive)
                {
                    if (longNoteMakingCount == 0)
                    {
                        lastSelectedGridPosition = selectedGridPosition;

                        NoteGenerator.Instance.DisposeNoteLong(longNoteMakingCount, new Vector3[] { lastSelectedGridPosition, selectedGridPosition });

                        longNoteMakingCount++;
                    }
                    else if (longNoteMakingCount == 1)
                    {
                        Vector3 tailPositon = selectedGridPosition;
                        tailPositon.x = lastSelectedGridPosition.x; // 롱노트는 사선으로 작성될 수 없으므로, 다른 라인(x)에 찍어도 종전과 동일한 위치를 유지
                        lastSelectedGridPosition.y = headTemp.TransformDirection(headTemp.transform.position).y;

                        // tail을 head보다 낮게 배치했을 경우 뒤집어주어야함
                        if (lastSelectedGridPosition.y < tailPositon.y)
                        {
                            NoteGenerator.Instance.DisposeNoteLong(longNoteMakingCount, new Vector3[] { lastSelectedGridPosition, tailPositon });
                        }
                        else
                        {
                            NoteGenerator.Instance.DisposeNoteLong(longNoteMakingCount, new Vector3[] { tailPositon, lastSelectedGridPosition });
                        }

                        longNoteMakingCount = 0;
                    }
                }
                else if (isShortNoteActive)
                {
                    NoteGenerator.Instance.DisposeNoteShort(NoteType.Short, selectedGridPosition );
                }
            }
        }
        else if (btnName == "rightButton")
        {
            if (selectedNoteObject != null)
            {
                //Debug.Log("노트 삭제");
                if (isLongNoteActive)
                {
                    // long은 부모 찾아서 비활성화
                    selectedNoteObject.transform.parent.gameObject.SetActive(false);
                }
                else if (isShortNoteActive)
                {
                    selectedNoteObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// 마우스휠 - 음악 및 그리드 위치 이동 ( Mouse wheel - Move music and grids pos )
    /// </summary>
    /// <param name="value"></param>
    public void Scroll(float value)
    {
        scrollValue = value;

        // 스크롤 시 해당 스냅만큼 이동 (컨트롤키가 입력되지않았을때만)
        if (!isCtrl)
        {
            float snap = Editor.Instance.Snap;
            if (scrollValue > 0)
            {
                Editor.Instance.objects.transform.position += Vector3.down * snap * 0.25f;         
                AudioManager.Instance.MovePosition(GameManager.Instance.sheets[GameManager.Instance.title].BeatPerSec * 0.001f * snap);
                //Debug.Log(GameManager.Instance.sheets[GameManager.Instance.title].BeatPerSec * 0.001f * snap);
            }
            else if (scrollValue < 0)
            {
                Editor.Instance.objects.transform.position += Vector3.up * snap * 0.25f;
                AudioManager.Instance.MovePosition(-GameManager.Instance.sheets[GameManager.Instance.title].BeatPerSec * 0.001f * snap);
                //Debug.Log(-GameManager.Instance.sheets[GameManager.Instance.title].BeatPerSec * 0.001f * snap);
            }
        }
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

    //private void OnGUI()
    //{
    //    GUIStyle style = new GUIStyle();
    //    style.fontSize = 36;
    //    GUI.Label(new Rect(100, 100, 100, 100), "Mouse Pos : " + inputManager.mousePos.ToString(), style);
    //    GUI.Label(new Rect(100, 200, 100, 100), "ScreenToWorld : " + worldPos.ToString(), style);
    //    GUI.Label(new Rect(100, 300, 100, 100), "CurrentBar : " + Editor.Instance.currentBar.ToString(), style);
    //    GUI.Label(new Rect(100, 400, 100, 100), "Snap : " + Editor.Instance.Snap.ToString(), style);
    //}
}

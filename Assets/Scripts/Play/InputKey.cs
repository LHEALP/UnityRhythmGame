using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputKey : MonoBehaviour
{
    Judgement judgement;
    AudioSource music;
    GameObject[] notes;
    SongManager songManager;

    Text textSpeed;
    int speed = 10;

    Transform d;
    Transform f;
    Transform j;
    Transform k;

    Touch touch;
    Vector3 touchPos;


    // Start is called before the first frame update
    void Start()
    {
        judgement = GameObject.Find("Judgement").GetComponent<Judgement>();
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        textSpeed = GameObject.Find("SpeedText").GetComponent<Text>();
        songManager = GameObject.Find("SongSelect").GetComponent<SongManager>();

        d = GameObject.Find("DBtn").GetComponent<Transform>();
        d.gameObject.SetActive(false);
        f = GameObject.Find("FBtn").GetComponent<Transform>();
        f.gameObject.SetActive(false);
        j = GameObject.Find("JBtn").GetComponent<Transform>();
        j.gameObject.SetActive(false);
        k = GameObject.Find("KBtn").GetComponent<Transform>();
        k.gameObject.SetActive(false);


    }

    void Update()
    {
#if UNITY_STANDALONE
        /* D!!!!!!!!!!!!!!!!!!!!!!!!! */
        if (Input.GetKeyDown(KeyCode.D))
        {
            d.gameObject.SetActive(true);
            judgement.JudgeNote(1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            d.gameObject.SetActive(false);
            //judgement.JudgeNote(5);
        }

        /* F!!!!!!!!!!!!!!!!!!!!!!!!! */
        if (Input.GetKeyDown(KeyCode.F))
        {
            f.gameObject.SetActive(true);
            judgement.JudgeNote(2);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            f.gameObject.SetActive(false);
            //judgement.JudgeNote(6);
        }

        /* J!!!!!!!!!!!!!!!!!!!!!!!!! */
        if (Input.GetKeyDown(KeyCode.J))
        {
            j.gameObject.SetActive(true);
            judgement.JudgeNote(3);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            j.gameObject.SetActive(false);
            //judgement.JudgeNote(7);
        }

        /* K!!!!!!!!!!!!!!!!!!!!!!!!! */
        if (Input.GetKeyDown(KeyCode.K))
        {
            k.gameObject.SetActive(true);
            judgement.JudgeNote(4);
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            k.gameObject.SetActive(false);
            //judgement.JudgeNote(8);
        }


        //여기까지 노트입력키

        //그외의 키
        //배속
        if (Input.GetKeyDown(KeyCode.F5))
        {
            notes = GameObject.FindGameObjectsWithTag("Note");
            foreach (GameObject note in notes)
                note.GetComponent<Note>().ChangeNoteSpeed(1);
            speed -= 1;
            textSpeed.text = "노트 배속 " + speed.ToString();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            notes = GameObject.FindGameObjectsWithTag("Note");
            foreach (GameObject note in notes)
                note.GetComponent<Note>().ChangeNoteSpeed(0);
            speed += 1;
            textSpeed.text = "노트 배속 " + speed.ToString();
        }
        // 뒤로가기
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            songManager.StopSong(true);

            GameObject sheet = GameObject.Find("Sheet");
            GameObject score = GameObject.Find("Score");
            GameObject songSelect = GameObject.Find("SongSelect");
            Destroy(sheet);
            Destroy(score);
            Destroy(songSelect);
            SceneManager.LoadScene("SongSelect");
        }

#endif
#if UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            for(int i=0; i<Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);
                if(touch.phase.Equals(TouchPhase.Began))
                {
                    touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                    if(touchPos.Equals(s.transform.position))
                    {
                        s.gameObject.SetActive(true);
                        judgement.JudgeNote(1);
                    }
                    if (touchPos.Equals(d.transform.position))
                    {
                        d.gameObject.SetActive(true);
                        judgement.JudgeNote(2);
                    }
                    if (touchPos.Equals(f.transform.position))
                    {
                        f.gameObject.SetActive(true);
                        judgement.JudgeNote(3);
                    }
                    if (touchPos.Equals(j.transform.position))
                    {
                        j.gameObject.SetActive(true);
                        judgement.JudgeNote(4);
                    }
                    if (touchPos.Equals(k.transform.position))
                    {
                        k.gameObject.SetActive(true);
                        judgement.JudgeNote(5);
                    }
                    if (touchPos.Equals(l.transform.position))
                    {
                        l.gameObject.SetActive(true);
                        judgement.JudgeNote(6);
                    }
                    break;
                }
                else if(touch.phase.Equals(TouchPhase.Ended))
                {
                    if (touchPos.Equals(s.transform.position))
                        s.gameObject.SetActive(false);
                    if (touchPos.Equals(d.transform.position))
                        d.gameObject.SetActive(false);
                    if (touchPos.Equals(f.transform.position))
                        f.gameObject.SetActive(false);
                    if (touchPos.Equals(j.transform.position))
                        j.gameObject.SetActive(false);
                    if (touchPos.Equals(k.transform.position))
                        k.gameObject.SetActive(false);
                    if (touchPos.Equals(l.transform.position))
                        l.gameObject.SetActive(false);
                }
            }
        }
#endif
    }

}

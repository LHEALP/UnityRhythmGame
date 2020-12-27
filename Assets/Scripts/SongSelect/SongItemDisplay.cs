using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongItemDisplay : MonoBehaviour
{
    public Text textName;
    public Text textLevel;
    public Text textArtitst;
    public Image sprite;

    public delegate void SongItemDisplayDelegate(SongItem item);
    public event SongItemDisplayDelegate onClick;

    public SongItem item;

    public SongManager songManager;

    Player player;

    // static을 사용하지 않으면 이벤트가 변수값을 전부 기억하고 있기 때문에, 전에 한번이라도 눌렀던적 있다면
    // 다른것을 한번 누르고 다시 눌러도 씬이 전환되지 않기 위함.
    static string songCheck = "";
    static int clickCnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (item != null) Prime(item);

        songManager = GameObject.Find("SongSelect").GetComponent<SongManager>();

        player = FindObjectOfType<Player>();
    }

    public void Prime(SongItem item)
    {
        this.item = item;
        if (textName != null)
            textName.text = item.songName;
        if (textLevel != null)
            textLevel.text = item.songLevel;
        if (textArtitst != null)
            textArtitst.text = item.songArtist;
        if (sprite != null)
            sprite.sprite = item.sprite;
    }

    public void Click()
    {
        

        if (onClick != null)
            onClick.Invoke(item);
        else
        {
            // 같은 항목을 두번 클릭하면 플레이
            if (songCheck.Equals(item.songName))
                clickCnt++;
            else
            {
                clickCnt = 0;
                songCheck = item.songName;
                clickCnt++;
            }

            Debug.Log(item.songName);
            // 노래 미리듣기
            songManager.PlayAudioPreview(item.songName);


            // Play씬 전환및 전달할 곡데이터
            if (clickCnt.Equals(2))
            {
                songManager.SelectSong(item.songName);
                clickCnt = 0; // ESC를 눌러 돌아왔을때 제대로 작동하기 위함
                if (!player.isEditMode)
                    SceneManager.LoadSceneAsync("Play");
                else
                    SceneManager.LoadSceneAsync("NoteEditor");
            }


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("SongSelect");

        player.isEditMode = false;
    }
}

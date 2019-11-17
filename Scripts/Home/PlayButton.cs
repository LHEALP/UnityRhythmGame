using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    Dropdown dropDownClass;
    Player player;

    void Start()
    {
        dropDownClass = GameObject.Find("ClassDropdown").GetComponent<Dropdown>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void OnClickPlayButton()
    {
        player.PlayerClass = dropDownClass.value;

        SceneManager.LoadScene("SongSelect");
    }
}

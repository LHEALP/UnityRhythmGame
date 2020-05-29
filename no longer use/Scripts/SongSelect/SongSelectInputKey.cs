using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelectInputKey : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject player = GameObject.Find("Player");
            GameObject songSelect = GameObject.Find("SongSelect");
            Destroy(player);
            Destroy(songSelect);

            SceneManager.LoadScene("Home");
        }
    }
}

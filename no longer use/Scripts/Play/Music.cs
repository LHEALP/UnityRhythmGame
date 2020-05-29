using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    SongManager songManager;

    void Start()
    {
        songManager = GameObject.Find("SongSelect").GetComponent<SongManager>();
        songManager.PlayAudioForPlayScene();
    }
    void Update()
    {
        songManager.FinishSong();
    }
}

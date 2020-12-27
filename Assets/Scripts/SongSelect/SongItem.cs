using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SongItem : MonoBehaviour
{
    public string songName;
    public string songLevel;
    public string songArtist;
    public Sprite sprite;
}

[System.Serializable]
public class NewSongItem
{
    public string songName;
    public string songLevel;
    public string songArtist;
    public Sprite sprite;

    public NewSongItem(string name, string level, string artist, Sprite sprite)
    {
        songName = name;
        songLevel = level;
        songArtist = artist;
        this.sprite = sprite;
    }
}
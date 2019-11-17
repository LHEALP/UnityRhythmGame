using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongList : MonoBehaviour
{
    public List<SongItem> items = new List<SongItem>();
    public SongDisplay songDisplayPrefab;

    // Inventory.cs
    void Start()
    {
        SongDisplay song = (SongDisplay)Instantiate(songDisplayPrefab);
        song.Prime(items);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditButton : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnClickEditButton()
    {
        SceneManager.LoadSceneAsync("SongSelect");

        player.isEditMode = true;
    }
}

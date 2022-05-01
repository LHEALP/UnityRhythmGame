using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
            instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Insert(AudioClip clip)
    {
        audioSource.clip = clip;
    }
}

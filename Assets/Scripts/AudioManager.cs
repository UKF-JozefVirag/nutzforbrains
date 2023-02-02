using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] sfx;
    public AudioSource sfxSource, musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        PlayMusic("music");
    }

    public void PlayMusic(string name)
    {
        AudioClip s = Array.Find(sfx, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else sfxSource.PlayOneShot(s, 1f);
    }

    public void PlaySound(string name)
    {
        AudioClip s = Array.Find(sfx, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else sfxSource.PlayOneShot(s, 1f);
    }

}

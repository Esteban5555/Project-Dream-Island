using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Audio[] sounds;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Audio a in sounds) {

            a.source = gameObject.AddComponent<AudioSource>();

            a.source.clip = a.clip;
            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
        }
    }

    public void Play(string name) {
        Audio a = Array.Find(sounds, sound => sound.name == name);
        if (a == null) { 
            Debug.Log("No sound found");
            return;
        }
        a.source.Play();
    }

    public void Pause(string name)
    {
        Audio a = Array.Find(sounds, sound => sound.name == name);
        a.source.Pause();
    }

    public bool IsPlayingAudio(string name) {
        Audio a = Array.Find(sounds, sound => sound.name == name);
        return a.source.isPlaying;
    }

    public void PauseAll() {
        foreach (Audio a in sounds)
        {
            Pause(a.name);
        }
    }
}

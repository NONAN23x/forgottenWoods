using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public Sounds[] sounds;

    public static AudioManager instance;
    
    private void Awake() {
        
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        // DontDestroyOnLoad(gameObject);

        foreach (Sounds s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.spatialBlend = 0f;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    private void Start() {
        Play("backgroundMusic");
    }
    
    public void Play (String name) {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null ) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
        
    }
    public void Stop (String name) {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null ) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}

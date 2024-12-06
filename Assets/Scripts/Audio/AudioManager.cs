using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public bool dontDestroyOnLoad;

    [SerializeField] Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {

            if (Instance == null)
            {
                Instance = this;
            }
                else
                {
                    Destroy(gameObject);
                }

            DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        PlaySound("BGMusic");

    }

    void Start()
    {
      //      PlaySound("BGMusic");
    }

    void Update()
    {

    }

    public Sound GetSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        return sound;
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Play();
    }

    public void StopSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Stop();
    }
}
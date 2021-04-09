using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    string level;

    public Sound[] sounds;

    public AudioMixer mixer; 

    //public static AudioManager instance;

    void Awake()
    {
        /*if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);*/

        // Creates all the necessary AudioSource objects from the list of Sounds
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;

            if (s.isMusic)
                s.source.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/Music")[0];
            else
                s.source.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/SFX")[0];
        }

        level = SceneManager.GetActiveScene().name;
    }

    // Start is called before the first frame update
    private void Start()
    {
        PlayMusic();
    }

    // Plays the level music
    void PlayMusic()
    {
        Play(level);
    }

    // Play the Sound with the given name
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound With Name : " + name + " Not Found!"); // If no sound
            return;
        }
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound With Name : " + name + " Not Found!"); // If no sound
            return;
        }
        s.source.Pause();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound With Name : " + name + " Not Found!"); // If no sound
            return;
        }
        s.source.Stop();
    }

    public void PlayIndividualSound(string name)
    {
        AudioSource src = gameObject.AddComponent<AudioSource>();
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound With Name : " + name + " Not Found!"); // If no sound
            return;
        }
        src.clip = s.clip;
        src.outputAudioMixerGroup = s.source.outputAudioMixerGroup;
        src.Play();
    }
}

using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    string level;

    public Sound[] sounds;

    //public static AudioManager instance;

    // Start is called before the first frame update
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

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        level = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        PlayMusic();
    }

    void PlayMusic()
    {
        Play(level);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound With Name : " + name + " Not Found!");
            return;
        }
        s.source.Play();
    }
}

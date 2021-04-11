using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject settingsMenuUI;

    bool loaded = false;

    // Loads the appropriate slider value to the sliders
    private void Update()
    {
        if (!loaded && settingsMenuUI.activeSelf)
        {
            float sfxV = 0;
            float musicV = 0;
            float masterV = 0;

            audioMixer.GetFloat("SFXVolume", out sfxV);
            audioMixer.GetFloat("MusicVolume", out musicV);
            audioMixer.GetFloat("MasterVolume", out masterV);

            settingsMenuUI.transform.Find("SFXSlider").GetComponent<Slider>().value = sfxV;
            settingsMenuUI.transform.Find("MusicSlider").GetComponent<Slider>().value = musicV;
            settingsMenuUI.transform.Find("MasterSlider").GetComponent<Slider>().value = masterV;

            loaded = true;
        }

    }

    // Changes the volume for the appropriate channels
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        Debug.Log(volume);
    }
    
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        Debug.Log(volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        Debug.Log(volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen; 
    }


    //Save settings
    public void SaveSettings()
    {
        Debug.Log("Saved Settings");
    }

    // Close Settings Menu
    public void CloseSettings()
    {
        SaveSettings();
        FindObjectOfType<AudioManager>().Play("Select");
        settingsMenuUI.SetActive(false);
    }
}

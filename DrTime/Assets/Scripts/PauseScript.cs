using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMeneUI;
    public GameObject settingMenuUI;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
                settingMenuUI.SetActive(false);
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        FindObjectOfType<AudioManager>().Pause(SceneManager.GetActiveScene().name);
        pauseMeneUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play(SceneManager.GetActiveScene().name);
        pauseMeneUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<AudioManager>().Play("Select");
    }

    public void OpenSettings()
    {
        //Time.timeScale = 1f;
        settingMenuUI.SetActive(true);
        Debug.Log("Opened Settings");
        FindObjectOfType<AudioManager>().Play("Select");
    }

    public void Quit()
    {
        SaveSystem.Save();
        FindObjectOfType<AudioManager>().Play("Select");
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}

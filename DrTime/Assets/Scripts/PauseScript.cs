using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMeneUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMeneUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMeneUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void OpenSettings()
    {
        Time.timeScale = 1f;
        Debug.Log("Opened Settings");
    }

    public void Quit()
    {
        Save();
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    private void Save()
    {
        Debug.Log("Saving...");
    }
}

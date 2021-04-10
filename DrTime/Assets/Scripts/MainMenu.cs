using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingMenuUI;
    public GameObject mainMenuUI;
    public GameObject startGameMenu;

    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        startGameMenu.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Select");
    }

    public void Back()
    {
        mainMenuUI.SetActive(true);
        startGameMenu.SetActive(false);
        FindObjectOfType<AudioManager>().Play("Select");
    }

    public void NewGame()
    {
        SaveSystem.Save();
        FindObjectOfType<AudioManager>().Play("Select");
        LaunchGame();
    }

    public void LoadGame()
    {
        PlayerSystem.inventory = SaveSystem.LoadPlayer().inventory;
        FindObjectOfType<AudioManager>().Play("Select");
        LaunchGame();
    }

    public void OpenSettings()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        settingMenuUI.SetActive(true);
    }

    public void QuitGame(){
        FindObjectOfType<AudioManager>().Play("Select");
        Debug.Log("Quit");
        Application.Quit();

    }

    void LaunchGame()
    {
        SceneManager.LoadScene("Lobby");
    }
}

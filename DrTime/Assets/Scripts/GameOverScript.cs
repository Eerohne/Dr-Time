using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public static bool gameOver = false; // Watches if the game is over

    bool endGame = true; // True if the game is still running

    public string LobbyName; // Lobby scene name
    public string MainMenu; // main menu scene name

    public GameObject gameOverUI; // Reference to the Game Over Screen;

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        if (endGame)
        {
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);
            FindObjectOfType<AudioManager>().Play("GameOver");
            endGame = false;
        }
    }

    void Resume(string level)
    {
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        endGame = true;
        gameOver = false;
        SceneManager.LoadScene(level);
    }

    public void ResetGame()
    {
        Resume(SceneManager.GetActiveScene().name);
    }

    public void ReturnToLobby()
    {
        Resume(LobbyName);
    }

    public void ReturnToMainMenu()
    {
        Resume(MainMenu);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButtonScript : MonoBehaviour
{
    public float exitTime = 0f; //time it takes to exit

    private void Start()
    {
        //instantiates the button
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    //once the button is press it play the start function
    void TaskOnClick()
    {
        StartCoroutine("Wait");
    }

    //once the button is pressed the sound plays, saves and loads the lobby
    IEnumerator Wait()
    {
        FindObjectOfType<AudioManager>().Play("Quit");
        SaveSystem.Save();
        yield return new WaitForSeconds(exitTime);
        SceneManager.LoadScene("Lobby");
    }
}

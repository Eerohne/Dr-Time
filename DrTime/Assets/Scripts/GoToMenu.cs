using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    public Animator transition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OpenCredits();
        }
    }

    public void OpenCredits()
    {
        StartCoroutine(SwitchScene("Closing"));
    }

    IEnumerator SwitchScene(string scene)
    {
        FindObjectOfType<AudioManager>().Play("Portal");
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }
}

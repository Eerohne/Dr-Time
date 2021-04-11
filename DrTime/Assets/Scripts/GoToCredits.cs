using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToCredits : MonoBehaviour
{
    public Animator transition;

    public void OpenCredits()
    {
        StartCoroutine(SwitchScene("Closing"));
    }

    //Opens Credits
    IEnumerator SwitchScene(string scene)
    {
        FindObjectOfType<AudioManager>().Play("Portal");
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }
}

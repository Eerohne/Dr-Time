using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButtonScript : MonoBehaviour
{
    private void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        SaveSystem.Save();
        FindObjectOfType<AudioManager>().Play("Quit");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Lobby");
    }
}

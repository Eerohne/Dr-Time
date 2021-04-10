using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButtonScript : MonoBehaviour
{
    public float exitTime = 0.5f;

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
        FindObjectOfType<AudioManager>().Play("Quit");
        SaveSystem.Save();
        yield return new WaitForSeconds(exitTime);
        SceneManager.LoadScene("Lobby");
    }
}

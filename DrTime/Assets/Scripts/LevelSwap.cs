using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwap : MonoBehaviour
{
    public string Scene;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            StartCoroutine("Swap");
        }
    }

    IEnumerator Swap()
    {
        FindObjectOfType<AudioManager>().Play("Portal");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Scene);
    }
}

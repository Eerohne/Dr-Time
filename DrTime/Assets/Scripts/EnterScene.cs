using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterScene : MonoBehaviour
{

    public string scene;
    public Text text;
    public Image backdrop;
    private bool isInside = false;
    public bool isPortal;
    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(1, 1, 1, 0);
        backdrop.enabled = false;
    }

    // Update is called once per frame
    void Update() {

        if (isInside && Input.GetKeyDown(KeyCode.R)){

            SaveSystem.Save();

            if (isPortal)
            {
                StartCoroutine("portalWait");
            }
            else
            {
                SceneManager.LoadScene(scene);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            isInside = true;
            StartCoroutine(FadeText(false));
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            isInside = false;

            StartCoroutine(FadeText(true));
        }
    }

    IEnumerator FadeText(bool fadeOut)
    {
        //fade out
        if (fadeOut)
        {
            for (float i= 0.5f; i>=0; i-=Time.deltaTime)
            {
                backdrop.color = new Color(0.1698f, 0.1698f, 0.1698f, i);
                text.color = new Color(120, 60, 120, i);

                yield return null;
            }

            backdrop.color = new Color(0.1698f, 0.1698f, 0.1698f, 0);
            text.color = new Color(120, 60, 120, 0);

        }

        //fade in
        else
        {
            text.text = "Press R to enter " + scene;
            backdrop.enabled = true;

            for (float i=0; i<=0.5; i+=Time.deltaTime)
            {
                backdrop.color = new Color(0.1698f, 0.1698f, 0.1698f, i);
                text.color = new Color(120, 60, 120, i);

                yield return null;
            }



        }
    }

    IEnumerator portalWait()
    {
        FindObjectOfType<AudioManager>().Play("Portal");
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }


}

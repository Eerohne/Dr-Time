using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDoorText : MonoBehaviour
{
    public Image backdrop;
    public Text text;

    public string insertText = "This door is closed";

    // If colliding with door, display insertText
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(FadeText(false));
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(FadeText(true));
        }
    }

    IEnumerator FadeText(bool fadeOut)
    {
        //fade out
        if (fadeOut)
        {
            for (float i = 0.5f; i >= 0; i -= Time.deltaTime)
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
            text.text = insertText;
            backdrop.enabled = true;

            for (float i = 0; i <= 0.5; i += Time.deltaTime)
            {
                backdrop.color = new Color(0.1698f, 0.1698f, 0.1698f, i);
                text.color = new Color(120, 60, 120, i);

                yield return null;
            }



        }
    }
}

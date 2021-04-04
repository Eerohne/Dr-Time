using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxEffect : MonoBehaviour
{

    public Text txt;
    public string story;

    public void Awake()
    {
        txt = GetComponent<Text>();
        story = txt.text;
        txt.text = "";


        StartCoroutine("PlayText");
    }

    public IEnumerator PlayText()
    {
        foreach(char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.125f);
        }
    }

}

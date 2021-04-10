using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBoxEffect : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public TextBoxEffect before;
    //public Text txt;
    public string story;
    public float wait;

    public void Awake()
    {
        //txt = GetComponent<T>();
        story = txt.text;
        txt.text = "";


        StartCoroutine("PlayText");
    }

    public IEnumerator PlayText()
    {
        if(before != null)
        {
            yield return new WaitForSeconds(before.txt.text.Length * before.wait + wait);
        }

        foreach(char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(wait);
        }
    }

}

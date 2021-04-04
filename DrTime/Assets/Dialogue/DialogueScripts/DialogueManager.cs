using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public Dialogue currentDialogue;

    public Text nameText;
    public Text dialogueText;

    public float letterDelay = 0;

    private Queue<string> sentences;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else
        {
            string currentSentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(PlayText(currentSentence));
        }
    }

    public IEnumerator PlayText(string sentence)
    {
        dialogueText.text = "";

        foreach (char c in sentence.ToCharArray())
        {
            dialogueText.text += c;
            if (letterDelay == 0)
                yield return null;
            else
                yield return new WaitForSeconds(letterDelay);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        currentDialogue.closeEvent.Invoke();
    }
}

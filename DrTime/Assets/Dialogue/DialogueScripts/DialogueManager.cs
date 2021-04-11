using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public Dialogue currentDialogue; // Displayed Dialogue Object

    public Text nameText; // Name of the speeker
    public Text dialogueText; 

    public float letterDelay = 0; // Delay between letters

    private Queue<string> sentences; // List of blocks of text

    public Animator animator; // Reference to Dialogue Box Animator

    bool isTriggered = false;
    public bool isTutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); // Loads Sentence Queue
        if (currentDialogue != null)
            StartCoroutine("DefaultDialogue");
            
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTriggered && (((!PauseScript.GameIsPaused && !GameOverScript.gameOver) || isTutorial)))
            DisplayNextSentence();
    }

    // Launches Dialogue
    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;

        isTriggered = true;

        animator.SetBool("IsOpen", true);
        FindObjectOfType<AudioManager>().Play("Dialogue");

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    // Dequeues through the sentence queue and prints the appropriate sentence
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

    // Display Text char by char
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

    // Hides Dialogue Box and Plays Dialogue Transition sound
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        FindObjectOfType<AudioManager>().Play("Dialogue");
        isTriggered = false;
        currentDialogue.closeEvent.Invoke();
    }

    IEnumerator DefaultDialogue()
    {
        yield return new WaitForSeconds(.1f);
        StartDialogue(currentDialogue);
    }
}

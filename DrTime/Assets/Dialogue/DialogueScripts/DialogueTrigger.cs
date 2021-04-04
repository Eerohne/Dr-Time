using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool destroyUponTrigger = true;

    public bool isATrigger = true;
    public KeyCode[] triggerKeys;

    //public GameObject chainDialogueTrigger;
    //public GameObject invisibleWall;

    public float triggerDelay;

    private void Update()
    {
        if (!isATrigger)
        {
            if(triggerKeys.Length == 0)
            {
                StartCoroutine(TriggerTimer(triggerDelay));
                return;
            }

            foreach(KeyCode key in triggerKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    StartCoroutine(TriggerTimer(triggerDelay));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerDialogue();
        StartCoroutine("DestroyDialogueTrigger");
    }

    public void TriggerDialogue()
    {
        //dialogue.closeEvent.AddListener(EndTask);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    IEnumerator DestroyDialogueTrigger()
    {
        yield return new WaitForSeconds(1f);
        if(destroyUponTrigger)
            Destroy(gameObject);
    }

    IEnumerator TriggerTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        TriggerDialogue();
        Destroy(gameObject);
    }

    /*public void EndTask()
    {
        Debug.Log("End Of Task");

        if(chainDialogueTrigger != null)
        {
            chainDialogueTrigger.GetComponent<DialogueTrigger>().enabled = true;
        }

        if(invisibleWall != null)
        {
            invisibleWall.GetComponent<RoomTransition>().collider.enabled = false;
        }
    }*/
}

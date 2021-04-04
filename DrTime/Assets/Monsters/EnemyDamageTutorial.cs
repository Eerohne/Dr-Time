using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTutorial : MonoBehaviour
{
    public float damage;
    public float health;

    public DialogueTrigger attackDialogueTrigger;
    public DialogueTrigger deathDialogueTrigger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.SendMessage("Damage", damage);
            attackDialogueTrigger.TriggerDialogue();
            GetComponent<TrackPlayer>().canMove = false;
            Destroy(attackDialogueTrigger);
            damage = 0.01f;
        }
    }

    void Damage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        deathDialogueTrigger.TriggerDialogue();
        Destroy(deathDialogueTrigger);
    }
}

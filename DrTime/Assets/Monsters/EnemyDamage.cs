using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage; //Damage dealt to Player upon collision
    public float health; //Enemy Health

    public float attackDelay; // Delay Between Damage Output to the Player
    public float immunityBetweenAttacks; // Upon Receiving Damage, time in seconds before receiving damage again

    public bool isImmune = false;
    public bool canAttack = true;

    float attackDelayBU; // Saved attack delay
    float immunityBU; // Saved enemy imunity

    public string deathSound; // Name of the sound to play upon death

    public Renderer enemyGFX; // Refenrence to Sprite Renderer

    public Color damageColor = Color.red;
    private Color defaultColor;

    private void Start()
    {
        // saves initial states of timers
        attackDelayBU = attackDelay;
        immunityBU = immunityBetweenAttacks;

        defaultColor = enemyGFX.material.color; // Saves default color of enemy
    }

    // Times immunity and attack delay
    private void Update()
    {
        if (isImmune)
        {
            immunityBetweenAttacks -= Time.deltaTime;

            if (immunityBetweenAttacks <= 0.01f)
            {
                ColorShift(false);
                immunityBetweenAttacks = immunityBU;
                isImmune = false;
            }
        }

        if (!canAttack)
        {
            attackDelay -= Time.deltaTime;

            if (attackDelay <= 0.01f)
            {
                attackDelay = attackDelayBU;
                canAttack = true;
            }
        }
    }

    // If collides with player, deals damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (canAttack)
            {
                collision.gameObject.SendMessage("Damage", damage);
                canAttack = false;
            }
        }
    }

    // Called when Player deals damage to enemy
    void Damage(float damage)
    {
        if (!isImmune)
        {
            ColorShift(true);
            health -= damage;
            isImmune = true;
        }

        if (health <= 0)
            Destroy(this.gameObject);
    }

    // Plays death sound when destroyed
    private void OnDestroy()
    {
        if(deathSound != "")
            FindObjectOfType<AudioManager>().PlayIndividualSound(deathSound);
    }

    //Shifts color on immunity
    void ColorShift(bool shift)
    {
        if (shift)
            enemyGFX.material.color = damageColor;
        else
         enemyGFX.material.color = defaultColor;
    }
}

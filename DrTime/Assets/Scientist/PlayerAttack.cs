using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    FieldOfView fov; // For attack range

    public bool hasKnife; // true if player possesses a knife

    public float fistDamage = 1f;
    public float knifeDamage = 2f;

    public float attackDelay; // Delay Between Damage Output to an Enemy
    float attackDelayBU; // Saved attack delay
    public bool canAttack = true;

    public GameObject fistAttack; // Rference to the punch sprite
    public GameObject knifeAttack; // Reference to the slash sprite

    private void Awake()
    {
        fov = GetComponent<FieldOfView>(); // Imports FieldOfView
        attackDelayBU = attackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasKnife)
        {
            if(PlayerSystem.inventory.itemList[(int)Item.ItemType.Knife].amount > 0)
            {
                hasKnife = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
            Attack(); // Attacks if Player presses on Q

        if (!canAttack)
        {
            attackDelay -= Time.deltaTime;
            if(attackDelay <= 0.01f)
            {
                attackDelay = attackDelayBU;
                canAttack = true;
            }
        }
    }

    void Attack()
    {
        if (canAttack)
        {
            float damage;
            Debug.Log("Attack!");
            AttackSound();
            foreach (Transform enemy in fov.visibleEnemies)
            {
                // Summons the appropriate Sprite and deals the appropriate damage
                if (hasKnife)
                {
                    damage = knifeDamage;
                    Instantiate(knifeAttack, enemy.transform.position, Quaternion.identity);
                }
                else
                {
                    damage = fistDamage;
                    Instantiate(fistAttack, enemy.transform.position, Quaternion.identity);
                }
                enemy.SendMessage("Damage", damage);
                canAttack = false;
            }
        }
    }

    // Attack Sounds Manager
    void AttackSound()
    {
        if (hasKnife)
            FindObjectOfType<AudioManager>().Play("Slash");
        else
            FindObjectOfType<AudioManager>().Play("Punch");
    }
}
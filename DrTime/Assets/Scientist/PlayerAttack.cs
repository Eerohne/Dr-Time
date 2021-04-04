using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    FieldOfView fov;

    public bool hasKnife;

    public float fistDamage = 1f;
    public float knifeDamage = 2f;

    public GameObject fistAttack;
    public GameObject knifeAttack;

    private void Awake()
    {
        fov = GetComponent<FieldOfView>();
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
            Attack();
    }

    void Attack()
    {
        float damage;
        Debug.Log("Attack!");
        AttackSound();
        foreach (Transform enemy in fov.visibleEnemies)
        {
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
        }
    }

    void AttackSound()
    {
        if (hasKnife)
            FindObjectOfType<AudioManager>().Play("Slash");
        else
            FindObjectOfType<AudioManager>().Play("Punch");
    }
}
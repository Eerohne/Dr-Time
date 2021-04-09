using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour{

    public float health;
    public int heartsNum;

    public Image[] hearts;
    public Sprite coloredHeart;
    public Sprite shadedHeart;

    float lastHealth;
    float lastHeartNum;

    // Saves initial values
    private void Start()
    {
        lastHealth = health;
        lastHeartNum = heartsNum;
    }

    //Set the UI for the number of hearts and health
    private void Update(){
        if (lastHeartNum != heartsNum)
        {
            // Limits health to number of hearts
            if (health > heartsNum)
                health = heartsNum;

            for (int i = 0; i < hearts.Length; i++)
            {
                //Display a heart if the player has this as many hearts as heartsNum
                if (i < heartsNum)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }

        if(lastHealth != health)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                //Decide whether to show a full heart or an empty heart based on the player's health
                if (i < health)
                {
                    hearts[i].sprite = coloredHeart;
                }
                else
                {
                    hearts[i].sprite = shadedHeart;
                }
            }
        }

        lastHealth = health;
        lastHeartNum = heartsNum;
    }

    public void ChangeHealth(float newAmount) {
        health = newAmount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwakeBoss : MonoBehaviour
{
    public GameObject boss; // Reference to the boss
    public int amountToHave; // Amount of a given item to have in order to awaken the Boss
    public string pieceToCheck; // Name of the item in inventory to monitor

    public GameObject coreCounter;
    public GameObject coreImage;

    public Image backdrop;
    public Text text;
    private bool isInside = false;

    public string insertText;

    // Update is called once per frame
    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("AwakingBoss");
        }
    }

    // Shows insertion prompt when colliding with golem and having all pieces
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerSystem.inventory.GetNumberOfPuzzlePiece(pieceToCheck) >= amountToHave)
        {
            ShowText(false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerSystem.inventory.GetNumberOfPuzzlePiece(pieceToCheck) >= amountToHave)
        {
            ShowText(true);
        }
    }

    void ShowText(bool fade)
    {
        isInside = !fade;
        StartCoroutine(FadeText(fade));
    }

    IEnumerator FadeText(bool fadeOut)
    {
        //fade out
        if (fadeOut)
        {
            for (float i = 0.5f; i >= 0; i -= Time.deltaTime)
            {
                backdrop.color = new Color(0.1698f, 0.1698f, 0.1698f, i);
                text.color = new Color(120, 60, 120, i);

                yield return null;
            }

            backdrop.color = new Color(0.1698f, 0.1698f, 0.1698f, 0);
            text.color = new Color(120, 60, 120, 0);

        }

        //fade in
        else
        {
            text.text = insertText;
            backdrop.enabled = true;

            for (float i = 0; i <= 0.5; i += Time.deltaTime)
            {
                backdrop.color = new Color(0.1698f, 0.1698f, 0.1698f, i);
                text.color = new Color(120, 60, 120, i);

                yield return null;
            }
        }
    }

    // Activates boss and destroy core counter
    IEnumerator AwakingBoss()
    {
        Destroy(coreImage);
        Destroy(coreCounter);
        for (float i = 0.5f; i >= 0; i -= Time.deltaTime)
        {
            backdrop.color = new Color(0.1698f, 0.1698f, 0.1698f, i);
            text.color = new Color(120, 60, 120, i);

            yield return null;
        }

        backdrop.color = new Color(0.1698f, 0.1698f, 0.1698f, 0);
        text.color = new Color(120, 60, 120, 0);
        yield return new WaitForSeconds(.3f);
        boss.SetActive(true);
        Destroy(gameObject);
    }
}

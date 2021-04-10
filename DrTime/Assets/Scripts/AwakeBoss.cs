using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeBoss : MonoBehaviour
{
    public GameObject boss; // Reference to the boss
    public int amountToHave; // Amount of a given item to have in order to awaken the Boss
    public string pieceToCheck; // Name of the item in inventory to monitor

    // Update is called once per frame
    void Update()
    {
        if (PlayerSystem.inventory.GetNumberOfPuzzlePiece(pieceToCheck) == amountToHave)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        boss.SetActive(true);
    }
}

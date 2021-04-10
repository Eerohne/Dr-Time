using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmountFragments : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    int displayAmount = 0;
    public int currentAmount;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAmount = PlayerSystem.inventory.GetNumberOfPuzzlePiece("Fragment");

        if (currentAmount != displayAmount)
        {
            displayAmount = currentAmount;
            text.text = displayAmount.ToString();
        }
    }
}

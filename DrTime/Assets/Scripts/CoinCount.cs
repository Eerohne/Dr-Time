using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCount : MonoBehaviour
{
    float displayedAmount;

    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        displayedAmount = PlayerSystem.inventory.itemList[(int)Item.ItemType.Coins].amount;

        text = GetComponent<TextMeshProUGUI>();
        text.text = displayedAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float currentCoinCount = PlayerSystem.inventory.itemList[(int)Item.ItemType.Coins].amount;

        if (currentCoinCount != displayedAmount)
        {
            displayedAmount = currentCoinCount;

            text.text = displayedAmount.ToString();
        }
    }
}

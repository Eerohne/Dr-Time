using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmountMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    int displayCoins = 0;
    public int currentCoins;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCoins = PlayerSystem.inventory.itemList[0].amount;
        if (currentCoins != displayCoins) {
            displayCoins = currentCoins;
            text.text = displayCoins.ToString();
        }
    }
}

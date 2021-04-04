using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemAmount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] public int item;
    // 1 is health potion
    // 2 is damage potion
    // 3 is super potion
    int displayAmount = 0;
    public int currentAmount;

    private void Start()
    {

        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAmount = PlayerSystem.inventory.itemList[item].amount;
        if (currentAmount != displayAmount)
        {
            displayAmount = currentAmount;
            text.text = displayAmount.ToString();
        }
    }
}

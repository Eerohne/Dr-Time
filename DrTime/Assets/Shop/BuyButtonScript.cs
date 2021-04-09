using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyButtonScript : MonoBehaviour
{
    public Item item; // Sold Item
    [SerializeField] private TextMeshProUGUI text; // Reference to Situation Text
    private void Start()
    {
        Button btn = gameObject.GetComponent<Button>(); // Reference to current button
        btn.onClick.AddListener(TaskOnClick); //Adds listener to button
    }

    void TaskOnClick()
    {
        //buy item, remove price from coins, add item to inventory, if knife can buy only one
        int coins = PlayerSystem.inventory.itemList[0].amount;
        int cost = Item.GetCost(item.itemType);

        if((coins - cost) < 0){
            text.text = "Not enough coins";
            StartCoroutine("Wait");
            FindObjectOfType<AudioManager>().Play("Deny");
        }

        if (item.itemType == Item.ItemType.Knife){
            foreach (Item item in PlayerSystem.inventory.itemList){
                if(item.itemType == Item.ItemType.Knife){
                    if (PlayerSystem.inventory.itemList[4].amount > 0){
                        text.text = "You already own one!";
                        FindObjectOfType<AudioManager>().Play("Deny");
                        StartCoroutine("Wait");
                        return;
                    }
                }
            }
        }

        if ((coins - cost) >= 0){
            PlayerSystem.inventory.itemList[0].amount -= cost;
            FindObjectOfType<AudioManager>().Play("Purchase");
            PlayerSystem.inventory.AddItem(item);
        }
        Debug.Log(PlayerSystem.inventory.ToString());
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(1);
        text.text = "";
    }
}

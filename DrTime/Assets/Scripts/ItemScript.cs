using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Item.ItemType type;
    public int amount;
    public float effect;

    public string itemName;

    private Item item;

    private void Start()
    {
        item = new Item(type, amount, effect, itemName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("Collect", item);
            Destroy(gameObject);
        }
    }

    public Item GetItem()
    {
        return item;
    }
}

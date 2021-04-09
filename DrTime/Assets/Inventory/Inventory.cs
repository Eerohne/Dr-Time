using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory{

    public Item item;
    public List<Item> itemList;

    public Inventory () {
        //creates list of items
        itemList = new List<Item>();
        
        AddItem(new Item(Item.ItemType.Coins, 500)); //position 0
        AddItem(new Item(Item.ItemType.HealthPotion, 0, 3)); // position 1
        AddItem(new Item(Item.ItemType.DamagePotion, 1000, 2)); // position 2
        AddItem(new Item(Item.ItemType.SuperPotion, 0, 3)); // position 3
        AddItem(new Item(Item.ItemType.Knife, 0)); // position 4
        // position 5 is first puzzle piece
        // position 6 is 2nd puzzle piece
        // position 7 is 3rd puzzle piece
    }

    public static Item GetItem (Item item){
        return item;
    }

    // see if item is stackable or else create a new one
    public void AddItem (Item item){
        if (item.IsStackable())
        {
            bool isInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    isInInventory = true;
                }
            }
            if (!isInInventory)
            {
                itemList.Add(item);
            }
        }else{
            itemList.Add(item);
        }
    }

    public override string ToString()
    {
        string s = " ";

        foreach (Item item in itemList)
        {
            s += item.itemType + ", " + item.amount + " ";
        }

        return s;
    }
}

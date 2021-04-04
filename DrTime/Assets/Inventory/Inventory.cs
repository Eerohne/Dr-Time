using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory{

    public Item item;
    public List<Item> itemList;

    public Inventory () {
        //creates list of items
        itemList = new List<Item>();
        
        AddItem(new Item(Item.ItemType.Coins, 500));
        AddItem(new Item(Item.ItemType.HealthPotion, 0, 3));
        AddItem(new Item(Item.ItemType.DamagePotion, 10, 2));
        AddItem(new Item(Item.ItemType.SuperPotion, 0, 3));
        AddItem(new Item(Item.ItemType.Knife, 0));
    }

    public static Item GetItem (Item item){
        return item;
    }

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

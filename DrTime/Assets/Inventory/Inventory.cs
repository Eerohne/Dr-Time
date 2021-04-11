using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory{

    public Item item;
    public List<Item> itemList;

    public Inventory () {
        //creates list of items
        itemList = new List<Item>();
        
        AddItem(new Item(Item.ItemType.Coins, 0)); //position 0
        AddItem(new Item(Item.ItemType.HealthPotion, 0, 3)); // position 1
        AddItem(new Item(Item.ItemType.DamagePotion, 0, 2)); // position 2
        AddItem(new Item(Item.ItemType.SuperPotion, 0, 3)); // position 3
        AddItem(new Item(Item.ItemType.Knife, 0)); // position 4
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

    public int GetNumberOfPuzzlePiece(string _name)
    {
        int amount = 0;

        foreach (Item i in itemList)
        {
            if (i.name == _name)
                amount += i.amount;
        }

        return amount;
    }
}

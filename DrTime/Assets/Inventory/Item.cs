using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class Item 
{ 
    //creates items with a type and an amount
    public enum ItemType{
        Coins,
        HealthPotion,
        DamagePotion, 
        SuperPotion,
        Knife,
        PuzzlePieces,
    }

    public ItemType itemType;
    public int amount;
    public float effect; // How much damage or health gained

    public string name; // For Puzzle Pieces

    public Item(ItemType type, int amount) : this(type, amount, 0f, "") { }

    public Item(ItemType type, int amount, float effect) : this(type, amount, effect, "") { }
    public Item(ItemType type, int amount, float effect, string name) 
    {
        itemType = type;
        this.amount = amount;
        this.effect = effect;
        this.name = name;
    }

    public int GetAmount(){
        return amount;
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.DamagePotion:
            case ItemType.HealthPotion:
            case ItemType.SuperPotion:
            case ItemType.Knife:
                return true;
            case ItemType.PuzzlePieces:
                return false;
        }
    }

    public static int GetCost(ItemType itemType){
        switch (itemType) {
            default:
            case ItemType.DamagePotion:  return 10;
            case ItemType.SuperPotion:   return 20;
            case ItemType.HealthPotion:  return 15;
            case ItemType.Knife:         return 75;
        }
    }
}

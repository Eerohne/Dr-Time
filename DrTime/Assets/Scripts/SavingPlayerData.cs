using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // makes it so that we can save it in a file 
public class SavingPlayerData
{
    //we chose to only save the inventory and the player will respawn in the lobby 
    public Inventory inventory;

    public SavingPlayerData ()
    {
        this.inventory = PlayerSystem.inventory; 
    }
}

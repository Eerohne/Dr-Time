using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    //the name of the object which is required to be in the player's inventory in order to destroy the object in question
    public string name;

  
    void Update()
    {
        //loop through every item in the player's inventory
        foreach (Item item in PlayerSystem.inventory.itemList)
        {
            //if the current item is the item which we are looking for
            if (item.name == name)
            {
                //destroy the object in question
                Destroy(gameObject);
            }
        }


    }




}

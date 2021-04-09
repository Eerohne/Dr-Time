using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyInvisibleWall : MonoBehaviour
{
    public string name;

  
    void Start()
    {

        foreach (Item item in PlayerSystem.inventory.itemList)
        {
            if (item.name == name)
            {
                Destroy(gameObject);
            }
        }


    }




}

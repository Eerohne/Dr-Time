using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFragments : MonoBehaviour
{
    // Clears all fragments at the beginning of level 3
    void Update()
    {
        for (int i = 0; i < PlayerSystem.inventory.itemList.Count; i++)
        {
            Debug.Log(PlayerSystem.inventory.itemList[i].name + " " + i);

            if (PlayerSystem.inventory.itemList[i].name.Equals("Fragment"))
            {
                PlayerSystem.inventory.itemList.RemoveAt(i);
            }
        }

        Destroy(gameObject.GetComponent<ClearFragments>());
    }
}

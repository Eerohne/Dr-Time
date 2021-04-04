using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        inventory = PlayerSystem.inventory;
    }

    private void Update()
    {
        
    }
}

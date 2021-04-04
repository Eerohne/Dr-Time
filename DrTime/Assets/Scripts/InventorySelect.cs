using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelect : MonoBehaviour
{
    //put an outer rim image if the key is pressed (for player to know which item is selected in the inventory)
    [SerializeField] Image image1;
    [SerializeField] Image image2;
    [SerializeField] Image image3;

    // Start is called before the first frame update
    void Start()
    {
        image1.enabled = false;
        image2.enabled = false;
        image3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            image1.enabled = true;
            image2.enabled = false;
            image3.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            image1.enabled = false;
            image2.enabled = true;
            image3.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            image1.enabled = false;
            image2.enabled = false;
            image3.enabled = true;
        }
    }
}

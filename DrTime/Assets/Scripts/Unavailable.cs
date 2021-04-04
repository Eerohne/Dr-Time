using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unavailable : MonoBehaviour
{
    //If none in the inventory puts image over (For player to know if available)
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
        if (PlayerSystem.inventory.itemList[1].amount == 0)
        {
            image1.enabled = true;
        }
        else { image1.enabled = false; }

        if (PlayerSystem.inventory.itemList[2].amount == 0)
        {
            image2.enabled = true;
        }
        else { image2.enabled = false; }

        if (PlayerSystem.inventory.itemList[3].amount == 0)
        {
            image3.enabled = true;
        }
        else { image3.enabled = false; }
    }
}

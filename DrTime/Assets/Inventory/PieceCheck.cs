using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceCheck : MonoBehaviour
{
    public string pieceName;

    public Sprite pieceSprite;
    Image img;

    //private Inventory inv;

    void Start()
    {
        img = GetComponent<Image>();
        //inv = PlayerSystem.inventory;
    }

    // Update is called once per frame
    void Update()
    {
        if(img.sprite != pieceSprite)
        {
            foreach (Item i in PlayerSystem.inventory.itemList)
            {
                if (i.name == this.pieceName)
                {
                    ShowSprite(i);
                }
            }
        }
    }

    void ShowSprite(Item i)
    {
        img.sprite = pieceSprite;
        Debug.Log("Captured Item : " + i.name);
    }
}

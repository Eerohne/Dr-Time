using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceCheck : MonoBehaviour
{
    public string pieceName;

    public Sprite pieceSprite;
    Image img;

    private Inventory inv;

    void Start()
    {
        img = GetComponent<Image>();
        inv = PlayerSystem.inventory;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShowSprite(new Item(Item.ItemType.Coins, 1));
        }

        if(img.sprite != pieceSprite)
        {
            foreach (Item i in inv.itemList)
            {
                if (i.name == this.name)
                {
                    ShowSprite(i);
                }
            }
        }
        else
        {
            Debug.Log("Already Done");
        }
        
    }

    void ShowSprite(Item i)
    {
        img.sprite = pieceSprite;
        Debug.Log("Captured Item : " + i.name);
    }
}

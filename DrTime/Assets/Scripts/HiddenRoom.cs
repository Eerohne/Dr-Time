using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenRoom : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<TilemapRenderer>().sortingLayerName = "Background";
            gameObject.GetComponent<TilemapRenderer>().sortingOrder = 0;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<TilemapRenderer>().sortingLayerName = "Foreground";
            gameObject.GetComponent<TilemapRenderer>().sortingOrder = 3;
        }
    }

}

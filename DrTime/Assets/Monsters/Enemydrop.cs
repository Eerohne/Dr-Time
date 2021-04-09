using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemydrop : MonoBehaviour
{
    public GameObject item;//item you want after death

    private void OnDestroy()//when the enemy dies
    {
        Instantiate(item, transform.position, item.transform.rotation); //drop the item
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverSound : MonoBehaviour
{
    public void PlayeHoverSound()
    {
        FindObjectOfType<AudioManager>().Play("Hover");
    }
}

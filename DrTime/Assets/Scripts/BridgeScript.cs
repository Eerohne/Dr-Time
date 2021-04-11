using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    public Transform otherSide; // Other Side of the bridge
    public PathFollow playerPathControl;

    public float inactivityTimer = 1f;
    public bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        playerPathControl = FindObjectOfType<PathFollow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(playerPathControl.target == null)
            {
                playerPathControl.enabled = true;
                playerPathControl.target = otherSide;
            }
            else if (playerPathControl.target == transform)
            {
                Destroy(gameObject);
                playerPathControl.enabled = false;
            }
            else
            {
                playerPathControl.enabled = true;
                playerPathControl.target = otherSide;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerPathControl.target == null)
            {
                playerPathControl.enabled = true;
                playerPathControl.target = otherSide;
            }
            else if (playerPathControl.target == transform)
            {
                Destroy(gameObject);
                playerPathControl.enabled = false;
            }
            else
            {
                playerPathControl.enabled = true;
                playerPathControl.target = otherSide;
            }
        }
    }
}

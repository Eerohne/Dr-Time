using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayer : MonoBehaviour
{
    public bool block;

    public PlayerSystem player;

    public float delay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine("Block", delay);
        //player.SetFree(!block);
    }

    IEnumerator Block(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        player.SetFree(!block);
    }
}

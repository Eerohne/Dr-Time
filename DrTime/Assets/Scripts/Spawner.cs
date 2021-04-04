using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawn;
    public int amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(spawn, gameObject.transform.position, Quaternion.identity);
            yield return null;
        }
    }
}

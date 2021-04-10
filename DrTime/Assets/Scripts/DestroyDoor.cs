using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoor : MonoBehaviour
{
    public GameObject door; // Reference To Door

    // When Enemy Is Destroyed, destroys door
    private void OnDestroy()
    {
        Destroy(door);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUponDestruction : MonoBehaviour
{
    public List<GameObject> objects;

    // Enables all objects in the list on destruction of gameObject
    private void OnDestroy()
    {
        foreach(GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }
}

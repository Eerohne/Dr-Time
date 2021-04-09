using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThrower : MonoBehaviour
{
    public Transform target;

    public ProjectileMotion motion;

    private void Start()
    {
        motion = GameObject.Find("ProjectileManager").GetComponent<ProjectileMotion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            motion.Throw(transform.position, true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    FollowAI followScript;
    PatrolAI patrolScript;

    public LayerMask playerMask;

    public float detectionRadius = 3f;

    private void Start()
    {
        followScript = GetComponent<FollowAI>();
        patrolScript = GetComponent<PatrolAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(gameObject.transform.position, detectionRadius, playerMask))
        {
            followScript.enabled = true;
            patrolScript.enabled = false;
        }
        else
        {
            patrolScript.enabled = true;
            followScript.enabled = false;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}

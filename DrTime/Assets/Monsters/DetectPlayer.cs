using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    FollowAI followScript;
    PatrolAI patrolScript;

    public LayerMask playerMask;

    public float detectionRadius = 3f;

    public Animator anim;
    public bool specialAnim = false;

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
            SwitchScript(true);
        }
        else
        {
            SwitchScript(false);
        }
    }

    void SwitchScript(bool follow)
    {
        followScript.enabled = follow;
        if (!specialAnim)
            anim.SetBool("isWalking", follow);
        patrolScript.enabled = !follow;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}

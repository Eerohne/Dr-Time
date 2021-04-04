using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PatrolAI : MonoBehaviour
{
    public List<Transform> targets;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float pauseLength = 1f;

    public Transform enemyGFX;

    protected Path path;
    protected int currentWaypoint = 0;

    protected Seeker seeker;
    protected Rigidbody2D rb;

    private bool destinationReached = false;

    int targetCount;
    int currentTargetIndex;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        targetCount = targets.Count;
        currentTargetIndex = 0;
    }

    private void OnEnable()
    {
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    protected void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, targets[currentTargetIndex].position, OnPathComplete);
        }
    }

    protected void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;

            if (destinationReached)
                destinationReached = false;
        }
    }

    void FixedUpdate()
    {
        if (destinationReached)
            return;

        if (path == null)
            return;

        if ((currentWaypoint >= path.vectorPath.Count))
        {
            Debug.LogWarning("Reached End! Path Count : " + path.vectorPath.Count);
            destinationReached = true;
            currentTargetIndex++;
            if (currentTargetIndex >= targetCount)
            {
                currentTargetIndex = 0;
            }
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
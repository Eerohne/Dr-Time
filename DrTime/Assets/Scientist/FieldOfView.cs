using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public enum FOVType
    {
        Player, Enemy, Projectile
    }

    bool InGame = false;
    public float viewRadius = 3f;

    [Range(0, 360)]
    public float viewAngle = 45f;

    SystemInterface system;

    public LayerMask enemyMask;
    public LayerMask obstacleMask;

    public FOVType type;

    [HideInInspector]
    public List<Transform> visibleEnemies = new List<Transform>();

    private void Awake()
    {
        switch (type)
        {
            case FOVType.Player:
                system = GetComponent<PlayerSystem>() as PlayerSystem;
                break;
            case FOVType.Enemy:
                //GetScript
                break;
            case FOVType.Projectile:
                system = GetComponent<Projectile>() as Projectile;
                break;
        }
        InGame = true;
    }

    private void Start()
    {
        StartCoroutine("FindEnemyWithDelay", .2F);
    }

    IEnumerator FindEnemyWithDelay(float delayInSeconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(delayInSeconds);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleEnemies.Clear();
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, viewRadius, enemyMask);

        for(int i = 0; i < enemies.Length; i++)
        {
            Transform target = enemies[i].transform;
            Vector3 directionToTarget = (transform.position - target.position).normalized;
            if(Vector3.Angle(-system.GetDirection(), directionToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics2D.Raycast(transform.position, target.position, dstToTarget, obstacleMask))
                {
                    visibleEnemies.Add(target);
                }
            }
        }
    }

    public Vector3 DirectionFromAngle(float angleInDegrees)
    {
        if(InGame)
            angleInDegrees += PlayerDirection();
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    float PlayerDirection()
    {
        float directionX = system.GetDirection().x;
        float directionY = system.GetDirection().y;
        float angle = 0f;

        if (directionY > 0) angle = 0f;
        else if (directionX < 0) angle = 270f;
        else if (directionY < 0) angle = 180f;
        else if (directionX > 0) angle = 90f;
        

        return angle;
    }
}
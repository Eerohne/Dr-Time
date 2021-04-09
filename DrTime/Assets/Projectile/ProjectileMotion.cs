using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public bool isSideView;

    public GameObject damagePotion;
    public GameObject superPotion;

    public float gravity;
    public float initialVelocity = 1f;
    float actualVelocity;

    public float projectileScale = 1f;

    public float projectileRange = 5f;

    public Sound potionBreak;


    private void Update()
    {       
        //projectileRange = initialVelocity * initialVelocity * Mathf.Sin(2 * Mathf.Deg2Rad * 45) / gravity;
    }

    public void Throw(Vector2 source, bool isSuperPotion)
    {
        PlayerSystem player = FindObjectOfType<PlayerSystem>();

        float launchAngle = ComputeLaunchAngle();

        ActualVelocity(player);
        projectileRange = actualVelocity * actualVelocity * Mathf.Sin(2 * launchAngle) / gravity;

        
        Vector2 targetPosition = transform.position;

        targetPosition.x += player.GetDirection().x * projectileRange;
        targetPosition.y += player.GetDirection().y * projectileRange;

        

        if (isSuperPotion)
        {
            Projectile.Instantiate(superPotion, Quaternion.identity, source, targetPosition, launchAngle, gravity, actualVelocity, projectileScale, isSideView);
        }
        else
        {
            Projectile.Instantiate(damagePotion, Quaternion.identity, source, targetPosition, launchAngle, gravity, actualVelocity, projectileScale, isSideView);
        }
    }

    void ActualVelocity(PlayerSystem player)
    {
        actualVelocity = initialVelocity;
        if (player.GetMovement().sqrMagnitude >= 0.1f)
            actualVelocity += player.speed;
    }

    float ComputeLaunchAngle(/*Vector2 source, Vector2 target*/)
    {
        /*float h = source.y - target.y;
        float x = target.x - source.x;*/

        float shift = 0;
        if (FindObjectOfType<PlayerSystem>().GetDirection().x < 0)
            shift = Mathf.PI/2;
        
        /*if(!isSideView || h == 0)
            return Mathf.PI / 4 + shift;//0.5F * Mathf.Asin((gravity * x)/(initialVelocity * initialVelocity));

        if (h != 0)
        {
            float a1 = Mathf.Atan((actualVelocity * actualVelocity + Mathf.Sqrt(Mathf.Pow(actualVelocity, 4) - gravity * (gravity * x * x + 2 * h * actualVelocity * actualVelocity)))/gravity * x);
            float a2 = Mathf.Atan((actualVelocity * actualVelocity - Mathf.Sqrt(Mathf.Pow(actualVelocity, 4) - gravity * (gravity * x * x + 2 * h * actualVelocity * actualVelocity)))/gravity * x);

            if (a1 > 0)
            {
                if (a2 > 0)
                {
                    if (a1 > a2)
                        return a2;
                }

                return a1;
            }
            else return a2;
        }*/

        return Mathf.PI / 4 + shift;//0.5F * Mathf.Asin((gravity * x)/(initialVelocity * initialVelocity));
    }
}
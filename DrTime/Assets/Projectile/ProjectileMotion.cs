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

    public float projectileScale = 1f;

    public float projectileRange = 5f;

    private void Update()
    {       
        projectileRange = initialVelocity * initialVelocity * Mathf.Sin(2 * Mathf.Deg2Rad * 45) / gravity;
    }

    public void Throw(Vector2 source, Vector2 target, bool isSuperPotion)
    {

        float launchAngle = ComputeLaunchAngle(source, target);

        if (isSuperPotion)
        {
            Projectile.Instantiate(superPotion, Quaternion.identity, source, target, launchAngle, gravity, initialVelocity, projectileScale, isSideView);
        }
        else
        {
            Projectile.Instantiate(damagePotion, Quaternion.identity, source, target, launchAngle, gravity, initialVelocity, projectileScale, isSideView);
        }
    }

    float ComputeLaunchAngle(Vector2 source, Vector2 target)
    {
        float h = source.y - target.y;
        float x = target.x - source.x;

        float shift = 0;
        if (FindObjectOfType<PlayerSystem>().GetDirection().x < 0)
            shift = 90;
            

        if (h == 0)
            return 45f + shift;//0.5F * Mathf.Asin((gravity * x)/(initialVelocity * initialVelocity));
        else
            return ((Mathf.Acos((((gravity * x * x) / (initialVelocity * initialVelocity)) - h) / Mathf.Sqrt(h * h + x * x)) + Mathf.Atan(x / h)) / 2) + shift;
    }
}
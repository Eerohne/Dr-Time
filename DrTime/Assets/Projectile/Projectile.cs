using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : SystemInterface
{
    public GameObject breakEffect;

    bool isSideView;
    bool atDestination = false;
    bool justStarted = true;

    float startTimer = 0.1f;

    float launchAngle;
    float gravity;
    float initialVelocity;

    Vector2 startPos;
    Vector2 endPos;

    Vector2 velocity;
    Vector2 direction;

    LayerMask enemyLayer;
    LayerMask obstacleLayer;

    Rigidbody2D rb;
    FieldOfView fov;
    Item item;
    PlayerSystem player;
    ProjectileMotion launcher;

    float timeAtLaunch;

    float initialScale;
    float maxScaleIncrementation = 0.5f;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fov = GetComponent<FieldOfView>();
        item = GetComponent<ItemScript>().GetItem();
        launcher = FindObjectOfType<ProjectileMotion>();

        enemyLayer = fov.enemyMask;
        obstacleLayer = fov.obstacleMask;

        timeAtLaunch = Time.time;

        initialScale = transform.localScale.x;
    }

    private void Awake()
    {
        player = FindObjectOfType<PlayerSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (justStarted)
        {
            startTimer -= Time.deltaTime;

            if (startTimer <= 0)
            {
                justStarted = false;
            }
        }

        if (isSideView)
        {
            rb.MovePosition(ComputeNewPosition());
        }
        else
        {
            if (atDestination)
                DealDamage();
            rb.MovePosition(ComputeNewPosition());
        }

        if (Vector3.Distance(startPos, transform.position) > Vector3.Distance(startPos, endPos))
            atDestination = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!justStarted)
            DealDamage();
    }

    void DealDamage()
    {
        if (!isSideView)
        {
            Debug.Log("Top View Damage");
        } else Debug.Log("Damage");


        foreach (Transform enemy in fov.visibleEnemies)
        {
            enemy.gameObject.SendMessage("Damage", item.effect);
        }

        FindObjectOfType<AudioManager>().PlayIndividualSound("PotionBreak");
        Destroy(gameObject);
    }

    public void OnCreated(float _launchAngle, float _gravity, float _initialVelocity, Vector2 startPosition, Vector2 endPosition, bool sideView)
    {
        gravity = _gravity;

        launchAngle = _launchAngle;

        startPos = startPosition;
        endPos = endPosition;

        initialVelocity = _initialVelocity;

        isSideView = sideView;

        ComputeVelocity();
    }

    void ComputeVelocity()
    {

        //velocity = player.GetVelocity();//FindObjectOfType<PlayerSystem>().GetVelocity();

        direction = player.GetDirection();

        velocity.x += initialVelocity * direction.x * Mathf.Cos(launchAngle);
        velocity.y += initialVelocity * Mathf.Sin(launchAngle);

        /*if(!isSideView)
            if (player.GetVelocity().sqrMagnitude >= 0.1f)
                velocity.x += player.speed * player.GetMovement().x;
        if (isSideView)
        {
            vel.x += (endPos.x - startPos.x) / timeTilHit;
            vel.y += ((endPos.y - startPos.y) - (0.5f * -gravity * timeTilHit * timeTilHit)) / timeTilHit;
        }
        else
        {
            vel.x += (endPos.x - startPos.x) / timeTilHit;
            vel.y += (endPos.y - startPos.y) / timeTilHit;

            //Vector2 heading = (endPos - startPos);
            //float distance = heading.magnitude;

            //vel = (heading / distance) * (endPos.x - startPos.x) / timeTilHit;

            Debug.Log(vel);
        }*/
    }

    Vector2 ComputeNewPosition()
    {
        float deltaTime = Time.time - timeAtLaunch;

        Vector2 newPosition = Vector2.zero;


        if (isSideView)
        {
            newPosition.x = velocity.x * deltaTime * direction.x + startPos.x;

            newPosition.y = velocity.y * deltaTime + 0.5f * -gravity * deltaTime * deltaTime + startPos.y;
        }
        else 
        {
            float vel = Mathf.Abs(initialVelocity * Mathf.Cos(launchAngle));
            //vel = Mathf.Abs(velocity.x);

            newPosition.x = vel * deltaTime * direction.x + startPos.x;

            newPosition.y = vel * deltaTime * direction.y + startPos.y; 

            /*if(Vector2.Distance(startPos, newPosition) < distance / 2)
                transform.localScale = new Vector2(transform.localScale.x + maxScaleIncrementation * Time.deltaTime, transform.localScale.x + maxScaleIncrementation * Time.deltaTime);
            else
                transform.localScale = new Vector2(transform.localScale.x - maxScaleIncrementation * Time.deltaTime, transform.localScale.x - maxScaleIncrementation * Time.deltaTime);*/
        }

        return newPosition;
    }

    public override Vector2 GetDirection()
    {
        return velocity;
    }

    public static Object Instantiate(Object original, Quaternion rotation, Vector2 start, Vector2 end, float launchAngle, float gravity, float initialVelocity, float scale, bool sideView)
    {
        GameObject projectile = Object.Instantiate(original, start, rotation) as GameObject;

        Projectile p = projectile.GetComponent<Projectile>();
        p.transform.localScale = new Vector3(scale, scale);


        p.OnCreated(launchAngle, gravity, initialVelocity, start, end, sideView);

        return p;
    }

    private void OnDestroy()
    {
        Instantiate(breakEffect, transform.position, Quaternion.identity);
    }
}

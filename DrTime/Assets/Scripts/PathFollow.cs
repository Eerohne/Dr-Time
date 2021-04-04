using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    public Transform target;
    public float speed;

    Animator anim;

    Vector2 movement;

    private void Start()
    {
        //target = GetComponent<Transform>();
        movement = Vector2.zero;
        anim = GetComponent<Animator>();

        gameObject.GetComponent<PlayerSystem>().isAnimation = true;
    }

    private void Update()
    {
        movement.x = (target.position.x - transform.position.x + 0.01f);
        movement.y = (target.position.y - transform.position.y + 0.01f);

        movement.x = Mathf.Clamp01(movement.x);
        movement.y = Mathf.Clamp01(movement.y);

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if (transform.position == target.position)
        {
            movement = Vector2.zero;
            gameObject.GetComponent<PlayerSystem>().isFree = true;
            gameObject.GetComponent<PlayerSystem>().isAnimation = false;
            Destroy(gameObject.GetComponent("PathFollow"));
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    public Transform target;
    public float speed;

    Animator anim;
    PlayerSystem player;

    Vector2 movement;

    private void Start()
    {
        //target = GetComponent<Transform>();
        movement = Vector2.zero;
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerSystem>();

        player.isAnimation = true;
        player.PlayWalkingSound(false);
    }

    private void OnEnable()
    {
        if(player != null)
        {
            player.PlayWalkingSound(false);
            player.isAnimation = true;
            player.isFree = false;
        }
    }

    private void OnDisable()
    {
        movement = Vector2.zero;
        player.PlayWalkingSound(true);
        player.isFree = true;
        player.isAnimation = false;
        target = null;
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
            GetComponent<PathFollow>().enabled = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    // Sets and prepares target
    public void SetTarget(GameObject _target)
    {
        target = _target.transform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 10f;

    Transform nextRoom;
    bool shouldMove;

    private void Update()
    {
        //Debug.Log("Should Move?");
        if (shouldMove)
        {
            //Debug.Log("YASS");
            transform.position = Vector3.Lerp(transform.position, nextRoom.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, nextRoom.position) <= 0.1)
            {
                //Debug.Log("Done Move");
                shouldMove = false;
            }
        }
    }

    public void Move(Transform room)
    {
        nextRoom = room;
        shouldMove = true;
    }
}

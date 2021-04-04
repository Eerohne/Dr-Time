using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomTransition : MonoBehaviour
{
    public RoomTracker tracker;

    public Transform previousRoom;
    public Transform nextRoom;

    public BoxCollider2D triggerZone;

    private void Awake()
    {
        triggerZone = transform.Find("InvisibleWall").GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Transition");

        Transform sourceRoom = tracker.currentRoom;

        if(sourceRoom.position == nextRoom.position)
        {
            tracker.ChangeRoom(previousRoom);
        }
        else
        {
            tracker.ChangeRoom(nextRoom);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerZone.enabled = true;
    }
}

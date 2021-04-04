using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracker : MonoBehaviour
{
    public Transform currentRoom;

    public CameraMovement camMovement;

    public void ChangeRoom(Transform room)
    {
        currentRoom = room;
        camMovement.SendMessage("Move", room);
    }
}

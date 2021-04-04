using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class IPathfinder : MonoBehaviour
{
    

    protected abstract void UpdatePath();

    protected abstract void OnPathComplete(Path p);

    public void Resume()
    {
        
    }
}

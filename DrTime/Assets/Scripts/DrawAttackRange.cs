#if (UNITY_EDITOR) 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class DrawAttackRange : Editor
{
    private void OnSceneGUI()
    {
        Handles.color = Color.white;
        FieldOfView fov = (FieldOfView)target;

        Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.up, 360, fov.viewRadius);

        Vector3 viewAngleRight = fov.DirectionFromAngle(fov.viewAngle / 2);
        Vector3 viewAngleLeft = fov.DirectionFromAngle(-fov.viewAngle / 2);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov.viewRadius);

        Handles.color = Color.red;
        foreach (Transform enemy in fov.visibleEnemies)
        {
            Handles.DrawLine(fov.transform.position, enemy.transform.position);
        }
    }
}

#endif


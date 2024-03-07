using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;

    private float gizmoSphereRadius = 0.2f;


    public int GetNextIndex(int currIndex)
    {
        int nextIndex = currIndex + 1;

        // If wayPoints[nextIndex] doesn't exist, return 0 as nextIndex
        if (nextIndex >= wayPoints.Length)
        {
            nextIndex = 0;
        }

        return nextIndex;
    }

    public Vector3 GetWayPoint(int index)
    {
        return wayPoints[index].position;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            Gizmos.DrawSphere(wayPoints[i].position, gizmoSphereRadius);
            // Draw lines between wayPoints
            Gizmos.DrawLine(wayPoints[i].position, wayPoints[GetNextIndex(i)].position);
        }
    }
}

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoadWaypoint : MonoBehaviour
{
    public RoadWaypoint[] availablePoints;
    private int random;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public RoadWaypoint GetNextPoint()
    {
        random = Random.Range(0, availablePoints.Length);
        return availablePoints[random];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 start = transform.position;

        foreach (var point in availablePoints)
        {
            Vector3 dir = (point.transform.position - start).normalized;
            Vector3 end = start + dir;

            Gizmos.DrawLine(start, point.transform.position);

            // Вершина стрелки
            Vector3 right = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 150, 0) * Vector3.forward;
            Vector3 left = Quaternion.LookRotation(dir) * Quaternion.Euler(0, -150, 0) * Vector3.forward;

            Gizmos.DrawLine(point.transform.position, point.transform.position + right * 0.2f * 10);
            Gizmos.DrawLine(point.transform.position, point.transform.position + left * 0.2f * 10);
        }
    }
}

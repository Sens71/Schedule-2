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
}

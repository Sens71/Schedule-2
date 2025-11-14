using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CarNavigation : MonoBehaviour
{
    public float nextPointDistanceTrigger = 2f;
    private RoadWaypoint currentWaypoint;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        RoadWaypoint[] points = FindObjectsByType<RoadWaypoint>(FindObjectsSortMode.None);
        points = points.Where(type => type.waypointType == WaypointType.Car).ToArray();
        float minDist = Mathf.Infinity;

        foreach (var p in points)
        {
            float dist = Vector3.Distance(transform.position, p.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                currentWaypoint = p;
            }
        }
        agent.destination = currentWaypoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaypoint == null) 
            return;
        float distance = Vector3.Distance(transform.position, currentWaypoint.transform.position);
        if (distance < nextPointDistanceTrigger)
        {
            currentWaypoint = currentWaypoint.GetNextPoint();
            agent.destination = currentWaypoint.transform.position;
        }
    }
}

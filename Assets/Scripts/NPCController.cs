using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private StatsHandler statsHandler;
    private Weapon weapon;
    public float _distance;
    public float viewAngle;
    private Player player;
    private NavMeshAgent agent;
    public LayerMask mask;
    public float rotationSpeed;
    private bool canShoot;
    private bool isMoving;
    private Animator animator;
    public float radius;
    public float deathTime;
    private float timer;
    public Transform[] patrolPoints;
    public bool isAggresive;

    public float nextPointDistanceTrigger = 2f;
    private RoadWaypoint currentWaypoint;
    public float alertRadius;
    public bool ragdollState;
    

    void Awake()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
        weapon.playerWeapon = false;
        statsHandler = GetComponent<StatsHandler>();
        player = FindAnyObjectByType<Player>();
        agent = GetComponent<NavMeshAgent>();
        SetRagDoll(false);

        RoadWaypoint[] points = FindObjectsByType<RoadWaypoint>(FindObjectsSortMode.None);
        points = points.Where(type => type.waypointType == WaypointType.Pedestrians).ToArray();
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
    private void OnEnable()
    {
        SetRagDoll(false);
        statsHandler.OnDeath += Death;
        agent.speed = 3.5f;
        statsHandler.OnHealthChanged += OnDamageTaken;
    }
    private void OnDisable()
    {
        statsHandler.OnDeath -= Death;
        statsHandler.OnHealthChanged -= OnDamageTaken;
    }

    void Update()
    {
        Navigation();
        animator.SetBool("isShooting", canShoot);
        animator.SetBool("isWalking", isMoving);
        
    }
    public void Shoot()
    {
        weapon.RemoteFire();
    }
    public async void Death(GameObject go)
    {
        SetRagDoll(true);
        agent.enabled = false;  
        isAggresive = false;
        canShoot = false;
        await Awaitable.WaitForSecondsAsync(60);
        if (!gameObject.activeSelf)
        {
            Destroy(go);
        }
    }

    

    private void Navigation()
    {
        if(agent.enabled == false)
            return; 
        isMoving = true;
        if (isAggresive )
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);
            var colliders = Physics.OverlapCapsule(transform.position + Vector3.up, player.transform.position + Vector3.up, radius, mask);
            var canSee = colliders.Length == 0;

            if (distance > _distance || !canSee)
            {
                agent.SetDestination(player.transform.position);
                canShoot = false;
            }
            else
            {
                agent.SetDestination(transform.position);
                RotateToPlayer();
                canShoot = true;
            }
        }
        else
        {
            if (currentWaypoint == null)
                return;
            float distance = Vector3.Distance(transform.position, currentWaypoint.transform.position);
            if (distance < nextPointDistanceTrigger)
            {
                currentWaypoint = currentWaypoint.GetNextPoint();
                agent.destination = currentWaypoint.transform.position;
            }
        }
    }
    private void RotateToPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void SetRagDoll(bool activeState)
    {
        ragdollState = activeState;
        animator.enabled = !activeState;
        var rbs = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rbs)
        {
            rb.isKinematic = !activeState;
        }
    }

    private void OnDamageTaken(float damage)
    {
        var colliders = Physics.OverlapSphere(transform.position, alertRadius);
        foreach (var collider in colliders)
        {
            var controller = collider.GetComponentInParent<NPCController>();
            if (controller != null)
            {
                if (ragdollState == true)
                    return;
                controller.isAggresive = true;
            }
        }
    }

   
}

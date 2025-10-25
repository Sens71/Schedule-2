using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private StatsHandler statsHandler;
    private Weapon weapon;
    private float timer;
    public float _distance;
    public float viewAngle;
    private Player player;
    private NavMeshAgent agent;
    public LayerMask mask;
    public float rotationSpeed;
    private bool canShoot;

    public float radius;

    void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
        weapon.playerWeapon = false;
        statsHandler = GetComponent<StatsHandler>();
        player = FindAnyObjectByType<Player>();
        agent = GetComponent<NavMeshAgent>();
        SetRagDoll(false);
    }
    private void OnEnable()
    {
        statsHandler.OnDeath += Death;
    }
    private void OnDisable()
    {
        statsHandler.OnDeath -= Death;
    }

    void Update()
    {
        Navigation();
        timer += Time.deltaTime;
        if (timer > 1 && canShoot)
        {
            timer = 0;
            weapon.RemoteFire();
        }
    }
    public void Death()
    {
        SetRagDoll(true);
    }
    private void Navigation()
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
    private void RotateToPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void SetRagDoll(bool activeState)
    {
        var rbs = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rbs)
        {
            rb.isKinematic = !activeState;
        }
    }

}

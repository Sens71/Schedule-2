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
    void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
        weapon.playerWeapon = false;
        statsHandler = GetComponent<StatsHandler>();
        player = FindAnyObjectByType<Player>();
        agent = GetComponent<NavMeshAgent>();
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
        if (timer > 1)
        {
            timer = 0;
            weapon.RemoteFire();
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    private void Navigation()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        var canSee = !Physics.Linecast(transform.position, player.transform.position, mask);
        if (distance > _distance && !canSee)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }

    }
}

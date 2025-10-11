using UnityEngine;

public class NPCController : MonoBehaviour
{
    private StatsHandler statsHandler;
    private Weapon weapon;
    private float timer;
    void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
        weapon.playerWeapon = false;
        statsHandler = GetComponent<StatsHandler>();
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
}

using UnityEngine;
using UnityEngine.AI;

public class Enemy : Damageable
{
    private NavMeshAgent agent;

    [SerializeField] private int damage;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        type = ObjectInfo.Type.ENEMY;
        register();
    }

    private void Update()
    {
        agent.SetDestination(GameManager.instance.getPlayerPos());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Damageable>().GetDamage(damage);
    }
}
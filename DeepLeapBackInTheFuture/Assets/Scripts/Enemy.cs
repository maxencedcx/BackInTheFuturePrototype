using UnityEngine;
using UnityEngine.AI;

public class Enemy : Damageable
{
    private NavMeshAgent agent;

    [SerializeField] private int damage;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameManager.instance.objectsToBeRewind.Add(2, gameObject);
    }

    private void Update()
    {
        agent.SetDestination(GameManager.instance.getPlayerPos());
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("aaaa");
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("bbbb");
            collision.gameObject.GetComponent<Damageable>().GetDamage(damage);
        }
    }
}
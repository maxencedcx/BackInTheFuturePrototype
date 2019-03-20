using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameManager.instance.objectsToBeRewind.Add(2, gameObject);
    }

    private void Update()
    {
        agent.SetDestination(GameManager.instance.getPlayerPos());
    }
}
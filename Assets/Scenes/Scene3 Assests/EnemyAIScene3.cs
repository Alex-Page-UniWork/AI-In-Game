using UnityEngine;
using UnityEngine.AI;

public class EnemyAIScene3 : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 15f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= chaseRange)
        {
            agent.SetDestination(player.position);
        }
    }
}
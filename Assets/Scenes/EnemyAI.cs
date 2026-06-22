using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    private NavMeshAgent agent;
    private Renderer rend;

    public Transform[] patrolPoints;
    private int currentPoint;

    public float chaseDistance = 10f;
    public float searchTime = 5f;

    private Vector3 lastKnownPosition;
    private Vector3 noisePosition;

    private float searchTimer;
    private float lookTimer;

    private bool lookingAround;
    private bool investigatingNoise;

    public enum State
    {
        Patrol,
        Chase,
        Search
    }

    public State currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        currentState = State.Patrol;
    }

    void Update()
    {
        UpdateColours();

        bool canSeePlayer = CanSeePlayer();

        switch (currentState)
        {
            case State.Patrol:
                Patrol();

                if (canSeePlayer)
                {
                    ChangeState(State.Chase);
                    lastKnownPosition = player.position;
                }
                break;

            case State.Chase:
                if (canSeePlayer)
                {
                    lastKnownPosition = player.position;
                    agent.SetDestination(player.position);
                }
                else
                {
                    ChangeState(State.Search);
                    agent.SetDestination(lastKnownPosition);
                    lookingAround = false;
                }
                break;

            case State.Search:
                Search(canSeePlayer);
                break;
        }
    }

    // ---------------- STATES ----------------

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[currentPoint].position);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    void Search(bool canSeePlayer)
    {
        // if player seen again
        if (canSeePlayer)
        {
            ChangeState(State.Chase);
            return;
        }

        // noise investigation overrides search
        if (investigatingNoise)
        {
            agent.SetDestination(noisePosition);

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                investigatingNoise = false;
            }

            return;
        }

        // go to last known position + look around
        if (!lookingAround)
        {
            agent.SetDestination(lastKnownPosition);

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                lookingAround = true;
                lookTimer = 0f;
                agent.ResetPath();
            }

            return;
        }

        lookTimer += Time.deltaTime;

        if (lookTimer < 1f)
            transform.Rotate(Vector3.up * 60f * Time.deltaTime);
        else if (lookTimer < 2f)
            transform.Rotate(Vector3.up * -120f * Time.deltaTime);
        else if (lookTimer < 3f)
            transform.Rotate(Vector3.up * 60f * Time.deltaTime);

        searchTimer += Time.deltaTime;

        if (searchTimer >= searchTime)
        {
            searchTimer = 0f;
            lookingAround = false;
            ChangeState(State.Patrol);
        }
    }

    // ---------------- VISION ----------------

    bool CanSeePlayer()
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 dir = player.position - origin;

        if (dir.magnitude > chaseDistance)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(origin, dir.normalized, out hit, chaseDistance))
        {
            if (hit.transform.CompareTag("Player"))
                return true;
        }

        return false;
    }

    // ---------------- HEARING ----------------

    public void HearNoise(Vector3 position)
    {
        if (currentState == State.Chase)
            return;

        noisePosition = position;
        investigatingNoise = true;

        ChangeState(State.Search);
        agent.SetDestination(noisePosition);
    }

    // ---------------- STATE CHANGE ----------------

    void ChangeState(State newState)
    {
        currentState = newState;
    }

    // ---------------- VISUAL DEBUG ----------------

    void UpdateColours()
    {
        if (rend == null) return;

        if (currentState == State.Patrol)
            rend.material.color = Color.green;
        else if (currentState == State.Chase)
            rend.material.color = Color.red;
        else
            rend.material.color = Color.yellow;
    }

    // ---------------- DAMAGE ----------------

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
                GameManager.instance.TakeDamage(1);
        }
    }
}
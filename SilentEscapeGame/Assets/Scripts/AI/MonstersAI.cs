using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public static MonsterAI Instance { get; private set; }

    [Header("AI Settings")]
    public float patrolRadius = 10f; 
    public float chaseSpeed = 4f;  
    public float patrolSpeed = 2f;
    public float chaseDuration = 5f;
    public Transform[] patrolPoints;

    private NavMeshAgent agent;
    private Transform player;
    private bool isChasing = false;
    private float chaseTimer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Ensure the player object is correctly assigned
        player = GameObject.FindWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("MonsterAI: Player not found! Ensure Player GameObject has 'Player' tag.");
        }

        Patrol();
    }

    private void Update()
    {
        if (isChasing)
        {
            chaseTimer -= Time.deltaTime;
            if (chaseTimer <= 0)
            {
                StopChasing();
            }
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Patrol();
        }
    }

    public void AlertMonster(Vector3 soundPosition)
    {
        if (!isChasing)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(soundPosition);
            isChasing = true;
            chaseTimer = chaseDuration;
        }
    }

    public void StartChasing()
    {
        if (player != null)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
            isChasing = true;
            chaseTimer = chaseDuration;
        }
    }

    private void StopChasing()
    {
        isChasing = false;
        agent.speed = patrolSpeed;
        Patrol();
    }

    private void Patrol()
    {
        if (patrolPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, patrolPoints.Length);
            agent.SetDestination(patrolPoints[randomIndex].position);
        }
        else
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * patrolRadius;
            randomPoint.y = transform.position.y;
            agent.SetDestination(randomPoint);
        }
    }

    // Initialize Instance inside a static method to resolve UDR0002 warning
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeMonsterAI()
    {
        if (Instance == null)
        {
            Instance = Object.FindFirstObjectByType<MonsterAI>();
            if (Instance == null)
            {
                Debug.LogWarning("MonsterAI: No MonsterAI instance found in scene.");
            }
        }
    }
}

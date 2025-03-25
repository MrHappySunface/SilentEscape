/*
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public static MonsterAI Instance { get; private set; }

    [Header("AI Settings")]
    public float patrolRadius = 10f;
    public float chaseSpeed = 4f;
    public float patrolSpeed = 2f;
    public float chaseDuration = 10f; 
    public float fieldOfViewAngle = 60f; 
    public float visionDistance = 10f; 
    public Transform[] patrolPoints;
    
    private NavMeshAgent agent;
    private Transform player;
    private bool isChasing = false;
    private float chaseTimer = 0f;
    private AudioSource audioSource;
    private AudioClip footstepSound;
    public AudioClip monsterRoar;

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
        audioSource = GetComponent<AudioSource>();

        footstepSound = Resources.Load<AudioClip>("Audio/footstep_wood");
        if (footstepSound == null)
        {
            Debug.LogError("MonsterAI: Could not find footstep_wood sound in Resources/Audio folder!");
        }

        player = GameObject.FindWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("MonsterAI: Player not found! Ensure Player GameObject has 'Player' tag.");
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("MonsterAI: Not on a NavMesh! Attempting reposition...");
            ValidateNavMeshPosition();
        }

        if (agent.isOnNavMesh)
        {
            Patrol();
        }
        else
        {
            Debug.LogError("MonsterAI: Still not on NavMesh after repositioning. Check spawn point.");
        }
    }

    private void Update()
    {
        if (agent == null || !agent.isOnNavMesh)
            return;

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

        if (CanSeePlayer())
        {
            StartChasing();
        }

        if (agent.velocity.magnitude > 0.1f)
        {
            PlayFootsteps();
        }
    }

    public void AlertMonster(Vector3 soundPosition)
    {
        if (!isChasing && agent.isOnNavMesh)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(soundPosition);
            isChasing = true;
            chaseTimer = chaseDuration;
            Debug.Log("Monster is alerted by sound and moving towards: " + soundPosition);
        }
    }

    private void Patrol()
    {
        if (!agent.isOnNavMesh) return;

        if (patrolPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, patrolPoints.Length);
            agent.speed = patrolSpeed;
            agent.SetDestination(patrolPoints[randomIndex].position);
        }
        else
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * patrolRadius;
            randomPoint.y = transform.position.y;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas))
            {
                agent.speed = patrolSpeed;
                agent.SetDestination(hit.position);
            }
        }
    }

    private void StartChasing()
    {
        if (player != null && agent.isOnNavMesh)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
            isChasing = true;
            chaseTimer = chaseDuration;

            if (monsterRoar != null)
            {
                audioSource.PlayOneShot(monsterRoar);
            }
        }
    }

    private void StopChasing()
    {
        isChasing = false;
        agent.speed = patrolSpeed;
        Patrol();
    }

    private bool CanSeePlayer()
    {
        if (player == null)
            return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < fieldOfViewAngle) 
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true; 
                }
            }
        }
        return false;
    }

    private void PlayFootsteps()
    {
        if (!audioSource.isPlaying && footstepSound != null && agent.velocity.magnitude > 0.1f)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }

    private void ValidateNavMeshPosition()
    {
        if (agent.isOnNavMesh) return;

        Debug.LogWarning("MonsterAI: NavMeshAgent is not on a NavMesh! Attempting reposition...");

        NavMeshHit hit;
        float searchRadius = 50f;

        if (NavMesh.SamplePosition(transform.position, out hit, searchRadius, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
            transform.position = hit.position;
            Debug.Log($"MonsterAI repositioned to: {hit.position}");
        }
        else
        {
            Debug.LogError("MonsterAI: Could not find a valid NavMesh position nearby! Increase NavMesh coverage.");
        }
    }
}

*/

using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public static MonsterAI Instance { get; private set; }

    [Header("AI Settings")]
    public float patrolRadius = 10f;
    public float patrolSpeed = 2f;
    public Transform[] patrolPoints;

    private NavMeshAgent agent;
    private AudioSource audioSource;
    private AudioClip footstepSound;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        footstepSound = Resources.Load<AudioClip>("Audio/footstep_wood");
        if (footstepSound == null)
            Debug.LogError("MonsterAI: Missing footstep_wood in Resources/Audio!");

        ValidateNavMeshPosition();

        InvokeRepeating("Patrol", 1f, 5f);
        Patrol();
    }

    private void Update()
    {
        if (!agent || !agent.isOnNavMesh) return;

        if (agent.velocity.magnitude > 0.1f) PlayFootsteps();

        if (!agent.pathPending && agent.remainingDistance < 0.5f) Patrol();
    }

    private void Patrol()
    {
        if (!agent.isOnNavMesh) return;

        if (patrolPoints.Length > 0)
        {
            int i = Random.Range(0, patrolPoints.Length);
            agent.speed = patrolSpeed;
            agent.SetDestination(patrolPoints[i].position);
        }
        else
        {
            Vector3 point = transform.position + Random.insideUnitSphere * patrolRadius;
            point.y = transform.position.y;

            if (NavMesh.SamplePosition(point, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
            {
                agent.speed = patrolSpeed;
                agent.SetDestination(hit.position);
            }
        }
    }

    public void AlertMonster(Vector3 soundPosition)
    {
        Debug.Log("Monster heard sound at: " + soundPosition);

        if (!agent.isOnNavMesh) return;

        agent.speed = patrolSpeed;
        agent.SetDestination(soundPosition);

        if (!audioSource.isPlaying && footstepSound != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }

    private void PlayFootsteps()
    {
        if (!audioSource.isPlaying && footstepSound != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }

    private void ValidateNavMeshPosition()
    {
        if (agent.isOnNavMesh) return;

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 50f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
            transform.position = hit.position;
            Debug.Log($"MonsterAI repositioned to: {hit.position}");
        }
        else
        {
            Debug.LogError("MonsterAI: Could not find valid NavMesh position nearby!");
        }
    }
}

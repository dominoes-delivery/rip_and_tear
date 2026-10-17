// EnemyFollow Script
using UnityEngine;
using UnityEngine.AI;

// script to make the enemy follow the player when they detect them
[RequireComponent(typeof(NavMeshAgent))]
public class enemyfollowyoubrah : MonoBehaviour
{
    [SerializeField] private AudioSource aggroSound;
    [SerializeField] private float aggroRadius;
    
    private Transform playerTransform;
    private NavMeshAgent enemyNavMeshAgent;

    private bool _aggro = false;
    public bool isAggro
    {
        get { return _aggro; }
        set
        {
            if (value && !_aggro) OnAggro();
            _aggro = value;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!isAggro && (playerTransform.position - transform.position).magnitude < aggroRadius)
        {
            isAggro = true;
        }
        if (isAggro)
        {
            enemyNavMeshAgent.isStopped = false;
            enemyNavMeshAgent.SetDestination(playerTransform.position);
        }
        else
        {
            enemyNavMeshAgent.isStopped = true;
        }
    }

    public void OnAggro()
    {
        aggroSound.Stop();
        aggroSound.Play();
    }
}

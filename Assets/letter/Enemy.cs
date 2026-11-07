using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{

    
    private NavMeshAgent agent;
        [SerializeField] private float stillThreshold = 0.1f;
        [SerializeField] private float maxStunTime = 1f;
        [SerializeField] private float minStunTime = 0.1f;


    

    [SerializeField] private float health = 2f;
    [SerializeField] private float gravity = 0f;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GameObject gunHitEffect;

    private Rigidbody body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyManager = Object.FindAnyObjectByType<EnemyManager>();
        if (!enemyManager)
        {
            Debug.LogWarning("No EnemyManager in this scene!");
        }
        body = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        body.AddForce(gravity * Vector3.down, ForceMode.Force);
        if (health <= 0)
        {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        GameObject hitEffect = Instantiate(gunHitEffect, transform.position, Quaternion.identity);
    }

    public void Knockback(Vector3 force)
    {
        StartCoroutine(ApplyKnockback(force));
    }

    public IEnumerator ApplyKnockback(Vector3 force)
    {
        yield return null;
        agent.enabled = false;
        body.useGravity = true;
        body.isKinematic = false;
        body.AddForce(force, ForceMode.Impulse);
        yield return new WaitForFixedUpdate();
        float start_time = Time.time;
        yield return new WaitUntil(() => body.angularVelocity.magnitude < stillThreshold || (Time.time - start_time) > maxStunTime);
        yield return new WaitForSeconds(minStunTime);
        Debug.Log("Knocked back " + this.name + " for: " + (Time.time - start_time).ToString("F3") + "seconds");
        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        body.useGravity = false;
        body.isKinematic = true;
        if (NavMesh.SamplePosition(body.position, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
            agent.enabled = true;
        }
        else
        {
            Debug.LogFormat("Enemy {0} was knocked off navmesh, killing it...", this.name);
            this.health = 0;
        }


        yield return null;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (enemy.TryGetComponent(out Rigidbody other))
            {
                if (other.isKinematic && collision.impulse.magnitude > stillThreshold)
                {
                    StartCoroutine(enemy.ApplyKnockback(-collision.impulse));
                }
            }
        }
    }
}
//"5 little pigeons sitting in a tree, the rest got shot by KFC uisyfoiuyfuoiyaoiuyfgiasufygouid"🗣️🗣️🗣️🗣️🗣️🔥🔥🔥🔥🔥🔥🔥
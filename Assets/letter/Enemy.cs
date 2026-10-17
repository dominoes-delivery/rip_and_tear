using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{

    [SerializeField] private float health = 2f;
    [SerializeField] private float gravity = 0f;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GameObject gunHitEffect;

    private Rigidbody body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyManager = Object.FindAnyObjectByType<EnemyManager>();
        if (!enemyManager) {
            Debug.LogWarning("No EnemyManager in this scene!");
        }
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        body.AddForce(gravity * Vector3.down, ForceMode.Force);
        if (health <= 0) {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount) {
        health -= amount;
        GameObject hitEffect = Instantiate(gunHitEffect, transform.position, Quaternion.identity);
    }

    public void Knockback(Vector3 force) {
        body.AddForce(force, ForceMode.Impulse);
    }
}

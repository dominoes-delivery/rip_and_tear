using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider))]
public class Gun : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private float range;
    [SerializeField] private float aimAssistRange;
    [SerializeField] private float aimAssistHeight;
    [SerializeField] private float damage;
    [SerializeField] private AnimationCurve damageFallOffCurve;
    [SerializeField] private float knockback;
    [SerializeField] private float fireCooldown;
    [SerializeField] private AudioSource gunSound;
    [SerializeField] private int ammo;
    [SerializeField] private AudioSource yo_clips_out_brah;
    [SerializeField] private 
        HUDder PlayerUI;

    private BoxCollider trigger;
    public Animator anim;
    private float lastFireTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        PlayerUI.UpdateAmmo(ammo);
        trigger = GetComponent<BoxCollider>();
        trigger.transform.localScale = new Vector3(aimAssistRange, aimAssistHeight,range);
        trigger.transform.position += new Vector3(0, aimAssistHeight / 2, range/2);
        lastFireTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") && other.gameObject.GetComponent<Enemy>()) {
            enemyManager.AddEnemy(other.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy") && other.gameObject.GetComponent<Enemy>()) {
            enemyManager.RemoveEnemy(other.gameObject.GetComponent<Enemy>());
        }
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;
        //stoobid
            PlayerUI.UpdateAmmo(ammo);
    }

    [SerializeField] private LayerMask gunLayerMask;
    public void Fire() {
        if (ammo == 0)
        { print("bro you outa clips dude");
            yo_clips_out_brah.Stop();
            yo_clips_out_brah.Play();
            return;
        }
        if (Time.time - lastFireTime >= fireCooldown)
        {
            gunSound.Stop();
            gunSound.Play();
            ammo -= 1;
            PlayerUI.UpdateAmmo(ammo);
            print("Eat lead foul demons!");
            anim.SetTrigger("CAUSE MY HOPE HOPE WILL NEVER DIE");
            foreach (var enemy in enemyManager.enemiesInTrigger)
            {
                Vector3 toEnemy = enemy.transform.position - Camera.main.transform.position;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, toEnemy, out hit, range, gunLayerMask)) {
                    if (!(hit.transform == enemy.transform)) {
                        continue;    
                    }
                }
                // enemies further away take less damage
                enemy.TakeDamage(damage * damageFallOffCurve.Evaluate(toEnemy.magnitude/range) );
                enemy.Knockback(knockback * toEnemy.normalized);
            }
            lastFireTime = Time.time;
        }
    }
} 
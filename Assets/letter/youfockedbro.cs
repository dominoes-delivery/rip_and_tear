using UnityEngine;
using System.Collections.Generic;

public class youFockedbro : MonoBehaviour
{

    [SerializeField] private int damage = 0;
    [SerializeField] private float cooldown;
    [SerializeField] private bool damageEnemies = false;
    private float lastTriggerTime = 0;
    public List<GameObject> toDamage = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastTriggerTime = -cooldown;
    }

    private void Update()
    {
        if (toDamage.Count > 0 && (Time.time - lastTriggerTime) > cooldown)
        {
            bool tookDamage = false;
            foreach (GameObject target in toDamage)
            {
                if (target == null)
                {
                    continue;
                }
                else if (target.GetComponent<poopypoopoo>())
                {
                    target.GetComponent<poopypoopoo>().DamagePlayer(damage);
                    tookDamage = true;
                }
                else if (target.GetComponent<Enemy>())
                {
                    target.GetComponent<Enemy>().TakeDamage(damage);
                    tookDamage = true;
                }
            }
            toDamage.RemoveAll(item => item == null);
            if (tookDamage)
            {
                lastTriggerTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            toDamage.Add(other.gameObject);
            Debug.Log(this.name + " hazard damaging player - " + other.gameObject.name);
        }

        if (damageEnemies && other.gameObject.CompareTag("enemy"))
        {
            toDamage.Add(other.gameObject);
            Debug.Log(this.name + " hazard damaging enemy -  " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        toDamage.Remove(other.gameObject);
    }

}



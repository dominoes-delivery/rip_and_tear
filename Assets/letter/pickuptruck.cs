using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int armor;
    [SerializeField] private int ammo;
    [SerializeField] private string key;
    [SerializeField][Tooltip("Whether or not the item pickup respawns after a while")] private bool respawn;
    [SerializeField] private int respawnTimer;
    [SerializeField] private AudioSource pickupSound;
    private float lastUsed = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastUsed = -respawnTimer; // prevents pickups being on cooldown at start

    }

    // Update is called once per frame
    void Update()
    {

    }

    // disables all children and then reenables them for a time
    IEnumerator HideChildrenForATime(GameObject toHide, float time)
    {
        List<Transform> childrenToReenable = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
                childrenToReenable.Add(child);
            }
        }
        yield return new WaitForSeconds(time);
        foreach (Transform child in childrenToReenable)
        {
            child.gameObject.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!respawn || Time.time - lastUsed > respawnTimer)
        {
            if (other.CompareTag("Player"))
            {
                bool used = false;
                poopypoopoo playerHealth = other.GetComponent<poopypoopoo>();
                if (!playerHealth)
                {
                    Debug.LogError("Pickup: " + gameObject.name + " did not find valid PlayerHealth component on player!");
                    return;
                }

                if (health > 0 && !playerHealth.IsFullHealth)
                {
                    Debug.Log("Pickup: " + gameObject.name + " healing player by " + health);
                    playerHealth.HealPlayer(health);
                    used = true;
                }

                if (armor > 0 && !playerHealth.IsFullArmor)
                {
                    Debug.Log("Pickup: " + gameObject.name + " armoring player by " + armor);
                    playerHealth.ArmorPlayer(armor);
                    used = true;
                }

                // TODO ammo system
                if (ammo > 0)
                {
                    Gun playerGun = other.GetComponentInChildren<Gun>();
                    if (playerGun)
                    {
                        Debug.Log("Pickup: " + gameObject.name + " refilling gun ammo " + ammo);
                        playerGun.AddAmmo(ammo);
                        used = true;
                    }
                    else
                    {
                        Debug.LogWarning("Pickup: " + gameObject.name + " could not find gun to refill ammo " + ammo);
                    }
                }

                if (key != null && (key.Length > 0))
                {
                    //PlayerInventory inventory = other.GetComponent<PlayerInventory>();
                    //if (!inventory)
                    //{
                    //    Debug.LogWarning("Player is missing inventory component!");
                    //}
                    //else
                    //{
                    //    inventory.AddKey(key);
                    //    used = true;
                   // }
                }

                // remove self if used
                if (used)
                {
                    if (pickupSound)
                    {
                        pickupSound.Stop();
                        AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);
                    }
                    if (respawn)
                    {
                        StartCoroutine(HideChildrenForATime(gameObject, respawnTimer));
                        lastUsed = Time.time;
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}

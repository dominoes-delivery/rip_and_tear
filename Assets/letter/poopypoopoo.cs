
using UnityEngine;
using UnityEngine.SceneManagement;

public class poopypoopoo : MonoBehaviour
{

    private int health;



    [SerializeField] private HUDder PlayerUI;
    [SerializeField] private int maxHealth;

    public bool IsFullHealth { get { return health >= maxHealth; } }

    [SerializeField] private int maxArmor;
    private int armor;
    public bool IsFullArmor { get { return armor >= maxArmor; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     health = maxHealth;
        armor = 0;
        PlayerUI.SetMaxHealth(maxHealth);
        PlayerUI.UpdateHealth(health);
        PlayerUI.UpdateArmor(armor);
        
    }

    public int getHealth()
    {
        return health;
    }

    public void HealPlayer(int heal)
    {
        Debug.Log(gameObject.name + " healed by " + heal);
        health += heal;
        if (health > maxHealth) health = maxHealth;
        PlayerUI.UpdateHealth(health);
        PlayerUI.UpdateArmor(armor);
    }
    
    public int getArmor()
    {
        return armor;
    }

    public void ArmorPlayer(int armor_amount)
    {
        Debug.Log(gameObject.name + " armored by " + armor_amount);
        armor += armor_amount;
        if (armor > maxArmor) armor = maxArmor;
        PlayerUI.UpdateHealth(health);
        PlayerUI.UpdateArmor(armor);
    }


    public void DamagePlayer(int damage)
    {
        // if the player has armor, it takes damage first
        if (armor >= damage)
        {
            armor -= damage;
        }
        else
        {
            damage -= armor;
            armor = 0;
            health -= damage;
        }

        PlayerUI.UpdateHealth(health);
        PlayerUI.UpdateArmor(armor);
        if (health <= 0)
        {
            // player is dead
            Debug.Log("Player Died");
            // reload the scene
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }

    
    public void OnJump() {
        DamagePlayer(15);
    }
    
}

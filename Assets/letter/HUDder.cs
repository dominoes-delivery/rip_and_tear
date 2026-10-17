using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class HUDder : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI armor;
    [SerializeField] private TextMeshProUGUI ammo;

    [SerializeField] Image healthIndicator;

    [SerializeField] List<Sprite> healthStates = new List<Sprite>();

    private int currentHealthState = 0;
    private float maxHealth = 101f;
    public void Start()
    {
        if (healthStates.Count <= 0)
        {
            Debug.LogWarning("Missing health indicator faces!");
        }
    }
    

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void UpdateHealth(int healthValue)
    {
        health.text = healthValue + " ";
        // different health states
        int newHealthState;
        if (healthValue >= 100)
        {
            newHealthState = 0;
        }
        else
        {
            newHealthState = Mathf.Clamp(Mathf.FloorToInt((maxHealth - Mathf.Abs(healthValue)) / 100f * healthStates.Count), 0, healthStates.Count - 1);
            Debug.Log("Current Health State: " + newHealthState.ToString());
        }

        if (newHealthState != currentHealthState)
        {
            currentHealthState = newHealthState;
            Debug.Log("New health State: " + newHealthState.ToString());
            healthIndicator.sprite = healthStates[newHealthState];
        }

    }

    public void UpdateArmor(int armorValue)
    {
        armor.text = armorValue + " ";

    }

    public void UpdateAmmo(int ammoValue)
    {
        ammo.text = ammoValue.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour
{
    [SerializeField] private AttributeConfig config;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject aura;

    private int health;
    private int damage;
    private int increaseDamage;

    public int Health => health;
    public int Damage => damage + increaseDamage;


    public void Init()
    {
        aura.SetActive(false);
        health = config.BaseHealth;
        damage = config.BaseDamage;
        increaseDamage = 0;
        healthBar.SetMaxHealth(config.MaxHealth);
        healthBar.SetHealth(health);
    }

    public void IncreaseHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, config.MaxHealth);
        healthBar.SetHealth(health);
    }

    public void DecreaseHealth(int amount)
    {
        health = Mathf.Clamp(health - damage, 0, config.MaxHealth);
        healthBar.SetHealth(health);
    }

    public void IncreaseDamage(int amount)
    {
        increaseDamage = amount;
        aura.SetActive(true);
    }

    public void DecreaseDamage()
    {
        increaseDamage = 0;
        aura.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour
{
    [SerializeField] private AttributeConfig config;

    private int health;
    private int damage;
    private int increaseDamage;

    public int Health => health;
    public int Damage => damage + increaseDamage;


    public void Init()
    {
        health = config.BaseHealth;
        damage = config.BaseDamage;
        increaseDamage = 0;
    }

    public void IncreaseHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, config.MaxHealth);
    }

    public void DecreaseHealth(int amount)
    {
        health = Mathf.Clamp(health - damage, 0, config.MaxHealth);
    }

    public void IncreaseDamage(int amount)
    {
        increaseDamage = amount;
    }

    public void DecreaseDamage()
    {
        increaseDamage = 0;
    }
}

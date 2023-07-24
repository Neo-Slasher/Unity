using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngamePlayer : MonoBehaviour
{
    public HealthBar healthBar;

    public int maxHealth = 100;
    public int currentHealth;

    void Start() {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}

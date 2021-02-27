using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;

    private float currentHealth;

    private void Start()
    {
        this.currentHealth = this.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        this.currentHealth -= damage;

        if (this.currentHealth <= 0)
        {
            this.Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}

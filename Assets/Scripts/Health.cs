using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;

    private float currentHealth;
    private bool canGetDamage;

    public void Start()
    {
        canGetDamage = true;
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float damageTaken()
    {
        return maxHealth - currentHealth;
    }

    public void TakeDamage(DamageState damage)
    {
        currentHealth -= TranslateDamageToValue(damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //dead = true;
        Destroy(this.gameObject);
    }

    public static float TranslateDamageToValue(DamageState damage)
    {
        switch (damage)
        {
            case DamageState.HALF:
                return 0.5f;
            case DamageState.FULL:
                return 1f;
            default:
                return 0;
        }
    }

    public static DamageState TranslateValueToDamage(float damage)
    {
        switch (damage)
        {
            case 0.5f:
                return DamageState.HALF;
            case 1f:
                return DamageState.FULL;
            default:
                return DamageState.NONE;
        }
    }
}

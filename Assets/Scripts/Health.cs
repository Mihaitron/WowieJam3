using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int soundIndex;
    public AudioSource deathSound;

    private float currentHealth;
    private bool canGetDamage;

    public void Start()
    {
        canGetDamage = true;
        currentHealth = maxHealth;
        if (soundIndex == 0)
        {
            deathSound = GameObject.Find("RatDeath").GetComponent<AudioSource>();
        }
        else if (soundIndex == 1)
        {
            deathSound = GameObject.Find("HumanDeath").GetComponent<AudioSource>();
        }
        else if (soundIndex == 2)
        {
            deathSound = GameObject.Find("ZombieDeath").GetComponent<AudioSource>();
        }
        else if (soundIndex == 3)
        {
            deathSound = GameObject.Find("SkeletDeath").GetComponent<AudioSource>();
        }
        else if (soundIndex == 4)
        {
            deathSound = GameObject.Find("BossDeath").GetComponent<AudioSource>();
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float damageTaken()
    {
        return maxHealth - currentHealth;
    }

    public void AddHealth()
    {
        currentHealth += 1;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void TakeDamage(DamageState damage)
    {
        currentHealth -= TranslateDamageToValue(damage);
        this.gameObject.GetComponent<GetHurt>().Hurt();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        deathSound.Play();
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

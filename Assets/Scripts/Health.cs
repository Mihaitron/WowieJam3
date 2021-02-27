using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;

    public void TakeDamage(DamageState damage)
    {
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public float TranslateDamageToValue(DamageState damage)
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

    public DamageState TranslateValueToDamage(float damage)
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

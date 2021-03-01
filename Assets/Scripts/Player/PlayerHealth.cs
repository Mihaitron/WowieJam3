using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class PlayerHealth : MonoBehaviour
{
    public List<Sprite> heartStates;
    public Transform healthBar;

    private Health health;
    //private float dmgTime = 5f;

    private void Start()
    {
        this.health = this.GetComponent<Health>();
    }

    private void Update()
    {
        this.ModifyHearts();
        this.ModifyHealthUI();
    }

    public void Heal()
    {
        health.AddHealth();
    }

    private void ModifyHearts()
    {
        float dmg = health.damageTaken();
        int current_index = this.healthBar.childCount - 1;

        while (current_index > 0)
        {
            Transform current = this.healthBar.GetChild(current_index);
            Heart current_heart = current.GetComponent<Heart>();

            while (dmg > 0.5f)
            {
                current_heart.state = DamageState.NONE;
                dmg -= 1f;
                current_index--;

                current = this.healthBar.GetChild(current_index);
                current_heart = current.GetComponent<Heart>();
            }


            if (dmg == 0.5f)
            {
                current_heart.state = DamageState.HALF;
                current_index--;
                dmg -= 0.5f;

            }
            else
            {
                current_heart.state = DamageState.FULL;
                current_index--;
            }
        }

    }

    public void ResetUI()
    {
        int current_index = this.healthBar.childCount - 1;
        Transform current = this.healthBar.GetChild(current_index);
        current.GetComponent<Image>().sprite = this.heartStates[0];

        while (current_index > 0)
        {
            current_index--;
            current = this.healthBar.GetChild(current_index);

            current.GetComponent<Image>().sprite = this.heartStates[2];
        }
    }

    private void ModifyHealthUI()
    {
        int current_index = this.healthBar.childCount - 1;
        Transform current = this.healthBar.GetChild(current_index);
        Heart current_heart = current.GetComponent<Heart>();

        while (current_heart.state == DamageState.NONE && current_index > 0)
        {
            Image current_image = current.GetComponent<Image>();
            current_image.sprite = this.heartStates[0];

            current_index--;
            current = this.healthBar.GetChild(current_index);
            current_heart = current.GetComponent<Heart>();
        }

        if (current_heart.state == DamageState.HALF)
        {
            Image current_image = current.GetComponent<Image>();
            current_image.sprite = this.heartStates[1];
        }
        else if (current_heart.state == DamageState.FULL)
        {
            Image current_image = current.GetComponent<Image>();
            current_image.sprite = this.heartStates[2];
        }
    }
}

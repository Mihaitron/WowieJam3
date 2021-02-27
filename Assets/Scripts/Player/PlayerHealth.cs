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

    private void Start()
    {
        this.health = this.GetComponent<Health>();
    }

    private void Update()
    {
        this.ModifyHealthUI();
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

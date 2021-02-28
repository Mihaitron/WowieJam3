using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMove))]
public class PlayerController : MonoBehaviour
{
    public GameObject weapon;
    public GameObject shield;
    public float attackTime = 1f;

    private bool isMoving = false;
    private bool isLooking = false;
    private Vector2 movingDirection = Vector2.zero;
    private Vector2 mousePosition = Vector2.zero;
    private PlayerMove playerMovement;
    private bool isBlocking;
    private List<GameObject> enemies;
    private bool nearChest;
    private Transform chest;
    private GameObject spawnedShield = null;
    private float currentAttackTime;

    private void Start()
    {
        nearChest = false;
        enemies = new List<GameObject>();
        isBlocking = false;
        this.playerMovement = this.GetComponent<PlayerMove>();
        this.currentAttackTime = this.attackTime;
    }

    private void FixedUpdate()
    {
        this.ApplyMovement();
        this.currentAttackTime -= Time.deltaTime;
    }

    public bool IsBlocking()
    {
        return isBlocking;
    }

    public void OnBlock()
    {
        isBlocking = !isBlocking;

        if (this.isBlocking)
        {
            this.spawnedShield = Instantiate(this.shield, this.transform.GetChild(0), false);
        }
        else
        {
            Destroy(this.spawnedShield);
            this.spawnedShield = null;
        }
    }

    public void OnAttack()
    {
        if (!this.isBlocking && this.currentAttackTime <= 0)
        {
            this.currentAttackTime = this.attackTime;
            GameObject spawned_sword = Instantiate(this.weapon, this.transform.GetChild(0), false);
            List<GameObject> enemiesToRemove = new List<GameObject>();

            spawned_sword.GetComponent<Animator>().speed = 5;

            foreach (GameObject enemy in enemies)
            {


                Health enemyHealth = enemy.GetComponent<Health>();
                float enemyHealthCurrent = enemyHealth.maxHealth;
                enemyHealth.TakeDamage(DamageState.FULL);

                if (enemyHealthCurrent == 1)
                {
                    enemiesToRemove.Add(enemy);
                }
            }

            foreach (GameObject enemy in enemiesToRemove)
            {
                enemies.Remove(enemy);
            }
        }
    }

    public void OnInteract()
    {
        if (this.nearChest)
        {
            chest.GetComponent<Chest>().Interact();
        }
    }

    public void OnPause()
    {
        GameObject.Find("Canvas").GetComponent<PauseMenu>().Action();
    }

    public void OnMove(InputValue input)
    {
        Vector2 input_vector = input.Get<Vector2>();

        if (input_vector != Vector2.zero)
        {
            this.isMoving = true;
        }
        else
        {
            this.isMoving = false;
        }

        this.movingDirection = input_vector;
    }

    public void OnLook(InputValue input)
    {
        Vector2 input_vector = input.Get<Vector2>();

        if (input_vector != Vector2.zero)
        {
            this.isLooking = true;
        }
        else
        {
            this.isLooking = false;
        }
        this.mousePosition = input_vector;
    }

    public void ApplyMovement()
    {
        if (this.isMoving)
        {
            this.playerMovement.HandleMovement(this.movingDirection);
        }
        else
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (this.isLooking)
        {
            this.playerMovement.HandleLooking(this.mousePosition);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
        else if (other.CompareTag("Chest"))
        {
            nearChest = true;
            chest = other.transform;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
            nearChest = false;
            chest = null;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Heart"))
        { 
            this.GetComponent<PlayerHealth>().Heal();
            Destroy(collision.gameObject);
        }
    }
}

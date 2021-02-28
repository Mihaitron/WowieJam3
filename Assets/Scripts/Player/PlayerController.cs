using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMove))]
public class PlayerController : MonoBehaviour
{
    private bool isMoving = false;
    private bool isLooking = false;
    private Vector2 movingDirection = Vector2.zero;
    private Vector2 mousePosition = Vector2.zero;
    private PlayerMove playerMovement;
    private bool isBlocking;
    private List<GameObject> enemies;

    private void Start()
    {
        enemies = new List<GameObject>();
        isBlocking = false;
        this.playerMovement = this.GetComponent<PlayerMove>();
    }

    private void FixedUpdate()
    {
        this.ApplyMovement();
    }

    public bool IsBlocking()
    {
        return isBlocking;
    }

    public void OnBlock()
    {
        isBlocking = !isBlocking;
    }

    public void OnAttack()
    {
        List<GameObject> enemiesToRemove = new List<GameObject>();
        //Debug.Log(enemies.Count);

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
            Debug.Log(enemy.name);
            enemies.Remove(enemy);
        }
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
            Debug.Log(other.gameObject.name);
            enemies.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }
}

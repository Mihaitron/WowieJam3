using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIType
{
    NORMAL,
    SUMMONER,
    BOSS
}


public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public AIType type;
    public Transform player;
    //public float delta = 0.1f;
    //public float speed = 1;
    public DamageState dmg;

    private bool canSeePlayer;
    private float damageTime;
    private bool damageble;

    private void Start()
    {
        damageTime = 0f;
        canSeePlayer = false;
    }

    private void Update()
    {
        if (type == AIType.NORMAL)
        {
            if (canSeePlayer)
            {
                agent.SetDestination(player.position);
            }

            if (damageTime <= 0 && damageble)
            {
                player.gameObject.GetComponent<Health>().TakeDamage(dmg);
                damageTime = 2f;

            }

            if (damageTime > 0)
                damageTime -= Time.deltaTime;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            canSeePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //player = other.transform;
            canSeePlayer = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            damageble = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            damageble = false;
        }
    }
}

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
    public float distance;
    public float waitDamageTime;

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
            if (Vector3.Distance(player.position, this.transform.position) <= distance)
            {
                canSeePlayer = true;
            }
            else
            {
                canSeePlayer = false;
            }

            if (canSeePlayer)
            {
                agent.SetDestination(player.position);
            }

            if (damageTime <= 0 && damageble)
            {
                if (!player.GetComponent<PlayerController>().IsBlocking())
                    player.gameObject.GetComponent<Health>().TakeDamage(dmg);
                
                damageTime = waitDamageTime;

            }

            if (damageTime > 0)
                damageTime -= Time.deltaTime;

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

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
    public float delta = 0.1f;
    public float speed = 1;
    public bool canSeePlayer;

    private void Start()
    {
        canSeePlayer = false;
    }

    private void Update()
    {
        //this.transform.position = Vector3.MoveTowards(this.transform.position, player.position, this.delta) * this.speed;
        if (canSeePlayer)
        {
            Debug.Log("CPLM?!");
            agent.SetDestination(player.position);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        }
    }
}

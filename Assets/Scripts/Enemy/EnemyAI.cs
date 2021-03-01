using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIType
{
    NORMAL,
    SUMMONER,
    BOSS,
    BOSS_SUMMONER
}


public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public AIType type;
    
    //public float delta = 0.1f;
    //public float speed = 1;
    public DamageState dmg;
    public float distance;
    public float waitDamageTime;
    public GameObject summon;
    public Transform summonLocation;
    public Transform player;

    private bool canSeePlayer;
    private float damageTime;
    private bool damageble;
    private Vector3 destinationPos;
    private bool bossSecondAttack;
    private bool summonOnlyOnce;
    private AudioSource bossPunch;

    private void Start()
    {
        summonOnlyOnce = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        damageTime = 0f;
        canSeePlayer = false;
        if (type == AIType.BOSS)
        {
            bossPunch = GameObject.Find("BossPunch").GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, this.transform.position) <= distance)
        {
            canSeePlayer = true;
        }
        else
        {
            canSeePlayer = false;
        }

        if (type == AIType.NORMAL)
        {
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
        }
        else if (type == AIType.SUMMONER)
        {
            if (canSeePlayer)
            {
                
                agent.SetDestination(destinationPos);
                if (damageTime <= 0)
                {
                    AISummon();
                    damageTime = waitDamageTime;
                }
            }
            else
                destinationPos = new Vector3(this.transform.position.x + (player.position.x - this.transform.position.x) / 2, this.transform.position.y + (player.position.y - this.transform.position.y) / 2, this.transform.position.z + (player.position.z - this.transform.position.z) / 2);
        }
        else if (type == AIType.BOSS_SUMMONER)
        {
            if (canSeePlayer)
            {
                agent.SetDestination(player.position);
                if (!summonOnlyOnce)
                { 
                    AISummon();
                    summonOnlyOnce = true;
                }
            }
            agent.speed = 0;
        }
        else if (type == AIType.BOSS)
        {
            if (canSeePlayer)
            {
                agent.SetDestination(player.position);
            }

            if (damageTime <= 0 && damageble)
            {
                if (!player.GetComponent<PlayerController>().IsBlocking())
                    player.gameObject.GetComponent<Health>().TakeDamage(dmg);

                bossPunch.Play();

                if (!bossSecondAttack)
                {
                    bossSecondAttack = true;
                    damageTime = 0.5f;
                }
                else
                {
                    bossSecondAttack = false;
                    damageTime = waitDamageTime;
                }

            }
        }

        if (damageTime > 0)
            damageTime -= Time.deltaTime;
    }

    private void AISummon()
    {
        Instantiate(summon, summonLocation.position, Quaternion.identity);
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

    public void SetDamageable(bool is_damageable)
    {
        this.damageble = is_damageable;
    }
}

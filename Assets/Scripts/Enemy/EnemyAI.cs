using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIType
{
    NORMAL,
    SUMMONER,
    BOSS
}

public class EnemyAI : MonoBehaviour
{
    public AIType type;
    public Transform player;
    public float delta = 0.1f;
    public float speed = 1;

    private void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.position, this.delta) * this.speed;
    }
}

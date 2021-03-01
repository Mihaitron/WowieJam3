using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public List<Transform> respawnPoints;
    public GameObject player;

    private int spawnIndex;

    private void Start()
    {
        this.spawnIndex = 0;
    }

    public void RespawnAtLastSpawnpoint()
    {
        GameObject new_player = Instantiate(this.player, this.respawnPoints[this.spawnIndex].position, Quaternion.identity);

        new_player.GetComponent<PlayerController>().deathMenu = GameObject.Find("DeathMenu");
        new_player.GetComponent<PlayerHealth>().healthBar = GameObject.Find("Health Bar").transform;
        new_player.GetComponent<Health>().ResetMaxHealth();

        GameObject.Find("Render Texture Camera").GetComponent<CameraFollowTarget>().target = new_player.transform;
        GameObject.Find("DeathMenu").SetActive(false);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyAI ai = enemy.GetComponent<EnemyAI>();

            ai.player = new_player.transform;
            ai.SetDamageable(false);
        }
    }

    public void NextRespawnPoint()
    {
        this.spawnIndex++;

        if (this.spawnIndex >= this.respawnPoints.Count)
        {
            this.spawnIndex = 0;
        }
    }
}

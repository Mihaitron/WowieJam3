using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public List<Transform> respawnPoints;
    public GameObject player;

    private static int spawnIndex;

    private void Start()
    {
        spawnIndex = 0;
    }

    public void RespawnAtLastSpawnpoint()
    {
        GameObject new_player = Instantiate(this.player, this.respawnPoints[spawnIndex].position, Quaternion.identity);
        Debug.Log(spawnIndex);

        new_player.GetComponent<PlayerController>().deathMenu = GameObject.Find("DeathMenu");
        new_player.GetComponent<PlayerHealth>().healthBar = GameObject.Find("Health Bar").transform;
        new_player.GetComponent<Health>().ResetMaxHealth();
        new_player.GetComponent<PlayerHealth>().ResetUI();

        GameObject.Find("Render Texture Camera").GetComponent<CameraFollowTarget>().target = new_player.transform;
        GameObject.Find("DeathMenu").SetActive(false);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyAI ai = enemy.GetComponent<EnemyAI>();

            ai.player = new_player.transform;
            ai.SetDamageable(false);
        }

        GameObject.Find("Level 2 Door").GetComponent<NextLevelDoor>().player = new_player;
        GameObject.Find("Level 3 Door").GetComponent<NextLevelDoor>().player = new_player;
    }

    public void NextRespawnPoint()
    {
        spawnIndex++;

        if (spawnIndex >= this.respawnPoints.Count)
        {
            spawnIndex = 0;
        }
    }
}

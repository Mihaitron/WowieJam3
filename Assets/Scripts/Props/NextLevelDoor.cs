using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{
    public GameObject player;
    public Transform nextLevelStart;
    public GameObject retryButton;

    private void GoToNextLevel()
    {
        this.player.transform.position = this.nextLevelStart.position;

        this.retryButton.GetComponent<Respawn>().NextRespawnPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GoToNextLevel();
        }
    }
}

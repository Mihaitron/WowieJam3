using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Transform item;
    public Transform spawnPosition;
    public GameObject closedChest;

    private void Start()
    {
    }

    public void Interact()
    {
        Instantiate(item, spawnPosition.position, Quaternion.identity);
        Instantiate(this.closedChest, this.transform.position, this.transform.rotation);

        Destroy(this.gameObject);
    }
}

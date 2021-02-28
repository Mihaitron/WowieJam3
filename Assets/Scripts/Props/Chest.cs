using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Transform item;
    public Transform spawnPosition;

    private bool interacted;

    private void Start()
    {
        interacted = false;
    }


    public void Interact()
    {
        if (!interacted)
        { 
            Instantiate(item, spawnPosition.position, Quaternion.identity);
            interacted = true;
        }
    }
}

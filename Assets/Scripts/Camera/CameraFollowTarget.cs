using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        this.FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 new_position = new Vector3(this.target.position.x, this.transform.position.y, this.target.position.z);
        
        this.transform.position = new_position;
    }
}

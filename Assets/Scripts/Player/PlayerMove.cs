using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public float speed = 1f;

    private Rigidbody rigidbody;

    private void Start()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();
    }

    public void HandleMovement(Vector2 direction)
    {
        Vector3 new_direction = new Vector3(-direction.x, 0f, -direction.y);

        this.rigidbody.AddForce(new_direction * this.speed * Time.deltaTime);
    }

    public void HandleLooking(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(0, -angle - 90, 0);
    }
}

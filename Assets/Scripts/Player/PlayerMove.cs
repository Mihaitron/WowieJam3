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

        this.rigidbody.velocity = new_direction * this.speed * Time.deltaTime;
    }

    public void HandleLooking(Vector2 mouse_position)
    {
        // Mouse position, independent of screen resolution, in the interval [-0.5, 0.5]
        Vector3 mouse_game = new Vector3(mouse_position.x / Screen.width - 0.5f, mouse_position.y / Screen.height - 0.5f, 0.0f);
        float angle = Mathf.Atan2(mouse_game.x, mouse_game.y) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(new Vector3(0f, angle  +180, 0f));
    }
}

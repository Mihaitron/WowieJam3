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

    public void HandleLooking(Vector2 mouse_position)
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(this.transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(mouse_position);
        float angle = this.AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle + 90, 0f));
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMove))]
public class PlayerController : MonoBehaviour
{
    public Camera camera;

    private bool isMoving = false;
    private bool isLooking = false;
    private Vector2 movingDirection = Vector2.zero;
    private Vector2 mousePosition = Vector2.zero;
    private PlayerMove playerMovement;


    private void Start()
    {
        this.playerMovement = this.GetComponent<PlayerMove>();
    }

    private void FixedUpdate()
    {
        this.ApplyMovement();
    }

    public void OnMove(InputValue input)
    {
        Vector2 input_vector = input.Get<Vector2>();

        if (input_vector != Vector2.zero)
        {
            this.isMoving = true;
        }
        else
        {
            this.isMoving = false;
        }

        this.movingDirection = input_vector;
    }

    public void OnLook(InputValue input)
    {
        Vector2 input_vector = input.Get<Vector2>();

        if (input_vector != Vector2.zero)
        {
            this.isLooking = true;
        }
        else
        {
            this.isLooking = false;
        }
        this.mousePosition = input_vector;
    }

    public void ApplyMovement()
    {
        if (this.isMoving)
        {
            this.playerMovement.HandleMovement(this.movingDirection);
        }
        if (this.isLooking)
        {
            this.playerMovement.HandleLooking(this.mousePosition);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalNewMovement : MonoBehaviour
{
    private PlayerInputSystem input;

    public float speed = 4;
    public float jumpForce = 7;

    public Transform groundCheck;
    public float groundRadius;
    public LayerMask layerGrounds;

    private bool isGrounded;
    private float movementX;

    private new Rigidbody2D rigidbody;
    private GameObject player;

    public static float Axis;

    private void Awake()
    {
        input = new PlayerInputSystem();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        player = gameObject.gameObject;

        input.Player.Move.performed += context => Move(context.ReadValue<float>());
        input.Player.Move.canceled += context => Move(0);
        input.Player.Jump.performed += context => Jump();
    }

    private void Update()
    {
        if (!TotalNewControl.CheckForConnection(player))
        {
            movementX = 0;
        }
        else if (input.Player.Move.IsPressed()) Move(Axis);
        rigidbody.velocity = new Vector2(movementX, rigidbody.velocity.y);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, layerGrounds);
    }

    private void Move(float axis)
    {
        if (!isGrounded || !TotalNewControl.CheckForConnection(player)) return;
        movementX = axis * speed;
        Axis = axis;
        if (axis != 0)
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * axis, transform.localScale.y,
                transform.localScale.z);
    }

    private void Jump()
    {
        if (!isGrounded || !TotalNewControl.CheckForConnection(player)) return;
        rigidbody.velocity = new Vector2(movementX, jumpForce);
        isGrounded = false;
        //Axis = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        movementX = 0;
    }

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputSystem input;

    public float speed = 4;
    public float jumpForce = 7;

    public Transform groundCheck;
    public float groundRadius;
    public LayerMask layerGrounds;

    private bool isGrounded;
    private bool previousMoveWasJump = false;
    private float movementX;

    private new Rigidbody2D rigidbody;

    public static readonly Dictionary<string, bool> ConnectionIsTrue = new Dictionary<string, bool>
        {{"Square", false}, {"Square1", false}, {"Square2", false}};

    private void Awake()
    {
        input = new PlayerInputSystem();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        input.Player.Move.performed += context => Move(context.ReadValue<float>());
        input.Player.Move.canceled += context => Move(0);
        input.Player.Jump.performed += context => Jump();
        input.Player.SwapPlayer.performed += context => SwapPlayer();
    }

    private void Update()
    {
        if (ConnectionIsTrue[rigidbody.name])
        {
            rigidbody.velocity = new Vector2(movementX, rigidbody.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, layerGrounds);
        
        /*if (isGrounded && previousMoveWasJump)
        {
            rigidbody.velocity = new Vector2(0, 0);
            previousMoveWasJump = false;
        }*/
    }

    private void Move(float axis)
    {
        if (isGrounded)
        {
            if (previousMoveWasJump)
            {
                movementX = 0;
                previousMoveWasJump = false;
                rigidbody.velocity = new Vector2(0, 0);
            }
            else
                movementX = axis * speed;
        }
        else previousMoveWasJump = false;
    }

    private void Jump()
    {
        if (!(isGrounded && ConnectionIsTrue[rigidbody.name])) return;
        rigidbody.velocity = new Vector2(movementX, jumpForce);
        isGrounded = false;
        previousMoveWasJump = true;
    }

    private void SwapPlayer()
    {
        var squares = Control.Squares;
        if (ConnectionIsTrue[squares[2]])
        {
            var temp = squares[1];
            squares[1] = squares[2];
            squares[2] = squares[3];
            squares[3] = temp;
        }

        else if (ConnectionIsTrue[squares[3]])
        {
            var temp = squares[1];
            squares[1] = squares[3];
            squares[3] = squares[2];
            squares[2] = temp;
        }
    }
    
    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();
}
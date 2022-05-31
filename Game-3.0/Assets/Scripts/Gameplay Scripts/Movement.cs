using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private PlayerInputSystem input;
    private Animator anim;

    public float speed = 4;
    public float jumpForce = 7;

    public Transform groundCheck;
    public float groundRadius;
    public LayerMask layerGrounds;

    private bool isGrounded;
    private float movementX;
    private bool jump;

    private new Rigidbody2D rigidbody;
    private GameObject player;

    public static float Axis;

    private void Awake()
    {
        input = new PlayerInputSystem();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        anim=gameObject.GetComponent<Animator>();
        player = gameObject.gameObject;

        input.Player.Move.performed += context => Move(context.ReadValue<float>());
        input.Player.Move.canceled += context => Move(0);
        input.Player.Jump.performed += context => Jump();
    }

    private void Update()
    {
        if (!Control.CheckForConnection(player))
        {
            movementX = 0;
        }
        else if (input.Player.Move.IsPressed()) Move(Axis);
        rigidbody.velocity = new Vector2(movementX, rigidbody.velocity.y);
        anim.SetBool("run",movementX!=0);
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, layerGrounds);
        anim.SetBool("jump",!isGrounded);

    }

    private void Move(float axis)
    {
        if (SceneManager.GetActiveScene().name == "First Scene" || SceneManager.GetActiveScene().name == "Gallery") 
            MoveHint.StartAnimation();
        Axis = axis;
        if (!Control.CheckForConnection(player)) return;
        movementX = axis * speed;
        if (axis != 0)
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * axis, transform.localScale.y,
                transform.localScale.z);
    }

    private void Jump()
    {
        if (!isGrounded || !Control.CheckForConnection(player)) return;
        if (SceneManager.GetActiveScene().name == "First Scene") SpaceHint.StartAnimation();
        rigidbody.velocity = new Vector2(movementX, jumpForce);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        movementX = 0;
    }

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();
}

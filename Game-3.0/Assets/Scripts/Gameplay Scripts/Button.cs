using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private float startPositionY;
    private bool onCollision;

    private Animator animator;

    public float upperEdge = 5f;
    public float bottomEdge = -1f;
    public float speed = 3f;
    public GameObject platform;

    void Start()
    {
        startPositionY = transform.position.y;
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        // опускание кнопки и поднятие кнопки
        if (!onCollision)
            MovePlatformDown();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetTrigger("goDown");
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        onCollision = true;
        MovePlatformUp();
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        onCollision = false;
        MovePlatformDown();
        animator.SetTrigger("goUp");
    }

    private void MovePlatformUp()
    {
        if (platform.transform.position.y > upperEdge)
            return;
        platform.transform.position =
            new Vector2(platform.transform.position.x, platform.transform.position.y + speed * Time.deltaTime);
        
    }

    private void MovePlatformDown()
    {
        if (platform.transform.position.y < bottomEdge)
            return;
        platform.transform.position =
            new Vector2(platform.transform.position.x, platform.transform.position.y - speed * Time.deltaTime);
    }
}
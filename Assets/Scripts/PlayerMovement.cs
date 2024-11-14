using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rb;
    Vector2 moveDir;

    public Joystick joystick;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;
        
        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x == 0 & moveDir.y == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }

    void Move()
    {
        rb.velocity = new Vector2 (moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}

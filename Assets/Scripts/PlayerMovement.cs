using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed;
    Rigidbody2D rb;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;

    SpriteRenderer flipPlayer;
   
    private Camera mainCam;

    public Joystick joystick;

    private Animator anim;


    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void CameraMovement()
    {
        mainCam.transform.localPosition = new Vector3(transform.position.x, transform.position.y, -100f);
        transform.position = Vector2.MoveTowards(transform.position, mainCam.transform.localPosition, Time.deltaTime);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        flipPlayer = GetComponent<SpriteRenderer>();
        lastMovedVector = new Vector2(1, 0f);
        Canvas canvas = FindObjectOfType<Canvas>();
        joystick = canvas.transform.Find("Fixed Joystick").GetComponent<Joystick>();

    }

    void Update()
    {
        if (!isLocalPlayer) return;
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

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }
        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
        CameraMovement();
    }

    void FixedUpdate()
    {   
        Move();
    }

   

    void Move()
    {  
        if (!isLocalPlayer) return;
        rb.velocity = new Vector2 (moveDir.x * moveSpeed, moveDir.y * moveSpeed);

        if (moveDir.x < 0)
        {
            flipPlayer.flipX = true;
        }
        else
        {
            flipPlayer.flipX = false;
        }
    }
}

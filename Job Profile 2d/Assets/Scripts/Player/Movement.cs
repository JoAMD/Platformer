using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class Movement : MonoBehaviour
{
    public bool wallSliding;
    public Transform wallCheck;
    public float wallD;
    public LayerMask whatisGround;
    public bool isTouchingWall;
    public Movement player;
    public float wallSlideSpeed;
    public bool walljumping;
    public float xWallforce;
    public float yWallforce;
    public float wallJumpTime = 0.05f;
    public CharacterController2D controller;
    private Animator anim;
    public Collider2D disable;
    public float horizontalMove;
    private float runSpeed = 60f;
    private bool jump;
    private bool crouch = false;
    private int direction;
    private float dashTime;
    private float startDash = 0.1f;
    private float dashSpeed = 100f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void ResetJump()
    {
        walljumping = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallD, whatisGround);

        if (isTouchingWall == true && !controller.m_Grounded && horizontalMove != 0)
        {
            anim.SetBool("wallSliding", true);
            wallSliding = true;
        }
        else
        {
            anim.SetBool("wallSliding", false);
            wallSliding = false;
        }
        if (Input.GetButtonDown("Jump") && wallSliding == true)
        {
            walljumping = true;
            Invoke("ResetJump", 0.02f);
        }
        if (walljumping == true)
        {
           rb.velocity = new Vector2(xWallforce * horizontalMove, yWallforce);
        }
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        setAnims();
        controller.Move(horizontalMove * Time.deltaTime, crouch, jump);
        jump = false;
        Dash();
    }
    public void onLanding()
    {
    }
    public void onCrouching(bool isCrouching)
    {
        anim.SetBool("isCrouching", isCrouching);
    }
   

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            crouch = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("slide", true);
            if(disable != null)
            {
                disable.enabled = false;
            }
        }else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetBool("slide", false);
            if(disable != null)
            {
                disable.enabled = true;
            }
        }
    }
    void setAnims()
    {
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
        anim.SetFloat("yVelocity", rb.velocity.y);
    }
    void Dash()
    {
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (horizontalMove < 0)
                {
                    direction = 1;
                }

                if (horizontalMove > 0)
                {
                    direction = 2;
                }

            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDash;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
            }
            if (direction == 1)
            {
                rb.velocity = Vector2.left * dashSpeed;
            }
            else if (direction == 2)
            {
                rb.velocity = Vector2.right * dashSpeed;
            }

        }
    }
}
    

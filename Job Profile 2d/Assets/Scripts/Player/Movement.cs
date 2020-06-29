using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController2D controller;
    private Animator anim;
    public Collider2D disable;
    public float horizontalMove;
    private float runSpeed = 60f;
    private bool jump = false;
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

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
        anim.SetFloat("yVelocity", rb.velocity.y);
        controller.Move(horizontalMove * Time.deltaTime, crouch, jump);
        jump = false;
        Dash();
    }
    public void onLanding()
    {
        anim.SetBool("isJumping", false);
    }
    public void onCrouching(bool isCrouching)
    {
        anim.SetBool("isCrouching", false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            anim.SetBool("isJumping", true);
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
    

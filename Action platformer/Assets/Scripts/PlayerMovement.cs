using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    float _xAmount;
    [Range(0, 400)] public float runSpeed;
    [Range(0, 400)] public float jumpForce;
    private Vector2 m_Velocity = Vector3.zero;
    private float smoothDamp = 0.05f;
    private bool facingRight, jump;
    private int direction = 0;
    private float dashTime;
    private float startDash = 0.1f;
    public float dashSpeed;
    [Header("jump")]
    public bool isGrounded;
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask whatiGround;
    private int extraJumps;
    private int extraJumpValue = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
        
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }
    }
    void FixedUpdate()
    {
        Checker();
        move();
        Flip(_xAmount);
        Jump();

    }

    void move()
    {
        Vector2 targetVelocity = new Vector2(_xAmount * runSpeed * Time.deltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, smoothDamp);
    }

    private void Flip(float x)
    {
        if (x > 0 && !facingRight || x < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    void Jump()
    {
        if (jump && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            jump = false;
        
        }
        else if (jump && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    void Controls()
    {
        _xAmount = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            jump = true;
        }
        Dash();
     
    }
    void Checker()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatiGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
    #region
    void Dash()
    {
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (_xAmount < 0)
                {
                    direction = 1;
                }

                if (_xAmount > 0)
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
    #endregion Dash
}

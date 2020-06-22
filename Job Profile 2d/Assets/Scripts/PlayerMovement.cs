using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float _xAmount;
    public static float _xMovement, _amount;
    private bool facingRight = true;
    private Animator anim;
    private bool crouch;
    public Transform ceilingCheck;
    public float ceilingRadius;
    public bool ceiling;
    private float crouchSpeed = 0.2f;
    public Vector2 wallHopD;
    public Vector2 wallJumpD;
    public float wallJump = 20f;
    public  float wallHop = 10f;
    private int facingD = 0;
    public LayerMask whatisGround;
    public LayerMask whatisWall;
    private float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2.5f;
    public float jumpForce = 3000f;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    private bool jump;
    public bool isTouchingWall;
    public Transform wallCheck;
    public float wallCheckD;
    private bool isWallSliding;
    public float wallSlidingSpeed;
    private bool isWallJumping;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        wallHopD.Normalize();
        wallJumpD.Normalize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _xMovement = Input.GetAxisRaw("Horizontal");
        HandleMovement(_xMovement);
        flip(_xMovement);
        anim.SetFloat("Speed", Mathf.Abs(_xMovement));
        if (Input.GetKeyDown(KeyCode.C) && !ceiling)
        {
            crouch = true;
            _amount *= crouchSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.C) && !ceiling)
        {
            crouch = false;
        }
        Crouch();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * lowJumpMultiplier);
        }
    }
    private void Update()
    {
        checkWallSliding();
        CheckCanJump();
        anim.SetBool("jump", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("wallSlide", isWallSliding);
        CheckArea();
        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }
        }
    }
    void HandleMovement(float _X)
    {
        if (!anim.GetBool("Slide"))
        {
            rb.velocity = new Vector2(_amount, rb.velocity.y);
        }
         _amount = _xAmount * _xMovement * Time.fixedDeltaTime;  
    }
    void flip(float _x)
    {
        if(_x > 0 && !facingRight || _x < 0 && facingRight && !isWallSliding)
        {
            facingD *= -1;
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    void Crouch()
    {
        if (crouch == true)
        {
            anim.SetBool("Crouch", true);
        }
        else
        {
            anim.SetBool("Crouch", false);
        }

    }
    void Jump()
    {
        float _yMove = jumpForce  * Time.fixedDeltaTime;
        if (jump && !isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, _yMove);
            rb.gravityScale = 2;
        }
        else if(jump && isWallSliding)//wall hop
        {
            isWallSliding = false;
            Vector2 addForce = new Vector2(wallHop * wallHopD.x * -facingD * _xMovement, wallHop * wallHopD.y);
            rb.AddForce(addForce, ForceMode2D.Impulse);
        }
        else if (isWallSliding && jump && _xMovement != 0)
        {
            isWallSliding = false;
            isWallJumping = true;
            Invoke("ResetWallJump", .5f);
            if (isWallJumping)
            {
                rb.velocity = new Vector2(wallJump * wallJumpD.x * _xMovement, wallJump * wallJumpD.y);
            }

        }
    }
    void CheckCanJump()
    {
        if ((isGrounded && rb.velocity.y <= 0.01f) || isWallSliding)
        {
            jump = true;
        }
        else
        {
            jump = false;
            anim.SetBool("jump", false);
        }
    }
    void checkWallSliding()
    {
        if(isTouchingWall && rb.velocity.y < 0f && !isGrounded && _xMovement == 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    void CheckArea()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatisGround);
        ceiling = Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatisGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckD, whatisGround);
    }
    void ResetWallJump()
    {
        isWallJumping = false;
    }
}

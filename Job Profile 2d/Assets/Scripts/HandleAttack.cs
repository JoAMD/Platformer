using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAttack : MonoBehaviour
{
    private Animator anim;
    private bool attack, slide = false;
    private Rigidbody2D rb;
    private float dashTime;
    public float dashSpeed;
    public float startDash;
    private int direction;
    List<string> animlist = new List<string>(new string[] { "attack", "attack2", "attack3" });
    private int combonum = 3;
    private float reset;
    private float resetTime = 4f;
    private float AttackTime;
    private float attackRate = .5f;
    public LayerMask enemyLayers;
    private float attackRange;
    public Transform attackPoint;
   
   
    private PlayerMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDash;
    }
    private void FixedUpdate()
    {
        Attack();
        Slide();
        Dash();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
      
    }
    void HandleInput()
    {
        if(Time.time >= AttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                attack = true;
                AttackTime = Time.time + attackRate;
            }
       
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
        }

        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 1;
            }
            else if (Input.GetKeyDown(KeyCode.RightShift))
            {
                direction = 2;
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
            else {
                dashTime -= Time.deltaTime;
            }
              
        }
    }
    void Attack()
    {
        if (attack)
        {
            int index = Random.Range(1, 4);
            anim.SetTrigger("attack" + index);
            rb.velocity = new Vector2(0,0);
            attack = false;
            Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider2D destructible in hitenemies)
            {
                Debug.Log("enemy hit"); 
            }
        }
    }
    void Slide()
    {
        if(slide)
        {
            anim.SetBool("Slide", true);
            slide = false;
        }else if (!slide)
        {
            anim.SetBool("Slide", false);
        }
    }
    void Dash()
    {
        if(direction == 1)
        {
            rb.velocity = Vector2.left * dashSpeed * Time.deltaTime;
        }else if(direction == 2)
        {
            rb.velocity = Vector2.right * dashSpeed * Time.deltaTime;
        }
    }
   
  
}

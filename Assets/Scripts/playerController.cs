using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Unity system
    public GameObject eventSystem;
    public CharacterController controller;
    heroProperties properties;
    Animator animator;

    // Character move and jump
    public float curr_movespeed = 6f;
    public int jumpCount = 2;
    Vector3 velocity;
    int jumpDefault;
    float jumpHeight;

    // Environment relevant
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public LayerMask boundaryMask;
    bool isBoundary;
    // Attack relevant
    public float curr_attackDamage;
    public float curr_attackSpeed;
    float attackCoolDown = 0f;
    float attackRange;
    float attackRecover;
    float attackRecover_timer = 0f;
    public LayerMask enemyLayers;
    Transform attackPoint;

    // Sound relevant
    public AudioSource attackSound;
    public AudioSource attackHitSound;
    private void Start()
    {
        properties = GetComponent<heroProperties>();
        eventSystem = GameObject.Find("EventSystem");
        // assigning move and jump
        curr_movespeed = properties.moveSpeed;
        jumpHeight = properties.jumpHeight;
        jumpDefault = jumpCount;
        // assigning attack relevant
        curr_attackDamage = properties.attackDamage;
        curr_attackSpeed = properties.attackSpeed;
        attackRange = properties.attackRange;
        attackPoint = properties.attackPoint;
        attackRecover = properties.attackRecover;
        // Unity system
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isBoundary = Physics.CheckSphere(groundCheck.position, groundDistance, boundaryMask);

        // if the player is at the boundary, then he will die.
        if (isBoundary)
        {
            eventSystem.GetComponent<GameSystem>().player1Die();
            return;
        }

        // for each frame, if attackRecover is required, means that the character just attacked,
        // during this time, the character should freeze : no move, no jump, no attack again, no lay down,
        // until the recover time is over;
        if (attackRecover_timer > 0)
        {
            attackRecover_timer -= Time.deltaTime;
            return;
        }
        
        // if the player is on the ground, do 3 things:
        // 1. tell animator that the character is on the ground
        // 2. reset the jumpcount
        // 3. keep the velocity of y as -2f, instead of accelerating.
        if (isGrounded && velocity.y < 0) {
            animator.SetBool("isGrounded", true);
            jumpCount = jumpDefault;
            velocity.y = -2f;
        }

        // collect different inputs.
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool jump = Input.GetButtonDown("Jump");
        bool attack = Input.GetButtonDown("Attack");

        // character will fall down due to the effect of gravity
        // PS: if the character is in attack_recover, it will hold and freeze in the air
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // everytime the player attack, it will update the next allowed attack time: Time.time + attackCooldown
        if(Time.time >= attackCoolDown)
        {
            if (attack)
            {
                attackSound.Play(0);
                animator.SetTrigger("attack");
                
                attackCoolDown = Time.time + 1f / curr_attackSpeed;
                attackRecover_timer = attackRecover;
            }
        }
        
        if(jump && jumpCount > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumpCount--;
            animator.SetTrigger("jump");
            animator.SetBool("isGrounded", false);
        }

        if (horizontal != 0)
        {
            animator.SetBool("isMoving", true);
            transform.rotation = horizontal > 0 ? Quaternion.Euler(0f, 90f, 0f) : Quaternion.Euler(0f, -90f, 0f);
            controller.Move(curr_movespeed * new Vector3(horizontal, 0,0) * Time.deltaTime);   
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if(vertical != 0)
        {
            Vector3 jumpDirection = new Vector3(0, 0, vertical).normalized;

            // if the character is on the ground and player press down button, then lay down.
            if (vertical == -1 && isGrounded )
            {
                animator.SetBool("isLying", true);
            }
        }
        else
        {
            animator.SetBool("isLying", false);
        }
       
    }
    public void checkAttackPoint()
    {
        Collider[] hitEnermies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemy in hitEnermies)
        {
            attackHitSound.Play(0);
            enemy.GetComponent<EnemyProperties>().takeDamage(curr_attackDamage);
        }
    }
    
}

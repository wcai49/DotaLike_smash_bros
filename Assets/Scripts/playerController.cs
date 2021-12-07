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
    public LayerMask enemyLayers;
    Transform attackPoint;

    // Sound relevant
    public AudioSource attackSound;
    public AudioSource attackHitSound;
    private void Start()
    {
        properties = GetComponent<heroProperties>();

        // assigning move and jump
        curr_movespeed = properties.moveSpeed;
        jumpHeight = properties.jumpHeight;
        jumpDefault = jumpCount;
        // assigning attack relevant
        curr_attackDamage = properties.attackDamage;
        curr_attackSpeed = properties.attackSpeed;
        attackRange = properties.attackRange;
        attackPoint = properties.attackPoint;
        // Unity system
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isBoundary = Physics.CheckSphere(groundCheck.position, groundDistance, boundaryMask);

        if (isBoundary)
        {
            eventSystem.GetComponent<GameSystem>().player1Die();
            return;
        }
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
        Vector3 jumpDirection = new Vector3(0, 0, vertical).normalized;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        if(Time.time >= attackCoolDown)
        {
            if (attack)
            {
                attackSound.Play(0);
                animator.SetTrigger("attack");
                Collider[] hitEnermies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider enemy in hitEnermies)
                {
                    attackHitSound.Play(0);
                    enemy.GetComponent<EnemyProperties>().takeDamage(curr_attackDamage);
                }
                attackCoolDown = Time.time + 1f / curr_attackSpeed;
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
            if (vertical == -1 && isGrounded )
            {
                animator.SetBool("isLying", true);
            }
            else
            {
                Debug.Log("Pressing up");
            }
        }
        else
        {
            animator.SetBool("isLying", false);
        }



        
    }
    
}

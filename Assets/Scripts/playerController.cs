using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController controller;
    public float curr_movespeed = 6f;
    public float gravity = -9.81f;
    public int jumpCount = 2;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    heroProperties properties;

    Vector3 velocity;
    Animator animator;
    bool isGrounded;
    int jumpDefault;
    float jumpHeight;

    private void Start()
    {
        properties = GetComponent<heroProperties>();

        curr_movespeed = properties.moveSpeed;
        jumpHeight = properties.jumpHeight;
        jumpDefault = jumpCount;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
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

        
        if(jump && jumpCount > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumpCount--;
            animator.SetTrigger("jump");
            animator.SetBool("isGrounded", false);
        }

        if (attack)
        {
            horizontal = 0;
            animator.SetTrigger("attack");
        }

        if (horizontal != 0)
        {
            animator.SetBool("isMoving", true);
            transform.rotation = horizontal > 0 ? Quaternion.Euler(0f, 120f, 0f) : Quaternion.Euler(0f, -120f, 0f);
            controller.Move(curr_movespeed * new Vector3(horizontal, 0,0) * Time.deltaTime);   
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if(vertical != 0)
        {
            if (vertical < 0 && isGrounded)
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



        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

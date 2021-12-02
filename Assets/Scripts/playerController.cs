using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController controller;
    public float movespeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.8f;
    public int jumpCount = 1;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    Animator animator;
    bool isGrounded;
    int jumpDefault;

    private void Start()
    {
        jumpDefault = jumpCount;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) {
            jumpCount = jumpDefault;
            velocity.y = -2f;
        }



        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Vector3 direction = new Vector3(horizontal, 0, 0).normalized;
        Vector3 jumpDirection = new Vector3(0, 0, vertical).normalized;

        bool jump = Input.GetButtonDown("Jump");
        if(jump && jumpCount > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpCount--;
        }

        if (horizontal != 0)
        {
            animator.SetBool("isMoving", true);
            transform.rotation = horizontal > 0 ? Quaternion.Euler(0f, 90f, 0f) : Quaternion.Euler(0f, -90f, 0f);
            controller.Move(movespeed * new Vector3(horizontal, 0,0) * Time.deltaTime);   
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

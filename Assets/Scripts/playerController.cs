using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController controller;
    public float movespeed = 6f;
    public float gravity = -9.18f;
    public float jumpHeight = 1.8f;

    public int jumpCount = 1;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            jumpCount = 1;
            velocity.y = -2f;
        }



        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, 0).normalized;
        Vector3 jumpDirection = new Vector3(0, 0, vertical).normalized;

        if(jumpDirection.magnitude > 0.1)
        {
            Debug.Log(jumpDirection);
        }

        if(direction.magnitude > 0.1)
        {
            transform.rotation = direction.x > 0 ? Quaternion.Euler(0f, 120f, 0f) : Quaternion.Euler(0f, -120f, 0f);
            controller.Move(movespeed * direction * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheker;
    public LayerMask groundMask;
    public float gravity = -9.81f;

    readonly float groundDistance = 0.4f;
    readonly float speed = 12f;
    readonly float jumpHeight = 3f;

    Vector3 velocity;
    [SerializeField] bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheker.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -10f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(speed * Time.deltaTime * move);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // formula of jump square root of height * -2 * gravity
        }

        velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * Time.deltaTime); // two times time.deltaTime because the acceleration
        
    }
}

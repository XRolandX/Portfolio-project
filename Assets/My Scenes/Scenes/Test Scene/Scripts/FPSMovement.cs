using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public CharacterController contorller;
    public Transform groundChecker;
    public LayerMask groundMask;


    readonly float speed = 12f;
    readonly float groundedCheckerRadius = 0.4f;
    readonly float jumpHeight = 3f;

    [SerializeField] bool isGrounded;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Vector3 velocity;


    void Update()
    {
        #region M O V I N G
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveY;
        contorller.Move(speed * Time.deltaTime * move);
        #endregion

        #region G R O U N D    C H E C K    A N D    G R A V I T Y    C O N D I T I O N S
        isGrounded = Physics.CheckSphere(groundChecker.position, groundedCheckerRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = gravity;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        contorller.Move(Time.deltaTime * velocity);  // two deltatime because the acceleration of free fall
        #endregion
    }
}

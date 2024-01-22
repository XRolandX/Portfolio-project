using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public Joystick moveJoystick;
    public CharacterController controller;
    public Transform groundCheker;
    public Button jumpButton;
    public LayerMask groundMask;
    public float gravity = -9.81f;

    readonly float groundDistance = 0.4f;
    readonly float speed = 12f;
    readonly float jumpHeight = 1f;

    Vector3 velocity; // gravity target for CharacterController.Move()

    [SerializeField] bool isGrounded;

    

    private void Awake()
    {
        transform.localScale = Vector3.one; // set the parent scale to avoid the child scale deformation 
        if (GameObject.Find("Jump Button").TryGetComponent<Button>(out jumpButton))
        {
            jumpButton.onClick.AddListener(JumpUIButton);
        }
    }
    void Update()
    {
        MoveWithKeyboard();
        MoveWithJoystick();

        Gravity();
        JumpKeyboardButton();
    }

    public void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheker.position, groundDistance, groundMask); // ground checker job

        
        velocity.y += gravity * Time.deltaTime; // strength down

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -10f;
        }

        controller.Move(2 * Time.deltaTime * velocity); // two times time.deltaTime because the acceleration
        
    }
    private void MoveWithJoystick()
    {

        if (moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
        {
            float x = moveJoystick.Horizontal;
            float z = moveJoystick.Vertical;
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(speed * Time.deltaTime * move);
        }

    }
    private void JumpUIButton()
    { 
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

#if UNITY_STANDALONE_WIN

    private void JumpKeyboardButton()
    {

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // formula of jump square root of height * -2 * gravity
        }

    }
    private void MoveWithKeyboard()
    {


        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(speed * Time.deltaTime * move);
        }


    }

#endif
}

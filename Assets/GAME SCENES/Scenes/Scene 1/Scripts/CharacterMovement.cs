using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
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

    private PlayerControls playerControls;
    private Vector2 moveInput;

    readonly float groundDistance = 0.4f;
    readonly float speed = 12f;
    readonly float jumpHeight = 1f;

    Vector3 velocity; // gravity target for CharacterController.Move()

    [SerializeField] bool isGrounded;

    private void PlayerControlsInitialisation()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }
    private void Awake()
    {
        PlayerControlsInitialisation();
    }
    void Update()
    {
        MoveWithJoystick();
        Gravity();
#if UNITY_STANDALONE_WIN
        MoveWithKeyboard();
        JumpKeyboardButton();
#endif
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
    public void JumpUIButton()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }                
    private void JumpKeyboardButton()
    {
        if (playerControls.Player.Jump.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // formula of jump square root of height * -2 * gravity
        }
    }
    private void MoveWithKeyboard()
    {
        Vector3 move = transform.TransformDirection(new Vector3(moveInput.x, 0f, moveInput.y));
        controller.Move(speed * Time.deltaTime * move);
    }
    private void OnEnable()
    {
        playerControls.Player.Enable();
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
    }
}

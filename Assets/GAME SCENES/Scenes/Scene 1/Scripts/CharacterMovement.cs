using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
<<<<<<< HEAD
    public CharacterController characterController;
    private PlayerControls playerControls;
    public Transform groundChecker;
    public Joystick moveJoystick;

    private Vector3 gravityVelocity;
=======
    public Joystick moveJoystick;
    public CharacterController controller;
    public Transform groundCheker;
    public Button jumpButton;
>>>>>>> parent of 84c6216 (minor improvements scene 2)
    public LayerMask groundMask;
    public float gravity = -9.81f;

    private PlayerControls playerControls;
    private Vector2 moveInput;

<<<<<<< HEAD
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float jumpPower = 1f;
    [SerializeField] private bool isGrounded;
=======
    readonly float groundDistance = 0.4f;
    readonly float speed = 12f;
    readonly float jumpHeight = 1f;

    Vector3 velocity; // gravity target for CharacterController.Move()
>>>>>>> parent of 84c6216 (minor improvements scene 2)

    private readonly float groundCheckerDistance = 0.1f;
    public readonly float gravity = -9.81f * 4.0f;

    private void PlayerControlsInitialisation()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }
<<<<<<< HEAD

    void Update()
    {
        Gravity();

#if UNITY_ANDROID
            MoveWithJoystick();
#endif

=======
    private void Awake()
    {

        PlayerControlsInitialisation();
    }
    void Update()
    {
        MoveWithJoystick();
        Gravity();
>>>>>>> parent of 84c6216 (minor improvements scene 2)
#if UNITY_STANDALONE_WIN
        MoveWithKeyboard();
        JumpKeyboardButton();
#endif
    }

    public void Gravity()
    {
<<<<<<< HEAD
        isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckerDistance, groundMask);

        if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }
        else
        {
            gravityVelocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(gravityVelocity * Time.deltaTime);
    }

    private void MoveWithKeyboard()
    {
        _ = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        characterController.Move(movementSpeed * Time.deltaTime * move);
    }

    private void JumpKeyboardButton()
    {
        if (playerControls.Player.Jump.triggered && isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpPower * -2 * gravity);
        }
    }

    #region ANDROID CONTROL
=======
        isGrounded = Physics.CheckSphere(groundCheker.position, groundDistance, groundMask); // ground checker job
        velocity.y += gravity * Time.deltaTime; // strength down

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -10f;
        }

        controller.Move(2 * Time.deltaTime * velocity); // two times time.deltaTime because the acceleration
    }
>>>>>>> parent of 84c6216 (minor improvements scene 2)
    private void MoveWithJoystick()
    {

        if (moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
        {
            float x = moveJoystick.Horizontal;
            float z = moveJoystick.Vertical;
            Vector3 move = transform.right * x + transform.forward * z;
<<<<<<< HEAD

            // Переміщення за допомогою CharacterController
            characterController.Move(movementSpeed * Time.deltaTime * move);
=======
            controller.Move(speed * Time.deltaTime * move);
>>>>>>> parent of 84c6216 (minor improvements scene 2)
        }

    }

    public void JumpUIButton()
    {
        if (isGrounded)
        {
<<<<<<< HEAD
            gravityVelocity.y = Mathf.Sqrt(jumpPower * -2 * gravity);  // Стрибок через UI кнопку
=======
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }                
    private void JumpKeyboardButton()
    {
        if (playerControls.Player.Jump.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // formula of jump square root of height * -2 * gravity
>>>>>>> parent of 84c6216 (minor improvements scene 2)
        }
    }
    private void MoveWithKeyboard()
    {
        Vector3 move = new(moveInput.x, 0f, moveInput.y);
        controller.transform.Translate(speed * Time.deltaTime * move);
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

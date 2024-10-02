using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController characterController;
    private PlayerControls playerControls;
    public Transform groundChecker;
    public Joystick moveJoystick;

    private Vector3 gravityVelocity;
    public LayerMask groundMask;
    private Vector2 moveInput;

    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float jumpPower = 1f;
    [SerializeField] private bool isGrounded;

    private readonly float groundCheckerDistance = 0.1f;
    public readonly float gravity = -9.81f * 4.0f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void Update()
    {
        Gravity();

#if UNITY_ANDROID
            MoveWithJoystick();
#endif

#if UNITY_STANDALONE_WIN
        MoveWithKeyboard();
        JumpKeyboardButton();
#endif
    }

    public void Gravity()
    {
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
    private void MoveWithJoystick()
    {
        if (moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
        {
            float x = moveJoystick.Horizontal;
            float z = moveJoystick.Vertical;
            Vector3 move = transform.right * x + transform.forward * z;

            // Переміщення за допомогою CharacterController
            characterController.Move(movementSpeed * Time.deltaTime * move);
        }
    }

    public void JumpUIButton()
    {
        if (isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpPower * -2 * gravity);  // Стрибок через UI кнопку
        }
    }
    #endregion

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
    }
}

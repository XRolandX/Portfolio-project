using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    public Transform groundChecker;
    public Joystick moveJoystick;
    public Button jumpButton;

    private Vector3 gravityVelocity;
    public LayerMask groundMask;
    private Vector2 moveInput;

    readonly float groundCheckerDistance = 0.4f;
    public readonly float gravity = -9.81f;
    readonly float movementSpeed = 12f;
    readonly float jumpPower = 1f;

    [SerializeField] bool isGrounded;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }
<<<<<<< Updated upstream
    private void Awake()
    {

        PlayerControlsInitialisation();
    }
=======
>>>>>>> Stashed changes
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
        isGrounded = Physics.Raycast(groundChecker.position, Vector3.down, groundCheckerDistance, groundMask);

        if (!isGrounded)
        {
            gravityVelocity.y += gravity * Time.deltaTime;
            transform.Translate(gravityVelocity * Time.deltaTime);
        }
    }

    private void MoveWithKeyboard()
    {
        Vector3 move = movementSpeed * Time.deltaTime * new Vector3(moveInput.x, 0f, moveInput.y);
        transform.Translate(move, Space.Self);
    }

    private void JumpKeyboardButton()
    {
        if (playerControls.Player.Jump.triggered && isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpPower * -2 * gravity); // formula of jump square is root of height * -2 * gravity
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
            transform.Translate(move, Space.Self);
        }
    }
    public void JumpUIButton()
    {
        if (isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpPower * -2 * gravity);
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

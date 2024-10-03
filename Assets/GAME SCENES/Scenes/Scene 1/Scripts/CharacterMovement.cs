using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController characterController;
    private PlayerControls playerControls;
    public Transform groundCheker;
    public Joystick moveJoystick;

    private Vector3 gravityVelocity;
    public LayerMask groundMask;
    private Vector2 moveInput;
    
    private readonly float gravity = -9.81f * 4.0f;
    private readonly float groundDistance = 0.1f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float speed = 12f;
    [SerializeField] bool isGrounded;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void Update()
    {
        Gravity();

#if UNITY_STANDALONE_WIN
        MoveWithKeyboard();
        JumpKeyboardButton();
#endif

#if UNITY_ANDROID
        MoveWithJoystick();
#endif
    }

    public void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheker.position, groundDistance, groundMask);

        if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }
        else
        {
            gravityVelocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(Time.deltaTime * gravityVelocity);
    }

    private void JumpKeyboardButton()
    {
        if (playerControls.Player.Jump.triggered && isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
    private void MoveWithKeyboard()
    {
        _ = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        characterController.Move(speed * Time.deltaTime * move);
    }

    #region ANDROID CONTROL
    private void MoveWithJoystick()
    {

        if (moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
        {
            float x = moveJoystick.Horizontal;
            float z = moveJoystick.Vertical;
            Vector3 move = transform.right * x + transform.forward * z;
            characterController.Move(speed * Time.deltaTime * move);
        }

    }
    public void JumpUIButton()
    {
        if (isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
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

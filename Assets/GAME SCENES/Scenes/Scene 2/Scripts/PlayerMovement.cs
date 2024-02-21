using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed { get; set; } = 25f;
    [SerializeField] private float mouseSensitivity = 20f;
    private float yRotation = 0;

    private CharacterController controller;
    private PlayerControls playerControls;

    private Vector2 moveInput;
    private Vector2 lookInput;

    public Joystick moveJoystick;
    public Joystick lookJoystick;

    void Awake()
    {
        playerControls = new PlayerControls();
        controller = GetComponent<CharacterController>();

        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        playerControls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void LateUpdate()
    {
#if UNITY_STANDALONE_WIN
        PlayerRotation();
        PlayerMove();
#endif

#if UNITY_ANDROID
        JoystickMove();
        JoystickRotation();
#endif
    }

    void PlayerMove()
    {
        Vector3 move = MoveSpeed * Time.deltaTime * new Vector3(moveInput.x, 0f, moveInput.y);
        controller.transform.Translate(move);
    }

    void PlayerRotation()
    {
        Vector3 look = Time.deltaTime * mouseSensitivity * lookInput;
        transform.Rotate(0f, look.x, 0f, Space.World);
    }
    
    void JoystickMove()
    {
        if(moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
        {
            float x = moveJoystick.Horizontal;
            float y = moveJoystick.Vertical;
            Vector3 move = new (x, 0, y);
            move = controller.transform.TransformDirection(move);
            controller.Move(MoveSpeed * Time.deltaTime * move);
        }
    }
    void JoystickRotation()
    {
        float joyX = lookJoystick.Horizontal * Time.deltaTime * mouseSensitivity;
        yRotation += joyX;
        controller.transform.rotation = Quaternion.Euler(0, yRotation, 0);
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

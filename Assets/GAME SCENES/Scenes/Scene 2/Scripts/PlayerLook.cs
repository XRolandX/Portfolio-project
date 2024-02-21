using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private PlayerControls playerControls;
    public Joystick rotationYJoystick;
    private Camera playerCamera;

    private Vector2 lookInput;
    public float MouseSensitivity { get; set; } = 25f;

    private float pitch = 0f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }
    void Update()
    {
#if UNITY_STANDALONE_WIN
        MouseRotationY();
#endif

#if UNITY_ANDROID
        JoystickRotationY();
#endif
    }

    void MouseRotationY()
    {
        Vector2 look = MouseSensitivity * Time.deltaTime * lookInput;
        pitch -= look.y;
        pitch = Mathf.Clamp(pitch, -85f, 85f);
        transform.localEulerAngles = new Vector3(pitch, transform.localEulerAngles.y, 0f);
    }
    void JoystickRotationY()
    {
        if(rotationYJoystick.Vertical != 0f)
        {
            float joyY = rotationYJoystick.Vertical * Time.deltaTime * MouseSensitivity;
            pitch -= joyY;
            pitch = Mathf.Clamp(pitch, -85f, 85f);
            transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }

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
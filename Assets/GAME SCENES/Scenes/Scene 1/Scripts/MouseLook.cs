using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform weaponHolderTransform;
    private PlayerControls playerControls;
    public Transform characterBody;
    public Joystick lookJoystick;
    private Vector2 lookInput;
    
    public float unscopeSensitivity = 150f;
    public float scopeSensitivity = 100f;
    public float scope2xSensitivity = 50;
#if UNITY_STANDALONE_WIN
    private float cameraPitch = 0;
#endif

#if PLATFORM_ANDROID
    private float xRotation = 0f;
#endif

    [SerializeField] private float mouseSensitivity = 0f;

    public float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void Update()
    {
        Look();
    }

    private void Look()
    {
        #region L O O K   W I T H   T H E   M O U S E
#if UNITY_STANDALONE_WIN
        Vector3 look = mouseSensitivity * Time.deltaTime * lookInput;
        characterBody.Rotate(0f, look.x, 0f, Space.World);
        cameraPitch -= look.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);
        weaponHolderTransform.localEulerAngles = new Vector3(cameraPitch, transform.localEulerAngles.y, 0f);
#endif
        #endregion

        #region L O O K   W I T H   T H E   J O Y S T I C K
#if PLATFORM_ANDROID
        if (lookJoystick.Horizontal != 0 || lookJoystick.Vertical != 0)
        {
            float mouseX = lookJoystick.Horizontal * Time.deltaTime * MouseSensitivity;
            float mouseY = lookJoystick.Vertical * Time.deltaTime * MouseSensitivity;

            characterBody.Rotate(new Vector3(0f, mouseX, 0f)); // or Vector3.up * mouseX

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -85f, 85f);

            weaponHolderTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
#endif
        #endregion
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

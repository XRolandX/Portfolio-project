using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float sensitivity = 50f;
    private float cameraPitch = 0f;
    
    private Vector2 moveInput;
    private Vector2 lookInput;

    public PlayerControls controls;
    private Camera playerCamera;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        playerCamera = Camera.main;
    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
    private void Update()
    {
        Vector3 move = moveSpeed * Time.deltaTime * new Vector3 (moveInput.x, 0f, moveInput.y);
        transform.Translate(move, Space.Self);


        Vector2 look = sensitivity * Time.deltaTime * lookInput;
        transform.Rotate(0, look.x, 0, Space.World);
        cameraPitch -= look.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);
        playerCamera.transform.localEulerAngles = new Vector3(cameraPitch, playerCamera.transform.localEulerAngles.y, 0f);

    }
}

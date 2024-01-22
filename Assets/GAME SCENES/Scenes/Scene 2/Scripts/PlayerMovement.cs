using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed { get; set; } = 25f;
    [SerializeField] private float mouseSensitivity = 100f;
    private float yRotation = 0;
    private Vector3 moveDirection = Vector3.zero;

    private CharacterController controller;

    public Joystick moveJoystick;
    public Joystick lookJoystick;

    void Start()
    {
        controller = GetComponent<CharacterController>();
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
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new(moveX, 0, moveZ);
        move = controller.transform.TransformDirection(move);
        moveDirection = MoveSpeed * Time.deltaTime * move;

        controller.Move(moveDirection);

    }

    void PlayerRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        yRotation += mouseX;
        controller.transform.rotation = Quaternion.Euler(0, yRotation, 0);
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


}

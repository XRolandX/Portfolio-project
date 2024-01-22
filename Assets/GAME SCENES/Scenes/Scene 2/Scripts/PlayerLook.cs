using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Joystick rotationYJoystick;
    public float MouseSensitivity { get; set; } = 300f;

    private float pitch = 0f;

    // Update is called once per frame
    void LateUpdate()
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

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            // Get the mouse input
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;

            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -85f, 85f);
            transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        }

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

}
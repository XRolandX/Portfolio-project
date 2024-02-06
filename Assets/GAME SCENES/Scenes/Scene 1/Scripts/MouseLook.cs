using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Joystick lookJoystick;
    public Transform characterBody;
    public Transform weaponHolder;

    [SerializeField] private float mouseSensitivity = 0f;
    public float unscopeSensitivity = 150f;
    public float scopeSensitivity = 100f;
    public float scope2xSensitivity = 50;
    public float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }

    private float xRotation = 0f;

    private void Start()
    {
#if UNITY_STANDALONE_WIN
        Cursor.lockState = CursorLockMode.None;
#endif
    }

    void LateUpdate()
    {
        Look();

        #region Touches checker
        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    Vector3 touchesPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
        //    Debug.DrawLine(Vector3.zero, touchesPosition, Color.red);
        //}
        #endregion
    }

    private void Look()
    {
#if UNITY_STANDALONE_WIN
        #region L O O K   W I T H   T H E   M O U S E

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;

            characterBody.Rotate(new Vector3(0f, mouseX, 0f)); // or Vector3.up * mouseX

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -85f, 85f);

            weaponHolder.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        #endregion
#endif

#if PLATFORM_ANDROID
        #region L O O K   W I T H   T H E   J O Y S T I C K

        if (lookJoystick.Horizontal != 0 || lookJoystick.Vertical != 0)
        {
            float mouseX = lookJoystick.Horizontal * Time.deltaTime * MouseSensitivity;
            float mouseY = lookJoystick.Vertical * Time.deltaTime * MouseSensitivity;

            characterBody.Rotate(new Vector3(0f, mouseX, 0f)); // or Vector3.up * mouseX

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -85f, 85f);

            weaponHolder.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        #endregion
#endif
    }
}

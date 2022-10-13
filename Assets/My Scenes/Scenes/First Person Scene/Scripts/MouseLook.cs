using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Joystick lookJoystick;
    public Transform characterBody;
    public Transform weaponHolder;

    [SerializeField] float mouseSensitivity = 100f;
    public float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }

    public readonly float unscopeSensitivity = 150f;
    public readonly float scopeSensitivity = 25f;

    float xRotation = 0f;

    private void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked; // disable for mobile joystick controller
    }

    void LateUpdate()
    {
#if UNITY_EDITOR
        #region Look with the mouse
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
        #region Look with the joystick
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

        #region Touches checker
        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    Vector3 touchesPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
        //    Debug.DrawLine(Vector3.zero, touchesPosition, Color.red);
        //}
        #endregion
    }
}

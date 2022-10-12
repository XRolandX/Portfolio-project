using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    public Joystick moveJoystick;
    public Joystick lookJoystick;
    public CharacterController controller;
    readonly float speed = 12f;



    public Transform characterBody;
    public Transform weaponHolder;
    [SerializeField] float mouseSensitivity = 100f;
    float xRotation = 0f;

    private void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked; // disable for mobile joystick controller
    }

    void LateUpdate()
    {
        float mouseX = lookJoystick.Horizontal * Time.deltaTime * mouseSensitivity;
        float mouseY = lookJoystick.Vertical * Time.deltaTime * mouseSensitivity;

        characterBody.Rotate(new Vector3(0f, mouseX, 0f)); // or Vector3.up * mouseX

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        weaponHolder.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    void Update()
    {
        float x = moveJoystick.Horizontal;
        float z = moveJoystick.Vertical;
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(speed * Time.deltaTime * move);

        for(int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchesPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(Vector3.zero, touchesPosition, Color.red);
        }

    }
}

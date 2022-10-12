using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform characterBody;
    public Transform weaponHolder;

    [SerializeField] float mouseSensitivity =  100f;
    float xRotation = 0f;

    private void Start()
    {
      // Cursor.lockState = CursorLockMode.Locked; // disable for mobile joystick controller
    }
    
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        characterBody.Rotate(new Vector3(0f, mouseX, 0f)); // or Vector3.up * mouseX

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        weaponHolder.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float MouseSensitivity { get; set; } = 300f;

    private float pitch = 0f;

    // Update is called once per frame
    void LateUpdate()
    {
        CameraRotationX();
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }


    void CameraRotationX()
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

}
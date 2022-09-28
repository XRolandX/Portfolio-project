using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFPSLook : MonoBehaviour
{
    public Transform characterBody;
    readonly float sensitivity = 100f;
    float xRotation = 0f;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        characterBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);




    }
}

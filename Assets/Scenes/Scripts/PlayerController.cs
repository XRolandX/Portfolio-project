using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 200f;
    Vector3 currentEulerAngle;

    public float minX = -90f;
    public float maxX = 90f;

    public float rotationX;
    public float rotationY;
    

    public void Update()
    {
        MouseAiming();
    }

    void MouseAiming()
    {
        // take mouse input x and input y and multiply on turn speed value
        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");

        // set rotation value to x, y, z for eulerAngle
        rotationX += inputY * turnSpeed;
        rotationY += inputX * turnSpeed;

        currentEulerAngle = new Vector3(Mathf.Clamp(-rotationX, minX, maxX), rotationY, 0);

        transform.eulerAngles = currentEulerAngle;

    }

}

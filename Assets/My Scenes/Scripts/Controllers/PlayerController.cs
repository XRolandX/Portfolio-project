using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    [SerializeField] private float minViewAngle;
    [SerializeField] private float maxViewAngle;

    [Tooltip("GameObject Rotation value axis X modificated by Mouse axis Y")] [SerializeField] private float rotationX;
    [Tooltip("GameObject Rotation value axis Y modificated by Mouse axis X")] [SerializeField] private float rotationY;

    void MouseAiming()
    {
        // take mouse input x and input y
        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");

        // add mouse input multiplied on turn speed to rotation value x, y, z for eulerAngle
        rotationX += inputY * turnSpeed;
        rotationY += inputX * turnSpeed;

        transform.eulerAngles = new Vector3(Mathf.Clamp(-rotationX, minViewAngle, maxViewAngle), rotationY, 0);

    }
    void Movement()
    {
        ////
    }

    private void Update()
    {
        MouseAiming();
    }
    private void FixedUpdate()
    {
        Movement();
    }




}

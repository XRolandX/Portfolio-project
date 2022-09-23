using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region MOUSE VIEW VARIABLES
    [SerializeField] private float turnSpeed = 1.2f;
    [SerializeField] private float minViewAngle = 80f;
    [SerializeField] private float maxViewAngle = 80f;

    [Tooltip("GameObject Rotation axis X value modificated by Mouse axis Y")] [SerializeField] private float rotationX;
    [Tooltip("GameObject Rotation axis Y value modificated by Mouse axis X")] [SerializeField] private float rotationY;
    #endregion

    [SerializeField] private float movementSpeed = 1;

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
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        float vertical = Input.GetAxis("Vertical") * movementSpeed;

        //transform.position += horizontal * Time.deltaTime * transform.right;
        //transform.position += vertical * Time.deltaTime * transform.forward;

        // or simplier

        transform.position += (horizontal * transform.right + vertical * transform.forward) * Time.deltaTime;
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

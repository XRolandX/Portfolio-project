using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    float speed = 6f;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            if(value >= 0)
            {
                speed = value;
            }
            else
            {
                throw new Exception("The speed value can`t be negative");
            }
            
        }
    }

    readonly float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private Vector3 Vector3DirectionWithInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // input 1 or -1
        float vertical = Input.GetAxisRaw("Vertical");

        return new Vector3(horizontal, 0f, vertical).normalized; // vectors x y z in format 1 or -1
    } 
    private float CharacterSmoothRotationOnTargetAngle()  // return targetAngle
    {
        //Profiler.BeginSample("My sample");

        float targetAngle = Mathf.Atan2(Vector3DirectionWithInput().x, Vector3DirectionWithInput().z) * Mathf.Rad2Deg + cam.eulerAngles.y;  //  atan2 returns a value beeween x and z in radiance. camera rotation axis y plus
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); // smooth damp angle take current transform euler angle axis y, target atan2 betwen x and z angle, ref to change turn_smooth_velocity value and smooth_turn_time to smootly change the angle
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        return targetAngle;
        //Profiler.EndSample();
    }
    private void CharacterMoveOnTargetAngleDirection()
    {
        Vector3 moveDirection = Quaternion.Euler(0f, CharacterSmoothRotationOnTargetAngle(), 0f) * Vector3.forward;
        controller.Move(Speed * Time.deltaTime * moveDirection.normalized);
    }

    void Update()
    {
        Vector3DirectionWithInput(); // return Vector3 direction with Input values .normalized


        if(Vector3DirectionWithInput().magnitude >= 0.1f) // if vectors are bigger than 0
        {
            CharacterSmoothRotationOnTargetAngle();

            CharacterMoveOnTargetAngleDirection();
        }
    }
}

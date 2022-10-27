using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRotation : MonoBehaviour
{
    public new Renderer renderer;
    private float turnSmoothVelocity;
    float angle;
    public float smoothTurnTime;

    //bool xRotation = false;
    void Start()
    {
        
    }

    private void Update()
    {
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.x, 180f, ref turnSmoothVelocity, smoothTurnTime); // smooth damp angle take current transform euler angle axis y, target atan2 betwen x and z angle, ref to change turn_smooth_velocity value and smooth_turn_time to smootly change the angle
        transform.RotateAround(renderer.bounds.center, Vector3.right, angle);

    }



}

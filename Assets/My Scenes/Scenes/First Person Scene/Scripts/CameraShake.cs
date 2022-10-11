using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosOfCamera = default;
    public float shakeFrequency = default;

    void CamShake()
    {
        transform.position = originalPosOfCamera + Random.insideUnitSphere * shakeFrequency;
    }
    void StopCamShake()
    {
        transform.position = originalPosOfCamera;
    }
    void Start()
    {
        originalPosOfCamera = transform.position;
    }

}

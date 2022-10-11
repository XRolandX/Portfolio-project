using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Animator animator;
    private bool isScoped = false;

    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public Camera mainCamera;

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("isScoped", isScoped);
            if (isScoped) StartCoroutine(nameof(OnScoped));
            else StartCoroutine(nameof(UnScoped));
        }
        
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(0.15f);
        weaponCamera.SetActive(false);
        scopeOverlay.SetActive(true);
        mainCamera.fieldOfView = 15f;
        
    }
    IEnumerator UnScoped()
    {
        yield return new WaitForSeconds(0f);
        weaponCamera.SetActive(true);
        scopeOverlay.SetActive(false);
        mainCamera.fieldOfView = 60f;
    }
}

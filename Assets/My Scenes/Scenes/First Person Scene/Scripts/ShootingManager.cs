using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] AudioSource shoot; // shoot sound
    
    #region C A M E R A    S H A K E    V A R I A B L E S
    [SerializeField] Transform mainCameraTransform;
    [SerializeField] float scopeShakeFrequency;
    [SerializeField] float unScopeShakeFrequency;
    Vector3 originalPosOfMainCamera;
    Vector3 currentPosOfCamera;
    float shakeFrequency;
    bool thisIsAShot = false;
    readonly float shakeTime = .01f;
    #endregion

    #region A P P L E    F A L L    V A R I A B L E S
    [SerializeField] float blowWaveRadius = default;
    [SerializeField] LayerMask apple;
    Collider[] apples;
    #endregion

    #region S C O P E    V A R I A B L E S
    [SerializeField] Animator mainCamAnim; // scope animation
    [SerializeField] GameObject weaponCamera;
    [SerializeField] GameObject scopeOverlay;
    [SerializeField] Camera mainCamera;
    bool isScoped = false;
    readonly float unscopedFieldOfView = 60f;
    readonly float scopedFieldOfView = 15f;
    #endregion


    private void BlowWave()
    {
        apples = Physics.OverlapSphere(transform.position, blowWaveRadius, apple);

        foreach(Collider apple in apples)
        {
            apple.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void CameraShake()
    {
        mainCameraTransform.localPosition += Random.insideUnitSphere * shakeFrequency;
    }
    IEnumerator ShakeTimeControl()
    {
        yield return new WaitForSeconds(shakeTime);
        thisIsAShot = false;
    }
    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
        shakeFrequency = scopeShakeFrequency;
        mainCamera.fieldOfView = scopedFieldOfView;
    }
    void UnScoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        shakeFrequency = unScopeShakeFrequency;
        mainCamera.fieldOfView = unscopedFieldOfView;
    }
    
    void Start()
    {
        shakeFrequency = unScopeShakeFrequency; // camera shake
        shoot = GetComponent<AudioSource>(); // audio for shooting
    }
    void LateUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            thisIsAShot = true;
            shoot.Play();

            StartCoroutine(ShakeTimeControl()); // camera shake time control

            BlowWave(); // apple fall initiation
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetMouseButtonDown(1))
        {
            isScoped = !isScoped;
            mainCamAnim.SetBool("isScoped", isScoped);
            if (isScoped) StartCoroutine(nameof(OnScoped));
            else UnScoped();
        }

        if (thisIsAShot)
        {
            CameraShake(); // permanent camera shake if thisIsAShot == true
        }  // camera shake inside




    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] AudioSource shoot; // shoot sound
    [SerializeField] GameObject bulletHolePrefab;
    [SerializeField] TextMeshProUGUI scopeText;
    
    #region C A M E R A    S H A K E    V A R I A B L E S
    [SerializeField] Transform mainCameraTransform;


    [SerializeField] float shakeFrequency;
    public float ShakeFrequency{ get { return shakeFrequency; } set { shakeFrequency = value; }} // if i need access to shakeFrequency from other classes

    readonly float unScopedShakeFrequency = 0.01f;
    readonly float scopedShakeFrequency = 0.2f;
    readonly float scoped2xShakeFrequency = 0.5f;

    [SerializeField] float shakeTime;
    readonly float unscopedShakeTime = .01f;
    readonly float scopedShakeTime = .005f;
    readonly float scoped2xShakeTime = .001f;

    [SerializeField] bool thisIsAShot = false;
    #endregion

    #region A P P L E    F A L L    V A R I A B L E S
    [SerializeField] float blowWaveRadius = default;
    [SerializeField] LayerMask apple;
    Collider[] apples;
    #endregion

    #region S C O P E    V A R I A B L E S
    private MouseLook mouseLookScript;
    [SerializeField] Animator mainCamAnim; // scope animation
    [SerializeField] GameObject weaponCamera;
    [SerializeField] GameObject scopeOverlay;
    [SerializeField] Camera mainCamera;
    private readonly float unscopedFieldOfView = 60f;
    private readonly float scopedFieldOfView = 15f;
    private readonly float scoped2xFieldOfView = 5f;

    private int scopeClick = 0;
    #endregion


    #region P C   C O N T R O L
    private void PCControl()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Scope();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        

#endif
    }
    #endregion

    #region U I  B U T T O N S   C O N T R O L
    public void Shoot()
    {
        thisIsAShot = true;
        shoot.Play();

        StartCoroutine(ShakeTimeControl()); // camera shake time control

        BlowWave();
        RaycastShot();
    }
    public void Scope()
    {
        scopeClick++;

        switch (scopeClick)
        {
            case 1:
                mainCamAnim.SetBool("isScoped", true);
                StartCoroutine(nameof(OnScoped));
                scopeText.text = "2x";
                break;

            case 2:
                mainCamera.fieldOfView = scoped2xFieldOfView;
                mouseLookScript.MouseSensitivity = mouseLookScript.scope2xSensitivity;
                ShakeFrequency = scoped2xShakeFrequency; // shakeFrequency property
                shakeTime = scoped2xShakeTime;
                scopeText.text = "4x";
                break;

            case 3:
                UnScoped();
                scopeClick = 0;
                break;
        }

    }
    #endregion

    #region S C O P E D - U N S C O P E D
    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
        shakeTime = scopedShakeTime;
        ShakeFrequency = scopedShakeFrequency; // shakeFrequency property
        mainCamera.fieldOfView = scopedFieldOfView;
        mouseLookScript.MouseSensitivity = mouseLookScript.scopeSensitivity; // mouse look script variables from main camera game object
        mouseLookScript.lookJoystick.DeadZone = 0f;
    }
    void UnScoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        shakeTime = unscopedShakeTime;
        ShakeFrequency = unScopedShakeFrequency; // shakeFrequency property
        mainCamera.fieldOfView = unscopedFieldOfView;
        mouseLookScript.MouseSensitivity = mouseLookScript.unscopeSensitivity; // mouse look script variables from main camera game object
        mouseLookScript.lookJoystick.DeadZone = 0.05f;
        mainCamAnim.SetBool("isScoped", false);
    }
    #endregion

    #region C A M E R A   S H A K E
    private void CameraShake()
    {
        if (thisIsAShot)
        {
            mainCameraTransform.localPosition += Random.insideUnitSphere * shakeFrequency;
        }
        
    }
    IEnumerator ShakeTimeControl()
    {
        yield return new WaitForSeconds(shakeTime);
        thisIsAShot = false;
    }
    #endregion

    #region B U L L E T    H O L E S
    private void RaycastShot()
    {
        // Get the center of the screen
        Vector3 screenCenter = new(Screen.width / 2, Screen.height / 2, 0); 

        // Raycast to detect the hit point
        if (Physics.Raycast(Camera.main.ScreenPointToRay(screenCenter), out RaycastHit hit, 500f))
        {
            // Spawn bullet hole prefab at the hit point
            SpawnBulletHole(hit.point, hit.normal);
        }
    }

    private void SpawnBulletHole(Vector3 position, Vector3 normal)
    {
        // Instantiate the bullet hole prefab
        GameObject bulletHole = Instantiate(bulletHolePrefab, position, Quaternion.identity);

        // Rotate the bullet hole to align with the surface normal
        bulletHole.transform.LookAt(position - normal);

        // Move the bullet hole slightly away from the surface to avoid z-fighting
        bulletHole.transform.position += normal * 0.0001f;

        // Destroy the bullet hole after a certain time (optional)
        Destroy(bulletHole, 30f);
    }
    #endregion

    #region A P P L E   F A L L I N G
    private void BlowWave()
    {
#pragma warning disable UNT0028 // Use non-allocating physics APIs
        apples = Physics.OverlapSphere(transform.position, blowWaveRadius, apple);
#pragma warning restore UNT0028 // Use non-allocating physics APIs

        foreach (Collider apple in apples)
        {
            apple.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    #endregion

    void Start()
    {
        shakeTime = unscopedShakeTime; // default shake time
        shakeFrequency = unScopedShakeFrequency; // default camera shake
        shoot = GetComponent<AudioSource>(); // audio for shooting
        mouseLookScript = mainCamera.gameObject.GetComponent<MouseLook>(); // mouse look script from main camera game object
    }

    void LateUpdate()
    {
        PCControl();

        CameraShake();
    }

}
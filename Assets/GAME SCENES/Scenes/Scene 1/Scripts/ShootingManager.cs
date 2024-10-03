using System.Collections;
using TMPro;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] GameObject bulletHolePrefab;
    [SerializeField] TextMeshProUGUI scopeText;
    [SerializeField] AudioSource shoot;
    
    private PlayerControls playerControls;

    #region C A M E R A    S H A K E    V A R I A B L E S
    [SerializeField] Transform mainCameraTransform;

    #region Shake frequency variables
    [SerializeField] float shakeFrequency;
    public float ShakeFrequency { get { return shakeFrequency; } set { shakeFrequency = value; } }

    readonly float unScopedShakeFrequency = 0.01f;
    readonly float scopedShakeFrequency = 0.15f;
    readonly float scoped2xShakeFrequency = 0.3f;
    #endregion

    #region Shake time variables
    [SerializeField] float shakeTime;
    readonly float unscopedShakeTime = .01f;
    readonly float scopedShakeTime = .025f;
    readonly float scoped2xShakeTime = .05f;
    #endregion

    [SerializeField] bool thisIsAShot = false;
    #endregion

    #region A P P L E S   F A L L I N G   V A R I A B L E S
    [SerializeField] float blowWaveRadius = default;
    [SerializeField] LayerMask appleMask;
    Collider[] applesCollider;
    #endregion

    #region S C O P E    V A R I A B L E S
    private MouseLook mouseLookScript;
    [SerializeField] Animator mainCamAnim;
    [SerializeField] GameObject weaponCamera;
    [SerializeField] GameObject scopeOverlay;
    [SerializeField] Camera mainCamera;
    private readonly float unscopedFieldOfView = 60f;
    private readonly float scopedFieldOfView = 15f;
    private readonly float scoped2xFieldOfView = 5f;

    private int scopeClick = 0;
    #endregion


    #region S H O O T   A N D   S C O P E
    public void Shoot()
    {
        thisIsAShot = true;
        StartCoroutine(ShakeTimeControl());

        shoot.Play();
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
                ShakeFrequency = scoped2xShakeFrequency;
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
        ShakeFrequency = scopedShakeFrequency;
        mainCamera.fieldOfView = scopedFieldOfView;
        mouseLookScript.MouseSensitivity = mouseLookScript.scopeSensitivity;
        mouseLookScript.lookJoystick.DeadZone = 0f;
    }
    void UnScoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        shakeTime = unscopedShakeTime;
        ShakeFrequency = unScopedShakeFrequency;
        mainCamera.fieldOfView = unscopedFieldOfView;
        mouseLookScript.MouseSensitivity = mouseLookScript.unscopeSensitivity;
        mouseLookScript.lookJoystick.DeadZone = 0.05f;
        mainCamAnim.SetBool("isScoped", false);
    }
    #endregion

    #region C A M E R A   S H A K E
    IEnumerator ShakeTimeControl()
    {
        yield return new WaitForSeconds(shakeTime);
        thisIsAShot = false;
    }
    private void CameraShake()
    {
        if (thisIsAShot)
        {
            mainCameraTransform.localPosition += Random.insideUnitSphere * shakeFrequency;
        }

    }
    #endregion

    #region B U L L E T    H O L E S
    private void RaycastShot()
    {
        Vector3 screenCenter = new(Screen.width / 2, Screen.height / 2, 0);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(screenCenter), out RaycastHit hit, 500f))
        {
            SpawnBulletHole(hit.point, hit.normal);
        }
    }
    private void SpawnBulletHole(Vector3 hitPoint, Vector3 hitNormal)
    {
        Quaternion randomRotation = Quaternion.LookRotation(hitNormal) * Quaternion.Euler(0, 0, Random.Range(0f, 360f));


        GameObject bulletHole = Instantiate(bulletHolePrefab, hitPoint, randomRotation);

        bulletHole.transform.LookAt(hitPoint - hitNormal);

        bulletHole.transform.position += hitNormal * 0.00001f;

        Destroy(bulletHole, 30f);
    }
    #endregion

    #region A P P L E S   F A L L I N G   T R I G G E R I N G
    private void BlowWave()
    {
        applesCollider = new Collider[10];

        Physics.OverlapSphereNonAlloc(transform.position, blowWaveRadius, applesCollider, appleMask);

        for (int i = 0; i < applesCollider.Length; i++)
        {
            if(applesCollider[i] != null)
            {
                applesCollider[i].GetComponent<Rigidbody>().isKinematic = false;
                applesCollider[i].GetComponent<Rigidbody>().useGravity = true;
                applesCollider[i].gameObject.layer = 3;
            }
        }
    }
    #endregion

    void Awake()
    {
        mouseLookScript = mainCamera.gameObject.GetComponent<MouseLook>();
        shoot = GetComponent<AudioSource>();
        shakeFrequency = unScopedShakeFrequency;
        shakeTime = unscopedShakeTime;
    }

    void LateUpdate()
    {
        CameraShake();
    }

    private void OnEnable()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
        playerControls.Player.MouseClick.performed += ctx => Shoot();
        playerControls.Player.RightMouseClick.performed += ctx => Scope();
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
    }
}

using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AndroidControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected float moveSpeed = 25f;
    [SerializeField] protected float sensitivity = 50f;
    [SerializeField] protected float lookPitch = 0f;
    [SerializeField] private Joystick moveJoystick;
    [SerializeField] private Joystick lookJoystick;
    [SerializeField] private Button throwButton;
    [SerializeField] private PlayerController player;

    private EntityManager entityManager;
    private Entity mouseInputEntity;


    private void Awake()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EnsureMouseInputEntity();
    }
    private void Update()
    {
        MoveWithJoystick();
        LookWithJoystick();
    }

    private void MoveWithJoystick()
    {

        if (moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
        {
            float x = moveJoystick.Horizontal;
            float z = moveJoystick.Vertical;
            Vector3 move = moveSpeed * Time.deltaTime * new Vector3(x, 0f, z);
            player.transform.Translate(move, Space.Self);
        }

    }
    private void LookWithJoystick()
    {
        if (lookJoystick.Horizontal != 0 || lookJoystick.Vertical != 0)
        {
            float mouseX = lookJoystick.Horizontal * Time.deltaTime * sensitivity;
            float mouseY = lookJoystick.Vertical * Time.deltaTime * sensitivity;

            player.transform.Rotate(new Vector3(-mouseY, mouseX, 0.0f));

            lookPitch -= mouseY;
            lookPitch = Mathf.Clamp(lookPitch, -85f, 85f);
            player.transform.localEulerAngles = new Vector3(lookPitch, player.transform.localEulerAngles.y, 0f);
        }
    }


    private void EnsureMouseInputEntity()
    {
        var mouseInputQuery = entityManager.CreateEntityQuery(typeof(MouseInput));
        if (mouseInputQuery.IsEmptyIgnoreFilter)
        {
            mouseInputEntity = entityManager.CreateEntity(typeof(MouseInput));
        }
        else
        {
            mouseInputEntity = mouseInputQuery.GetSingletonEntity();
        }
    }
    public void ThrowButton(bool buttonPerformed)
    {
        entityManager
            .SetComponentData(mouseInputEntity, new MouseInput { LeftClickPerformed = buttonPerformed });
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
    }
    public void OnPointerUp(PointerEventData eventData)
    {
    }
}

using UnityEngine;

public class TMProFacePlayer : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject textMeshHolder;

    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }
    void Update()
    {
        FacePlayer();
    }

    void FacePlayer()
    {
        if (playerTransform != null)
        {
            Vector3 directionToPlayer = transform.position - playerTransform.position;
            directionToPlayer.y = 0;
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer);

            Debug.DrawLine(transform.position, playerTransform.position, Color.red);

            transform.rotation = rotation;

            textMeshHolder.transform.localRotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }
}
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    [Header("Settings")]
    public Vector3 followOffset;
  
    public float smoothSpeed = 0.2f;

    [Header("Components")]
    public Transform playerTranform;

    float zPosition;
    Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        zPosition = transform.position.z;
    }
    public void Update()
    {
        Vector3 targetPosition = playerTranform.position + followOffset;
        targetPosition.z = zPosition;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothSpeed);
    }
}

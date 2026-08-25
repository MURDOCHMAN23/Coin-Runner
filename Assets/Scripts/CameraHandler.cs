using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform player;

    private Vector3 positionOffset;
    private Quaternion rotationOffset;


    private void Start()
    {
        if (player == null)
            return;

        positionOffset = transform.position - player.position;
        rotationOffset = transform.rotation;
    }


    private void LateUpdate()
    {
        if (player == null)
            return;

        UpdatePosition();
    }


    private void UpdatePosition()
    {
        Vector3 position = player.position + positionOffset;

        position.x = 0f;

        transform.position = position;
        transform.rotation = rotationOffset;
    }
}
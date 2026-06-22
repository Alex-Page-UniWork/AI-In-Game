using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerMovement; // your PlayerMovement object
    public Vector3 offset = new Vector3(0, 15, 0);

    void Update()
    {
        if (playerMovement == null) return;

        transform.position = playerMovement.position + offset;
    }
}

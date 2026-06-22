using UnityEngine;

public class CameraFollowScene1 : MonoBehaviour
{
    public Transform player;

    public Vector3 offset = new Vector3(0, 3, -6);

    public float mouseSensitivity = 3f;
    public float distance = 6f;

    private float yaw;
    private float pitch = 10f;

    public float minPitch = -20f;
    public float maxPitch = 60f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        // Mouse input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Rotation around player
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 direction = new Vector3(0, 0, -distance);
        Vector3 position = player.position + rotation * direction;

        transform.position = position;
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}

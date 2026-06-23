using UnityEngine;

public class MouseLook : MonoBehaviour
{
      public Transform player;

    public float distance = 6f;
    public float height = 2f;
    public float sensitivity = 3f;

    public float minPitch = -80f;
    public float maxPitch = 80f;

    public float heightMultiplier = 1f;

    float yaw;
    float pitch = 10f;

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

        // mouse input
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // scale-aware offset
        float playerScale = player.lossyScale.y;

        float scaledHeight = height * playerScale * heightMultiplier;
        float scaledDistance = distance * playerScale;

        Vector3 offset = new Vector3(0, scaledHeight, -scaledDistance);

        transform.position = player.position + rotation * offset;

        transform.LookAt(player.position + Vector3.up * (1.5f * playerScale));
    }
}
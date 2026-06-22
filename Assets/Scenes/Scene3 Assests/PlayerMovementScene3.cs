using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementScene3 : MonoBehaviour
{
    public float speed = 20f;
    public Transform cameraTransform;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * z + right * x;

        controller.Move(move * speed * Time.deltaTime);
    }
}
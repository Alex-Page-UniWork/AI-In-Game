using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform;

    public float noiseRadius = 6f;
    public float noiseInterval = 0.5f;

    private float noiseTimer;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Move();
        HandleNoise();
    }

    void Move()
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

    void HandleNoise()
    {
        noiseTimer += Time.deltaTime;

        if (noiseTimer >= noiseInterval)
        {
            MakeNoise();
            noiseTimer = 0f;
        }
    }

    void MakeNoise()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, noiseRadius);

        foreach (Collider hit in hits)
        {
            EnemyAI enemy = hit.GetComponent<EnemyAI>();

            if (enemy != null)
            {
                enemy.HearNoise(transform.position);
            }
        }
    }
}
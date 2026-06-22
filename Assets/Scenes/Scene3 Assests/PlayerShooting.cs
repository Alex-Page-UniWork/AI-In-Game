using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Camera cam;
    public float damage = 1f;
    public float fireRate = 0.2f;

    float nextFire;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time < nextFire) return;

        nextFire = Time.time + fireRate;

        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            EnemyHealth enemy = hit.transform.root.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}

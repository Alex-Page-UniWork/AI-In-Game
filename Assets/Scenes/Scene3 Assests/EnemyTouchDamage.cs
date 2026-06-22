using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour
{
    public float damageInterval = 1f;
    private float timer;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        timer += Time.deltaTime;

        if (timer >= damageInterval)
        {
            timer = 0f;

            if (GameManagerScene3.instance != null)
            {
                GameManagerScene3.instance.TakeDamage(1);
            }
        }
    }
}
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 3f;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (GameManagerScene3.instance != null)
        {
            GameManagerScene3.instance.EnemyKilled();
        }

        Destroy(gameObject);
    }
}
using UnityEngine;

public class EnemySpawnerScene3 : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public float spawnInterval = 3f;
    public int maxEnemies = 10;

    public float enemyScale = 2f;

    private float timer;

    void Update()
    {
        // 🚨 STOP SPAWNING IF GAME ENDS OR IS WON
        if (GameManagerScene3.instance != null &&
            (GameManagerScene3.instance.gameWon || GameManagerScene3.instance.gameOver))
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (currentEnemies < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;

        int index = Random.Range(0, spawnPoints.Length);

        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPoints[index].position,
            Quaternion.identity
        );

        enemy.transform.localScale = Vector3.one * enemyScale;
    }
}
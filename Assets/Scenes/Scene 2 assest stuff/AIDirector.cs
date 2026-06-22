using UnityEngine;

public class AIDirector : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public float spawnInterval = 5f;
    public float minSpawnInterval = 1.5f;

    private float timer;
    private float survivalTime;

 void Update()
{
    if (GameManager.instance != null &&
        (GameManager.instance.gameOver || GameManager.instance.gameWon))
        return;

    survivalTime += Time.deltaTime;
    timer += Time.deltaTime;

    float difficultyFactor = survivalTime / 30f;
    spawnInterval = Mathf.Lerp(5f, minSpawnInterval, difficultyFactor);

    if (timer >= spawnInterval)
    {
        SpawnEnemy();
        timer = 0f;
    }
}

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        int index = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);
    }
}
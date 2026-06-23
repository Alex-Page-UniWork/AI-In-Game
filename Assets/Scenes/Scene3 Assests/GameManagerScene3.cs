using UnityEngine;

public class GameManagerScene3 : MonoBehaviour
{
    public static GameManagerScene3 instance;

    [Header("Player")]
    public int playerLives = 5;

    [Header("Win Condition")]
    public int killTarget = 5;

    [Header("Runtime")]
    public int enemiesKilled = 0;
    public bool gameWon = false;
    public bool gameOver = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (gameWon || gameOver) return;
    }

    public void TakeDamage(int dmg)
    {
        if (gameWon || gameOver) return;

        playerLives -= dmg;

        if (playerLives <= 0)
        {
            gameOver = true;
            Debug.Log("GAME OVER");
        }
    }

    public void EnemyKilled()
    {
        if (gameWon || gameOver) return;

        enemiesKilled++;

        if (enemiesKilled >= killTarget)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        gameWon = true;

        Debug.Log("YOU WIN!");

        // destroy enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        
        Time.timeScale = 0f;
    }
}
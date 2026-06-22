using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float survivalTime;
    public float winTime = 40f;

    public bool gameOver;
    public bool gameWon;

    public int playerLives = 3;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (gameOver || gameWon) return;

        survivalTime += Time.deltaTime;

        if (survivalTime >= winTime)
        {
            gameWon = true;
            Debug.Log("YOU WIN!");
        }
    }

    public void TakeDamage(int damage)
    {
        if (gameOver || gameWon) return;

        playerLives -= damage;

        if (playerLives <= 0)
        {
            gameOver = true;
            Debug.Log("GAME OVER");
        }
    }
}
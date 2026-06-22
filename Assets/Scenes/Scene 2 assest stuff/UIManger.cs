using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hudText;

    void Update()
    {
        if (GameManager.instance == null) return;

        string text =
            "Lives: " + GameManager.instance.playerLives + "\n" +
            "Time: " + Mathf.FloorToInt(GameManager.instance.survivalTime) + " / 40";

        if (GameManager.instance.gameWon)
        {
            text += "\nYOU WIN!";
        }
        else if (GameManager.instance.gameOver)
        {
            text += "\nGAME OVER";
        }

        hudText.text = text;
    }
}

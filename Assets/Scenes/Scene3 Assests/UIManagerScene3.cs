using UnityEngine;
using UnityEngine.UI;

public class UIManagerScene3 : MonoBehaviour
{
    public Text hudText;

    void Start()
    {
        Debug.Log("UIManager started");

        if (hudText == null)
        {
            Debug.LogError("HUD TEXT NOT ASSIGNED IN INSPECTOR");
        }
    }

    void Update()
    {
        if (GameManagerScene3.instance == null)
        {
            Debug.LogError("GameManagerScene3 instance is NULL");
            return;
        }

        Debug.Log("UI Updating | Lives: " + GameManagerScene3.instance.playerLives +
                  " Kills: " + GameManagerScene3.instance.enemiesKilled);

        string text =
            "Lives: " + GameManagerScene3.instance.playerLives + "\n" +
            "Kills: " + GameManagerScene3.instance.enemiesKilled + " / " + GameManagerScene3.instance.killTarget;

        if (GameManagerScene3.instance.gameWon)
        {
            Debug.Log("WIN CONDITION TRIGGERED");
            text += "\nYOU WIN!";
        }

        if (GameManagerScene3.instance.gameOver)
        {
            Debug.Log("GAME OVER TRIGGERED");
            text += "\nGAME OVER";
        }

        if (hudText != null)
        {
            hudText.text = text;
        }
        else
        {
            Debug.LogError("hudText is NULL - cannot update UI");
        }
    }
}

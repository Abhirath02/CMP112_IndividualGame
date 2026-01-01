using UnityEngine;

public class GameWonScreen : MonoBehaviour
{
    public GameObject WinUI;
    private bool gameWon = false;

    void Update()
    {
        if (gameWon)
        {
            return;
        }

        // If no boss exists, boss is dead
        if (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        gameWon = true;
        Time.timeScale = 0f;
        WinUI.SetActive(true);
    }
}
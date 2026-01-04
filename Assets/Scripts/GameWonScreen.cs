using UnityEngine;

public class GameWonScreen : MonoBehaviour
{
    public GameObject WinUI;
    private bool gameWon = false;

    void Update()
    {
        if (gameWon) //return if gameWon is false
        {
            return;
        }

        // If no boss exists, boss is dead
        if (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            WinGame(); // displays the game won screen
        }
    }

    void WinGame() // function to display the game won screen
    {
        gameWon = true;
        Time.timeScale = 0f;
        WinUI.SetActive(true);
    }
}
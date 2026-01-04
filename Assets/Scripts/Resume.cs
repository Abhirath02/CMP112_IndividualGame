using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseUI;

    private bool isPaused = false;

    private void Start()
    {
        pauseUI.SetActive(false);
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) // pauses and resumes the game when escape is pressed
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame() //function to pause the game
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f; // freezers the game
        isPaused = true;
    }

    public void ResumeGame() // function to resume the game if paused
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f; // resumes the game
        isPaused = false;
    }
}
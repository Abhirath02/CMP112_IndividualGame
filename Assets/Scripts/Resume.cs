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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
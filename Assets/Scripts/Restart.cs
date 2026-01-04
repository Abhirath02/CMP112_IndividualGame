using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void LoadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); //loads the game scene to restart the game
        Time.timeScale = 1.0f;
    }
}
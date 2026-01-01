using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        Debug.Log("Play button clicked");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }


    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
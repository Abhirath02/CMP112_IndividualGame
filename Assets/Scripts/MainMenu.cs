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
        SceneManager.LoadScene(1);// takes game from main menu to the game
    }


    public void ExitGame()
    {
        Debug.Log(0);
        Application.Quit(); // exits the game
    }
}
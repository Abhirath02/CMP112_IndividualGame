using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);// takes back to the first scene of the main menu
    }
}
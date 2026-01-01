using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameObject FailUI;


    public void TriggerGameOver()
    {

        Time.timeScale = 0;  // pause the game
        if (FailUI != null)
            FailUI.SetActive(true);
    }

}
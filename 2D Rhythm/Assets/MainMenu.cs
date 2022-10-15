using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playPress()
    {
        SceneManager.LoadScene("Level");
    }

    public void quitGame()
    {
        Debug.Log("Quit the game.");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private Camera mainCam;

    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void playPress()
    {
        //SceneManager.LoadScene("Level");
        mainCam.GetComponent<Animator>().SetTrigger("Play");
    }

    public void quitGame()
    {
        Debug.Log("Quit the game.");
        Application.Quit();
    }
}

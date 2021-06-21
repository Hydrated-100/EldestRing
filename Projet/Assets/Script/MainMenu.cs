using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitButton()
    {
        Debug.Log("Game Quitted");
        Application.Quit();
    }

    public void SoloGame()
    {
        SceneManager.LoadScene("Niveaux");
    }

    public void Multi()
    {
        SceneManager.LoadScene("Niveaux");
    }
}

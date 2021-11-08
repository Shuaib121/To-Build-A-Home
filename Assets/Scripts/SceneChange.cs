using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Game()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Story()
    {
        SceneManager.LoadScene("Story");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

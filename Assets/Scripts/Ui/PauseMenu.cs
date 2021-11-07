using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioScript audioSource;
    public GameObject pauseMenu;
    public List<GameObject> hideUiItems;

    public void Reset()
    {
        audioSource.PlaySound(2);
        pauseMenu.SetActive(false);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        audioSource.PlaySound(2);
        foreach(GameObject uiItem in hideUiItems)
        {
            uiItem.SetActive(false);
        }

        pauseMenu.SetActive(true);
    }

    public void Continue()
    {
        audioSource.PlaySound(2);
        foreach (GameObject uiItem in hideUiItems)
        {
            uiItem.SetActive(true);
        }

        pauseMenu.SetActive(false);
    }

    public void Close()
    {
        Application.Quit();
    }
}

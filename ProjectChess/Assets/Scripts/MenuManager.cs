using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    void Start()
    {
        MainMenuButton();
    }

    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(""); // Load Main level
    }

    public void MainMenuButton()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void OptionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

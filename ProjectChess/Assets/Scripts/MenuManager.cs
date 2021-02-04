using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    string clickSound = "Click";

    public GameObject mainMenu;
    public GameObject optionsMenu;
    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No audio manager.");
        }

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

    public void OnMouseDown()
    {
        audioManager.PlaySound(clickSound);
    }
}

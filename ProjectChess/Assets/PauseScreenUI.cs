using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenUI : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("APP QUIT");
        Application.Quit();
    }

    public void Continue()
    {
        Global.GameManager.pauseMenu.SetActive(false);
    }
}
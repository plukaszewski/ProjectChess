using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject gameOver;
    [SerializeField]
    public GameObject gameWon;
    [SerializeField]
    public GameObject pauseMenu;

    public delegate void PauseMenuCallback(bool active);
    public PauseMenuCallback onTogglePauseMenu;

    public LevelManager Level;
    public PlayerController Player;

    //private bool AreAllEnemiesInCurrentRoomDefeted()
    //{
    //    if (Level.CurrentRoom.GetComponentsInChildren<Enemy>().Length > 0)
    //    {
    //        return false;
    //    }
    //    return true;
    //}

    //private void _CheckForEnemies()
    //{
    //    if (AreAllEnemiesInCurrentRoomDefeted())
    //    {
    //        Level.CurrentRoom.EnableEntrances(true);
    //    }
    //}

    //private void CheckForEnemies(Enemy Enemy)
    //{
    //    Global.GlobalObject.DelayFunction(_CheckForEnemies);
    //}

    private void Awake()
    {
        Global.GameManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = Instantiate(Player, Level.Rooms[0].transform.position, new Quaternion());
        Global.GridManager = Level.Rooms[0].GetComponent<GridManager>();
        //Player.Movement.OnCapture.AddListener(CheckForEnemies);
        //Level.OnChangeRoom.AddListener(CheckForEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        onTogglePauseMenu.Invoke(pauseMenu.activeSelf);
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
        gameOver.SetActive(true);
    }

    public void EndWin()
    {
        Debug.Log("Game Ended.");
        gameWon.SetActive(true);
    }
}

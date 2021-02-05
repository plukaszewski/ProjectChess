﻿using System.Collections;
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
    public AIController AIController;
    public bool IsPlayerTurn;

    private void OnRoomChange()
    {
        ChangeTurn(Global.GameManager.AIController.AnyEnemiesLeft());
    }

    private void ChangeTurn(bool PlayerTurn)
    {
        IsPlayerTurn = !PlayerTurn;
        ChangeTurn();
    }

    private void ChangeTurn()
    {
        IsPlayerTurn = !IsPlayerTurn;

        if (IsPlayerTurn)
        {
            Player.Movement.Initialize();
        }
        else
        {
            if(AIController.AnyEnemiesLeft())
            {
                AIController.MakeTurn();
            }
        }
    }

    private void Awake()
    {
        Global.GameManager = this;
    }

    private void Setup()
    {
        Player.Movement.OnMove.AddListener(ChangeTurn);
        AIController.TurnMade.AddListener(ChangeTurn);
        Level.OnChangeRoom.AddListener(OnRoomChange);
        //ChangeTurn(true);
    }

    public void Die()
    {
        Destroy(Player.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = Instantiate(Player, Level.Rooms[0].transform.position, new Quaternion());
        Global.GridManager = Level.Rooms[0].GetComponent<GridManager>();
        Global.GlobalObject.DelayFunction(Setup);
        
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

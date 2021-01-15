using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public LevelManager Level;
    public PlayerController Player;

    private bool AreAllEnemiesInCurrentRoomDefeted()
    {
        if (Level.CurrentRoom.GetComponentsInChildren<Enemy>().Length > 0)
        {
            return false;
        }
        return true;
    }

    private void _CheckForEnemies()
    {
        if (AreAllEnemiesInCurrentRoomDefeted())
        {
            Level.CurrentRoom.EnableEntrances(true);
        }
    }

    private void CheckForEnemies()
    {
        Global.GlobalObject.DelayFunction(_CheckForEnemies);
    }

    private void Awake()
    {
        Global.GameManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = Instantiate(Player, Level.Rooms[0].transform.position, new Quaternion());
        Global.GridManager = Level.Rooms[0].GetComponent<GridManager>();
        Player.Movement.OnCapture.AddListener(CheckForEnemies);
        Level.OnChangeRoom.AddListener(CheckForEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

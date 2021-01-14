using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelManager Level;
    public PlayerController Player;

    private void Awake()
    {
        Global.GameManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = Instantiate(Player, Level.Rooms[0].transform.position, new Quaternion());
        Global.GridManager = Level.Rooms[0].GetComponent<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

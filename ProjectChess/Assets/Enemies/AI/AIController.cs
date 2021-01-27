using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public List<Enemy> Enemies = new List<Enemy>();
    public int MovesPerTurn = 2;
    private int Iterator = 0;

    private bool AnyEnemiesLeft()
    {
        if (Enemies.Count == 0)
        {
            return false;
        }
        return true;
    }

    private void GetEnemies()
    {
        Enemies.Clear();
        foreach (var Item in Global.GameManager.Level.CurrentRoom.GetComponentsInChildren<Enemy>())
        {
            Enemies.Add(Item);
        }
    }

    private void OnRoomChange()
    {
        GetEnemies();
        if(!AnyEnemiesLeft())
        {
            Global.GlobalObject.DelayFunction(Global.GameManager.Level.CurrentRoom.EnableEntrances);
        }
    }

    private void OnCapture(Enemy Enemy)
    {
        Enemies.Remove(Enemy);
        if(!AnyEnemiesLeft())
        {
            Global.GlobalObject.DelayFunction(Global.GameManager.Level.CurrentRoom.EnableEntrances);
        }
    }
    
    public void MakeTurn()
    {
        for(int i = 0; i < MovesPerTurn; i++)
        {
            if(Enemies.Count != 0)
            {
                if (Iterator >= Enemies.Count)
                {
                    Iterator = 0;
                }
                Move(Enemies[Iterator++]);
            }
        }
    }

    public void Move(Enemy Enemy)
    {
        Enemy.Move(CalculateNewPosition(Enemy) - Enemy.GridElement.GetPosition());
    }

    public Vector2Int CalculateNewPosition(Enemy Enemy)
    {
        var ValidPositions = Enemy.MovementPattern.GetIndicatorsPositions(Enemy.GridElement.GetPosition(), Enemy.GridIndicatorPrefabAttack.IndicatorTags);
        Vector2Int? ReturnValue = null;

        foreach (var Item in ValidPositions)
        {
            if(Global.GridManager.ContainsElementWithTag(Item, "Player"))
            {
                return Item;
            }
        }

        if (ReturnValue == null)
        {
            List<Vector2Int> TmpValidPositions = new List<Vector2Int>();

            foreach (var Item in ValidPositions)
            {
                foreach (var Element in Enemy.MovementPattern.GetIndicatorsPositions(Item, Enemy.GridIndicatorPrefabAttack.IndicatorTags))
                {
                    if (Global.GridManager.ContainsElementWithTag(Element, "Player"))
                    {
                        return Item;
                    }
                }
            }
        }

        if (ReturnValue == null)
        {
            ReturnValue = new Vector2Int(-100, -100);
        }

        return (Vector2Int)ReturnValue;
    }

    private void Awake()
    {
        Global.AIController = this;
    }

    private void Setup()
    {
        Global.GameManager.Player.Movement.OnCapture.AddListener(OnCapture);
        Global.GameManager.Level.OnChangeRoom.AddListener(OnRoomChange);
        OnRoomChange();
    }

    // Start is called before the first frame update
    void Start()
    {
        Global.GlobalObject.DelayFunction(Setup);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MakeTurn();
        }
    }
}

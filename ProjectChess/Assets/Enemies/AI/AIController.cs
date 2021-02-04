using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIController : MonoBehaviour
{
    public List<Enemy> Enemies = new List<Enemy>();
    public int MovesPerTurn = 2;
    public int MoveDelay = 2;
    private int Iterator = 0;
    private int MovesPerTurnIterator = 0;
    private Enemy CurrentEnemy;



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
        if(MovesPerTurnIterator++ < MovesPerTurn)
        {
            Global.GlobalObject.DelayFunction(MakeMove, MoveDelay);
        }
        else
        {
            //EndTurnCode
        }
    }

    private void MakeMove()
    {
        if (Enemies.Count != 0)
        {
            if (Iterator >= Enemies.Count)
            {
                Iterator = 0;
            }
            CurrentEnemy = Enemies[Iterator++];
            Move(CurrentEnemy);
        }

        MakeTurn();
    }

    public void Move(Enemy Enemy)
    {
        Enemy.Move(CalculateNewPosition(Enemy) - Enemy.GridElement.GetPosition());
    }

    private List<Vector2Int> TryFunction(UnityAction<List<Vector2Int>, List<Vector2Int>> Func, List<Vector2Int> AvailablePositions)
    {
        if (AvailablePositions.Count == 1)
        {
            return AvailablePositions;
        }

        List<Vector2Int> Tmp = new List<Vector2Int>();
        Func(AvailablePositions, Tmp);

        if (Tmp.Count == 0)
        {
            return AvailablePositions;
        }
        else
        {
            return Tmp;
        }
    }

    private void Capture(List<Vector2Int> AvailablePositions, List<Vector2Int> Out)
    {
        Out.Clear();

        foreach (var Item in AvailablePositions)
        {
            if (Global.GridManager.ContainsElementWithTag(Item, "Player"))
            {
                Out.Add(Item);
            }
        }
    }

    private void AttackNextMove(List<Vector2Int> AvailablePositions, List<Vector2Int> Out)
    {
        Out.Clear();

        foreach (var Item in AvailablePositions)
        {
            foreach (var Element in CurrentEnemy.MovementPattern.GetIndicatorsPositions(Item, CurrentEnemy.GridIndicatorPrefabAttack.IndicatorTags))
            {
                if (Global.GridManager.ContainsElementWithTag(Element, "Player"))
                {
                    Out.Add(Item);
                }
            }
        }
    }

    private void Defend(List<Vector2Int> AvailablePositions, List<Vector2Int> Out)
    {

    }

    private void MostDefendedPosition(List<Vector2Int> AvailablePositions, List<Vector2Int> Out)
    {

    }

    private void Nearest(List<Vector2Int> AvailablePositions, List<Vector2Int> Out)
    {

    }

    public Vector2Int CalculateNewPosition(Enemy Enemy)
    {
        var ValidPositions = Enemy.MovementPattern.GetIndicatorsPositions(Enemy.GridElement.GetPosition(), Enemy.GridIndicatorPrefabAttack.IndicatorTags);

        List<Vector2Int> NewPos = TryFunction(Capture, ValidPositions);

        NewPos = TryFunction(AttackNextMove, NewPos);

        if (NewPos.Count == 0)
        {
            return new Vector2Int(-100, -100);
        }

        return NewPos[0];

            //var ValidPositions = Enemy.MovementPattern.GetIndicatorsPositions(Enemy.GridElement.GetPosition(), Enemy.GridIndicatorPrefabAttack.IndicatorTags);
            //Vector2Int? ReturnValue = null;

        //foreach (var Item in ValidPositions)
        //{
        //    if(Global.GridManager.ContainsElementWithTag(Item, "Player"))
        //    {
        //        return Item;
        //    }
        //}

        //if (ReturnValue == null)
        //{
        //    List<Vector2Int> TmpValidPositions = new List<Vector2Int>();

        //    foreach (var Item in ValidPositions)
        //    {
        //        foreach (var Element in Enemy.MovementPattern.GetIndicatorsPositions(Item, Enemy.GridIndicatorPrefabAttack.IndicatorTags))
        //        {
        //            if (Global.GridManager.ContainsElementWithTag(Element, "Player"))
        //            {
        //                return Item;
        //            }
        //        }
        //    }
        //}

        //if (ReturnValue == null)
        //{
        //    ReturnValue = new Vector2Int(-100, -100);
        //}

        //return (Vector2Int)ReturnValue;
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

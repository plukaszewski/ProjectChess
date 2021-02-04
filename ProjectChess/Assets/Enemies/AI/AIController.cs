using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIController : MonoBehaviour
{
    public List<Enemy> Enemies = new List<Enemy>();
    public int MovesPerTurn = 2;
    public float MoveDelay = .5f;
    private int Iterator = 0;
    private int MovesPerTurnIterator = 0;
    private Enemy CurrentEnemy;
    private List<UnityAction<List<Vector2Int>, List<Vector2Int>>> AISteps = new List<UnityAction<List<Vector2Int>, List<Vector2Int>>>();

    public UnityEvent TurnMade;

    public bool AnyEnemiesLeft()
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

    private bool CompareEnemy(Enemy Enemy)
    {
        if (Global.GameManager.Player.Movement.LastCapturedEnemy == Enemy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCapture()
    {
        if(Enemies.FindIndex(CompareEnemy) < Iterator)
        {
            Iterator--;
        }

        Enemies.Remove(Global.GameManager.Player.Movement.LastCapturedEnemy);
        if(!AnyEnemiesLeft())
        {
            Global.GlobalObject.DelayFunction(Global.GameManager.Level.CurrentRoom.EnableEntrances);
        }
    }
    
    public void MakeTurn()
    {
        if(MovesPerTurnIterator++ < (MovesPerTurn < Enemies.Count ? MovesPerTurn : Enemies.Count) && AnyEnemiesLeft())
        {
            Global.GlobalObject.DelayFunction(MakeMove, MoveDelay);
        }
        else
        {
            MovesPerTurnIterator = 0;
            TurnMade.Invoke();
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
        var Tmp = CalculateNewPosition(Enemy);
        if (Tmp == null)
        {
            MovesPerTurnIterator--;
        }
        else
        {
            Enemy.Move((Vector2Int)CalculateNewPosition(Enemy) - Enemy.GridElement.GetPosition());
        }
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
                    break;
                }
            }
        }
    }

    private void Defend(List<Vector2Int> AvailablePositions, List<Vector2Int> Out)
    {
        Out.Clear();

        int TotalMax = 0;

        foreach (var Item in AvailablePositions)
        {
            int LocalMax = 0;
            foreach (var Element in CurrentEnemy.MovementPattern.GetIndicatorsPositions(Item, CurrentEnemy.GridIndicatorPrefabDefend.IndicatorTags))
            {
                if (Global.GridManager.ContainsElementWithTag(Element, "Enemy"))
                {
                    LocalMax++;
                }
            }
            if(LocalMax > TotalMax)
            {
                TotalMax = LocalMax;
                Out.Clear();
                Out.Add(Item);
            }
            else if(LocalMax == TotalMax)
            {
                Out.Add(Item);
            }
        }
    }

    private void MostDefendedPosition(List<Vector2Int> AvailablePositions, List<Vector2Int> Out)
    {
        Debug.Log("Not Implemented");
    }

    private void Nearest(List<Vector2Int> AvailablePositions, List<Vector2Int> Out)
    {
        Out.Clear();

        float TotalMin = 100;

        foreach (var Item in AvailablePositions)
        {
            float LocalMin = Vector2Int.Distance(Item, Global.GameManager.Player.GetComponent<GridElement>().GetPosition());

            if(LocalMin < TotalMin)
            {
                TotalMin = LocalMin;
                Out.Clear();
                Out.Add(Item);
            }
            else if (LocalMin == TotalMin)
            {
                Out.Add(Item);
            }
        }
    }

    public Vector2Int? CalculateNewPosition(Enemy Enemy)
    {
        var ValidPositions = Enemy.MovementPattern.GetIndicatorsPositions(Enemy.GridElement.GetPosition(), Enemy.GridIndicatorPrefabAttack.IndicatorTags);

        foreach (var Item in AISteps)
        {
            ValidPositions = TryFunction(Item, ValidPositions);
        }

        if(ValidPositions.Count == 0)
        {
            return null;
        }

        return ValidPositions[0];

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
        
        AISteps.Add(Capture);
        AISteps.Add(AttackNextMove);
        AISteps.Add(Defend);
        AISteps.Add(Nearest);
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
        Global.GameManager.AIController = this;

    }

    // Update is called once per frame
    void Update()
    {

    }
}

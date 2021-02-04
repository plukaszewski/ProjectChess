using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IOnMouseEnter, IOnMouseExit
{
    public GridElement GridElement;
    public MovementPattern MovementPattern;
    public GridIndicator GridIndicatorPrefabAttack;
    public GridIndicator GridIndicatorPrefabDefend;

    private bool bIndicatorsSpawned = false;

    public string deathSound = "DeathSound";

    public void Move(Vector2Int Vector)
    {
        transform.position += new Vector3(Vector.x, Vector.y, 0f);
        GridElement.UpdateInGridManager();

        AudioManager.instance.PlaySound("Move");

        DestroyIndicators();

        if (Global.GridManager.ContainsElementWithTag(GridElement.GetPosition(), "Player"))
        {
            Global.GameManager.Player.Movement.LastCapturedEnemy = this;
            Global.GameManager.Player.Movement.OnCapture.Invoke();
            Global.GameManager.Player.GetComponent<Health>().Damage(1);
            Destroy(gameObject);
        }
    }

    public List<Vector2Int> GetPosition()
    {
        var Tmp = new List<Vector2Int>();
        for (int i = 0; i < transform.lossyScale.x; i++)
        {
            for (int j = 0; j < transform.lossyScale.y; j++)
            {
                Tmp.Add(new Vector2Int(GridElement.GetPosition().x + i, GridElement.GetPosition().y + j));
            }
        }

        return Tmp;
    }

    public List<Vector2Int> GetPossibleMovementPositions()
    {
        var Tmp = new List<Vector2Int>();
        foreach (var Item in GetComponentsInChildren<GridIndicator>())
        {
            Tmp.Add(Item.GridElement.GetPosition());
        }

        return Tmp;
    }

    private void SpawnIndicators()
    {
        DestroyIndicators();

        var AttackIndicators = MovementPattern.GetIndicatorsPositions(GridElement.GetPosition(), GridIndicatorPrefabAttack.IndicatorTags);
        var DefendIndicators = MovementPattern.GetIndicatorsPositions(GridElement.GetPosition(), GridIndicatorPrefabDefend.IndicatorTags);

        foreach (var Item in AttackIndicators)
        {
            Instantiate(GridIndicatorPrefabAttack, Global.Vector2IntToVector3(Item), new Quaternion(), transform);
            DefendIndicators.Remove(Item);
        }

        foreach (var Item in DefendIndicators)
        {
            Instantiate(GridIndicatorPrefabDefend, Global.Vector2IntToVector3(Item), new Quaternion(), transform);
        }
        //MovementPattern.Spawn(GridElement, GridIndicatorPrefab);
    }

    private void DestroyIndicators()
    {
        foreach (var Item in GetComponentsInChildren<GridIndicator>())
        {
            Destroy(Item.gameObject);
        }
    }

    private void ShowIndicators(bool bVisible)
    {
        foreach (var Item in GetComponentsInChildren<GridIndicator>())
        {
            Destroy(Item.gameObject);
        }
    }

    public void OnMouseEnter()
    {
        SpawnIndicators();
    }

    public void OnMouseExit()
    {
        DestroyIndicators();
    }


    private void OnDestroy()
    {
        Global.InputSystem.Unsubscribe(this as IOnMouseEnter);
        Global.InputSystem.Unsubscribe(this as IOnMouseExit);
    }

    private void Awake()
    {
        Global.InputSystem.Subscribe(this as IOnMouseEnter);
        Global.InputSystem.Subscribe(this as IOnMouseExit);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

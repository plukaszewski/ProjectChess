using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IOnMouseEnter, IOnMouseExit
{
    public GridElement GridElement;
    public MovementPattern MovementPattern;
    public GridIndicator GridIndicatorPrefab;

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

    public void OnMouseEnter()
    {
        End();
        MovementPattern.Spawn(GridElement, GridIndicatorPrefab);
    }

    public void OnMouseExit()
    {
        End();
    }

    public void End()
    {
        foreach (var Item in GetComponentsInChildren<GridIndicator>())
        {
            Destroy(Item.gameObject);
        }
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

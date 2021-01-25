using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIndicator : GridIndicator, IClickable, IOnMouseEnter, IOnMouseExit
{
    public Movement MovementComponent;
    private bool Active;

    public void SpawnChildIndicator(Transform Object, MovementIndicator Indicator)
    {
        foreach (Transform Item in Object)
        {
            if (Item.GetComponent<MovementPatternSquare>() != null
                && Global.GameManager.Level.CurrentRoom.IsInLimits(Global.Vector3ToVector2Int(Item.position + Indicator.MovementComponent.transform.position))
                && !Global.GridManager.ContainsElementWithTag(Global.Vector3ToVector2Int(Item.position + Indicator.MovementComponent.transform.position), "Obstacle"))
            {
                var Tmp = Instantiate(Indicator.MovementComponent.MovementIndicatorPrefab, Item.position + Indicator.MovementComponent.transform.position, new Quaternion(), Indicator.MovementComponent.transform);
                Tmp.MovementComponent = Indicator.MovementComponent;
                if (Global.GridManager.ContainsNoElementsWithOtherTag(Global.Vector3ToVector2Int(Item.position + Indicator.MovementComponent.transform.position), "VisualOnly"))
                {
                    SpawnChildIndicator(Item, Tmp);
                }
            }
        }
    }

    protected void Highlight(bool b)
    {
        Active = b;

        if (b)
        {
            Sprite.color = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, .8f);
        }
        else
        {
            Sprite.color = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, .4f);
        }
    }

    public void OnClick(int Button)
    {
        MovementComponent.Move(new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.y));
    }

    private void OnDestroy()
    {
        Global.InputSystem.Unsubscribe(this as IOnMouseEnter);
        Global.InputSystem.Unsubscribe(this as IOnMouseExit);
        Global.InputSystem.Unsubscribe(this as IClickable);
    }

    private void Awake()
    {
        Global.InputSystem.Subscribe(this as IOnMouseEnter);
        Global.InputSystem.Subscribe(this as IOnMouseExit);
        Global.InputSystem.Subscribe(this as IClickable);
        Highlight(false);
    }

    public void OnMouseEnter()
    {
        Highlight(true);
    }

    public void OnMouseExit()
    {
        Highlight(false);
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

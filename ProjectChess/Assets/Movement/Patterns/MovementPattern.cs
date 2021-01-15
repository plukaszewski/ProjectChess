using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern : MonoBehaviour
{
    public void Spawn(Movement MovementComponent)
    {
        foreach (Transform Item in transform)
        {
            if (Item.GetComponent<MovementPatternSquare>() != null 
                && Global.GameManager.Level.CurrentRoom.IsInLimits(Global.Vector3ToVector2Int(Item.position + MovementComponent.transform.position))
                && !Global.GridManager.ContainsElementWithTag(Global.Vector3ToVector2Int(Item.position + MovementComponent.transform.position), "Obstacle"))
            {
                var Tmp = Instantiate(MovementComponent.MovementIndicatorPrefab, Item.position + MovementComponent.transform.position, new Quaternion(), MovementComponent.transform);
                Tmp.MovementComponent = MovementComponent;
                if(Global.GridManager.ContainsNoElementsWithOtherTag(Global.Vector3ToVector2Int(Item.position + MovementComponent.transform.position), "VisualOnly"))
                {
                    SpawnChildIndicator(Item, Tmp);
                }
            }
        }
    }

    private void SpawnChildIndicator(Transform Object, MovementIndicator Indicator)
    {
        foreach (Transform Item in Object)
        {
            if (Item.GetComponent<MovementPatternSquare>() != null 
                && Global.GameManager.Level.CurrentRoom.IsInLimits(Global.Vector3ToVector2Int(Item.position + Indicator.MovementComponent.transform.position)) 
                && !Global.GridManager.ContainsElementWithTag(Global.Vector3ToVector2Int(Item.position + Indicator.MovementComponent.transform.position), "Obstacle"))
            {
                var Tmp = Instantiate(Indicator.MovementComponent.MovementIndicatorPrefab, Item.position + Indicator.MovementComponent.transform.position, new Quaternion(), Indicator.MovementComponent.transform);
                Tmp.MovementComponent = Indicator.MovementComponent;
                if(Global.GridManager.ContainsNoElementsWithOtherTag(Global.Vector3ToVector2Int(Item.position + Indicator.MovementComponent.transform.position), "VisualOnly"))
                {
                    SpawnChildIndicator(Item, Tmp);
                }
            }
        }
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

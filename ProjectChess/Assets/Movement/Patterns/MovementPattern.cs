﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern : MonoBehaviour
{
    public void Spawn(Movement MovementComponent)
    {
        foreach (Transform Item in transform)
        {
            if (Item.GetComponent<MovementPatternSquare>() != null && MovementComponent.Grid.GetComponent<GridManager>().ContainsNoElementsWithOtherTag(Global.Vector3ToVector2Int(Item.position + MovementComponent.transform.position), "VisualOnly"))
            {
                var Tmp = Instantiate(MovementComponent.MovementIndicatorPrefab, Item.position + MovementComponent.transform.position, new Quaternion(), MovementComponent.transform);
                Tmp.MovementComponent = MovementComponent;
                SpawnChildIndicator(Item, Tmp);
            }
        }
    }

    private void SpawnChildIndicator(Transform Object, MovementIndicator Indicator)
    {
        foreach (Transform Item in Object)
        {
            if (Item.GetComponent<MovementPatternSquare>() != null && Indicator.MovementComponent.Grid.GetComponent<GridManager>().ContainsNoElementsWithOtherTag(Global.Vector3ToVector2Int(Item.position + Indicator.MovementComponent.transform.position), "VisualOnly"))
            {
                var Tmp = Instantiate(Indicator.MovementComponent.MovementIndicatorPrefab, Item.position + Indicator.MovementComponent.transform.position, new Quaternion(), Indicator.MovementComponent.transform);
                Tmp.MovementComponent = Indicator.MovementComponent;
                SpawnChildIndicator(Item, Tmp);
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
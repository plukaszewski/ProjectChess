﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern : MonoBehaviour
{
    private GridElement _GridElement;
    private GridIndicator _GridIndicatorPrefab;
    private Movement _MovementComponent;
    private MovementIndicator _MovementIndicatorPrefab;

    private void _SpawnGrid()
    {
        foreach (Transform Item in transform)
        {
            if (Item.GetComponent<MovementPatternSquare>() != null
                && Global.GameManager.Level.CurrentRoom.IsInLimits(Global.Vector3ToVector2Int(Item.position + _GridElement.transform.position))
                && !Global.GridManager.ContainsElementWithTag(Global.Vector3ToVector2Int(Item.position + _GridElement.transform.position), "Obstacle"))
            {
                var Tmp = Instantiate(_GridIndicatorPrefab, Item.position + _GridElement.transform.position, new Quaternion(), _GridElement.transform);
                Tmp.GridElement = _GridElement;
                if (Global.GridManager.ContainsNoElementsWithOtherTag(Global.Vector3ToVector2Int(Item.position + _GridElement.transform.position), "VisualOnly"))
                {
                    Tmp.SpawnChildIndicator(Item, Tmp);
                }
            }
        }
    }

    private void _SpawnMovement()
    {
        foreach (Transform Item in transform)
        {
            if (Item.GetComponent<MovementPatternSquare>() != null
                && Global.GameManager.Level.CurrentRoom.IsInLimits(Global.Vector3ToVector2Int(Item.position + _MovementComponent.transform.position))
                && !Global.GridManager.ContainsElementWithTag(Global.Vector3ToVector2Int(Item.position + _MovementComponent.transform.position), "Obstacle"))
            {
                var Tmp = Instantiate(_MovementIndicatorPrefab, Item.position + _MovementComponent.transform.position, new Quaternion(), _MovementComponent.transform);
                Tmp.MovementComponent = _MovementComponent;
                if (Global.GridManager.ContainsNoElementsWithOtherTag(Global.Vector3ToVector2Int(Item.position + _MovementComponent.transform.position), "VisualOnly"))
                {
                    Tmp.SpawnChildIndicator(Item, Tmp);
                }
            }
        }
    }

    public void Spawn(GridElement GridElement, GridIndicator GridIndicatorPrefab)
    {
        _GridElement = GridElement;
        _GridIndicatorPrefab = GridIndicatorPrefab;
        Global.GlobalObject.DelayFunction(_SpawnGrid);
    }

    public void Spawn(Movement MovementComponent, MovementIndicator MovementIndicatorPrefab)
    {
        _MovementComponent = MovementComponent;
        _MovementIndicatorPrefab = MovementIndicatorPrefab;
        Global.GlobalObject.DelayFunction(_SpawnMovement);
    }

    //public void Spawn(Movement MovementComponent)
    //{
    //    foreach (Transform Item in transform)
    //    {
    //        if (Item.GetComponent<MovementPatternSquare>() != null 
    //            && Global.GameManager.Level.CurrentRoom.IsInLimits(Global.Vector3ToVector2Int(Item.position + MovementComponent.transform.position))
    //            && !Global.GridManager.ContainsElementWithTag(Global.Vector3ToVector2Int(Item.position + MovementComponent.transform.position), "Obstacle"))
    //        {
    //            var Tmp = Instantiate(MovementComponent.MovementIndicatorPrefab, Item.position + MovementComponent.transform.position, new Quaternion(), MovementComponent.transform);
    //            Tmp.MovementComponent = MovementComponent;
    //            if(Global.GridManager.ContainsNoElementsWithOtherTag(Global.Vector3ToVector2Int(Item.position + MovementComponent.transform.position), "VisualOnly"))
    //            {
    //                SpawnChildIndicator(Item, Tmp);
    //            }
    //        }
    //    }
    //}

    //private void SpawnChildIndicator(Transform Object, MovementIndicator Indicator)
    //{
    //    foreach (Transform Item in Object)
    //    {
    //        if (Item.GetComponent<MovementPatternSquare>() != null
    //            && Global.GameManager.Level.CurrentRoom.IsInLimits(Global.Vector3ToVector2Int(Item.position + Indicator.MovementComponent.transform.position))
    //            && !Global.GridManager.ContainsElementWithTag(Global.Vector3ToVector2Int(Item.position + Indicator.MovementComponent.transform.position), "Obstacle"))
    //        {
    //            var Tmp = Instantiate(Indicator.MovementComponent.MovementIndicatorPrefab, Item.position + Indicator.MovementComponent.transform.position, new Quaternion(), Indicator.MovementComponent.transform);
    //            Tmp.MovementComponent = Indicator.MovementComponent;
    //            if (Global.GridManager.ContainsNoElementsWithOtherTag(Global.Vector3ToVector2Int(Item.position + Indicator.MovementComponent.transform.position), "VisualOnly"))
    //            {
    //                SpawnChildIndicator(Item, Tmp);
    //            }
    //        }
    //    }
    //}
}

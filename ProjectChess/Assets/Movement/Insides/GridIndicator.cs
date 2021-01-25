using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridIndicator : MonoBehaviour
{
    public GridElement GridElement;
    public SpriteRenderer Sprite;

    public virtual void SpawnChildIndicator(Transform Object, GridIndicator Indicator)
    {
        foreach (Transform Item in Object)
        {
            if (Item.GetComponent<MovementPatternSquare>() != null
                && Global.GameManager.Level.CurrentRoom.IsInLimits(Global.Vector3ToVector2Int(Item.position + GridElement.transform.position))
                && !Global.GridManager.ContainsElementWithTag(Global.Vector3ToVector2Int(Item.position + GridElement.transform.position), "Obstacle")
                && !Global.GridManager.ContainsElementWithTag(Global.Vector3ToVector2Int(Item.position + GridElement.transform.position), "Enemy"))
            {
                var Tmp = Instantiate(this, Item.position + GridElement.transform.position, new Quaternion(), GridElement.transform);
                Tmp.GridElement = Indicator.GridElement;
                if (Global.GridManager.ContainsNoElementsWithOtherTag(Global.Vector3ToVector2Int(Item.position + GridElement.transform.position), "VisualOnly"))
                {
                    SpawnChildIndicator(Item, Tmp);
                }
            }
        }
    }

    public List<Vector2Int> GetPosition()
    {
        var Tmp = new List<Vector2Int>();
        Tmp.Add(new Vector2Int((int)transform.position.x, (int)transform.position.y));
        return Tmp;
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

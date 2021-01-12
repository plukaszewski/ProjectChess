using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Grid Grid;
    private Dictionary<Vector2Int, List<GridElement>> Elements = new Dictionary<Vector2Int, List<GridElement>>();

    private void Awake()
    {
        if(Grid == null)
        {
            Grid = GetComponent<Grid>();
        }

        Global.GridManager = this;
    }

    public List<GridElement> GetElements(Vector2Int Position)
    {
        if(Elements.ContainsKey(Position))
        {
            return Elements[Position];
        }
        else
        {
            return null;
        }
    }

    public void AddElement(GridElement Object)
    {
        if(!Elements.ContainsKey(Object.GetPosition()))
        {
            Elements.Add(Object.GetPosition(), new List<GridElement>());
        }

        Elements[Object.GetPosition()].Add(Object);
    }

    public void RemoveElement(GridElement Object)
    {
        if (Elements.ContainsKey(Object.GetPosition()))
        {
            Elements[Object.GetPosition()].Remove(Object);
        }
    }

    public bool ContainsElementWithTag(Vector2Int Position, string Tag)
    {
        foreach(var Item in GetElements(Position))
        {
            if(Item.Tags.Contains(Tag))
            {
                return true;
            }
        }

        return false;
    }

    public bool ContainsNoElementsWithOtherTag(Vector2Int Position, string Tag)
    {
        if(GetElements(Position) == null)
        {
            return true;
        }

        foreach (var Item in GetElements(Position))
        {
            if (!Item.Tags.Contains(Tag))
            {
                return false;
            }
        }

        return true;
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

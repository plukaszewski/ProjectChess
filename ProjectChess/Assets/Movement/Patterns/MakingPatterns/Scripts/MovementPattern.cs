using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern : MonoBehaviour
{
    public List<Vector2Int> ValidSquares = new List<Vector2Int>();

    public void Awake()
    {
        foreach(var Item in GetComponentsInChildren<MovementPatternSquare>())
        {
            ValidSquares.Add(new Vector2Int((int)Item.transform.position.x, (int)Item.transform.position.y));
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

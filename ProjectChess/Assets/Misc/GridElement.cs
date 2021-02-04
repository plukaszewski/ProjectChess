using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
    public List<string> Tags = new List<string>();
    public bool Validate;

    private Vector3 Position = new Vector3();
    private Vector2Int GridPosition = new Vector2Int();

    private void UpdatePosition()
    {
        transform.position += transform.position.x >= 0 ? new Vector3(.5f, 0f) : new Vector3(-.5f, 0f);
        transform.position += transform.position.y >= 0 ? new Vector3(0f, .5f) : new Vector3(0f, -.5f);
        transform.position = new Vector3((int)transform.position.x, (int)transform.position.y);
    }

    public void UpdateInGridManager()
    {
        Global.GridManager.RemoveElement(this);
        GridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Global.GridManager.AddElement(this);
    }

    private void OnValidate()
    {
        //runInEditMode = false;
        UpdatePosition();
        Validate = false;
    }

    public Vector2Int GetPosition()
    {
        return GridPosition;
    }

    private void OnDestroy()
    {
        Global.GridManager.RemoveElement(this);
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
        GridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        UpdateInGridManager();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != Position)
        {
            UpdateInGridManager();
            Position = transform.position;
        }
    }
}

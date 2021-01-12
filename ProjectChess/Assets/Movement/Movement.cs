using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public GridManager Grid;
    [SerializeField] public MovementIndicator MovementIndicatorPrefab;
    [SerializeField] public MovementPattern Pattern;

    //@TODO: rename
    public void Initialize()
    {
        Pattern.Spawn(this);
    }

    //Actual movement, change this function to manage animation, leave current lines
    public void Move(Vector2Int Vector)
    {
        //move itself
        transform.position += new Vector3(Vector.x, Vector.y, 0f);

        //removing indicators
        End();
    }

    private void End()
    {
        foreach (var Item in GetComponentsInChildren<MovementIndicator>())
        {
            Destroy(Item.gameObject);
        }
    }

    private void Awake()
    {
        Grid = Global.GridManager;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Initialize();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GridManager Grid;
    [SerializeField] public MovementIndicator MovementindicatorPrefab;
    [SerializeField] public MovementPattern Pattern;
    [SerializeField] public Vector2 Offset;

    private void OnValidate()
    {
        Pattern.ValidSquares.Clear();

        foreach (var Item in Pattern.GetComponentsInChildren<MovementPatternSquare>())
        {
            Pattern.ValidSquares.Add(new Vector2Int((int)Item.transform.position.x, (int)Item.transform.position.y));
        }
    }

    //@TODO: rename
    public void Initialize()
    {
        foreach(var Item in Pattern.ValidSquares)
        {
            MovementIndicator Tmp = Instantiate(MovementindicatorPrefab, new Vector3(transform.position.x + Item.x, transform.position.y + Item.y, 0f), new Quaternion(), transform);

            Tmp.MovementComponent = this;
        }
    }


    public void Move(Vector2Int Vector)
    {
        transform.position += new Vector3(Vector.x + Offset.x, Vector.y + Offset.y, 0f);
        End();
    }

    private void End()
    {
        foreach (var Item in GetComponentsInChildren<MovementIndicator>())
        {
            Destroy(Item.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
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

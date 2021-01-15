using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    [SerializeField] public MovementIndicator MovementIndicatorPrefab;
    [SerializeField] public MovementPattern Pattern;

    public UnityEvent OnMove;
    public UnityEvent OnCapture;

    //@TODO: rename
    public void Initialize()
    {
        End();
        Pattern.Spawn(this);
    }

    public void Move(Vector2Int Vector)
    {
        transform.position += new Vector3(Vector.x, Vector.y, 0f);

        if(Global.GridManager.ContainsElementWithTag(Global.Vector3ToVector2Int(transform.position), "Enemy"))
        {
            foreach(var Item in Global.GridManager.GetElements(Global.Vector3ToVector2Int(transform.position)))
            {
                if(Item.Tags.Contains("Enemy"))
                {
                    Destroy(Item.gameObject);
                    OnCapture.Invoke();
                }
            }
        }

        End();

        OnMove.Invoke();
    }

    public void End()
    {
        foreach (var Item in GetComponentsInChildren<MovementIndicator>())
        {
            Destroy(Item.gameObject);
        }
    }

    private void Awake()
    {
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

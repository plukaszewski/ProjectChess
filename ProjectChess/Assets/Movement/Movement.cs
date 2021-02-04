using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    public class OnCaptureEvent : UnityEvent<Enemy>
    {

    }

    [SerializeField] public MovementIndicator MovementIndicatorPrefab;
    [SerializeField] public MovementPattern MovementPattern;

    public UnityEvent OnMove;
    [SerializeField] public OnCaptureEvent OnCapture = new OnCaptureEvent();

    //@TODO: rename
    public void Initialize()
    {
        End();
        foreach (var Item in MovementPattern.GetIndicatorsPositions(GetComponent<GridElement>().GetPosition(), MovementIndicatorPrefab.IndicatorTags))
        {
            var Tmp = Instantiate(MovementIndicatorPrefab, Global.Vector2IntToVector3(Item), new Quaternion(), transform);
            Tmp.MovementComponent = this;
        }
    }

    public void Move(Vector2Int Vector)
    {
        AudioManager.instance.PlaySound("Move");
        transform.position += new Vector3(Vector.x, Vector.y, 0f);

        if(Global.GridManager.ContainsElementWithTag(Global.Vector3ToVector2Int(transform.position), "Enemy"))
        {

            foreach(var Item in Global.GridManager.GetElements(Global.Vector3ToVector2Int(transform.position)))
            {
                if(Item.Tags.Contains("Enemy"))
                {
                    OnCapture.Invoke(Item.GetComponent<Enemy>());
                    Destroy(Item.gameObject);
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

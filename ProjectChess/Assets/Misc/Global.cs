using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Global : MonoBehaviour
{
    public static Camera Camera;
    public static InputSystem InputSystem;
    public static GridManager GridManager;
    public static Grid Grid;
    public static GameManager GameManager;
    public static Global GlobalObject;
    public static AIController AIController;

    public static Vector2Int Vector3ToVector2Int(Vector3 Vector)
    {
        return new Vector2Int((int)Vector.x, (int)Vector.y);
    }

    public static Vector3 Vector2IntToVector3(Vector2Int Vector)
    {
        return new Vector3(Vector.x, Vector.y);
    }

    private void Awake()
    {
        Grid = FindObjectOfType<Grid>();
        GlobalObject = this;
    }

    private IEnumerator Delay(UnityAction Function, float Seconds)
    {
        yield return new WaitForSeconds(Seconds);
        Function.Invoke();
    }

    private IEnumerator Delay(UnityAction Function)
    {
        yield return new WaitForEndOfFrame();
        Function.Invoke();
    }

    public void DelayFunction(UnityAction Function)
    {
        StartCoroutine(Delay(Function));
    }

    public void DelayFunction(UnityAction Function, float Seconds)
    {
        StartCoroutine(Delay(Function, Seconds));
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

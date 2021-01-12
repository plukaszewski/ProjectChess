using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Camera Camera;
    public static InputSystem InputSystem;
    public static GridManager GridManager;

    public static Vector2Int Vector3ToVector2Int(Vector3 Vector)
    {
        return new Vector2Int((int)Vector.x, (int)Vector.y);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int Position;
    public InputSystem InputSystem;
    public PlayerSpawn PlayerSpawn;
    public Vector4 Limits;

    public Entrance[] Entrances;

    public bool IsInLimits(Vector2Int Position)
    {
        if (Position.x > transform.position.x + Limits.x || Position.x < Limits.z + transform.position.x || Position.y > Limits.y + transform.position.y || Position.y < Limits.w + transform.position.y)
        {
            return false;
        }

        return true;
    }

    public void EnableEntrances(bool b)
    {
        foreach(var Item in Entrances)
        {
            if(Item != null)
            {
                Item.SetActive(b);
                Item.gameObject.SetActive(b);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EnableEntrances(true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            EnableEntrances(false);
        }
    }
}

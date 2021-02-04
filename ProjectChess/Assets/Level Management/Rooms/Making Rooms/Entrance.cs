using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour, IClickable
{
    public Vector2Int Direction;
    private bool b = true;

    List<Vector2Int> IInputSystemInerface.GetPosition()
    {
        var Tmp = new List<Vector2Int>();
        for(int i = 0; i < transform.lossyScale.x; i++)
        {
            for(int j = 0; j < transform.lossyScale.y; j++)
            {
                Tmp.Add(new Vector2Int((int)transform.position.x + i, (int)transform.position.y + j));
            }
        }
        return Tmp;
    }

    void _OnClick()
    {
        Global.GameManager.Level.ChangeRoom(Direction);
        b = true;
    }

    void IClickable.OnClick(int Button)
    {
        if(b)
        {
            Global.GlobalObject.DelayFunction(_OnClick);
            b = false;
        }
    }

    private void OnDestroy()
    {
        Global.InputSystem.Unsubscribe(this);
    }

    public void SetActive(bool b)
    {
        if(b)
        {
            Global.InputSystem.Subscribe(this);
        }
        else
        {
            Global.InputSystem.Unsubscribe(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Global.InputSystem.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

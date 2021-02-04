using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour, IClickable
{
    public Vector2Int Direction;
    private bool b = true;
    public GameObject OpenDoors;
    public GameObject ClosedDoors;

    public void Open()
    {
        OpenDoors.SetActive(true);
        ClosedDoors.SetActive(false);
        Global.InputSystem.Subscribe(this);
    }

    public void Close()
    {
        OpenDoors.SetActive(false);
        ClosedDoors.SetActive(true);
        Global.InputSystem.Unsubscribe(this);
    }

    List<Vector2Int> IInputSystemInerface.GetPosition()
    {
        var Tmp = new List<Vector2Int>();

        Tmp.Add(new Vector2Int((int)transform.position.x, (int)transform.position.y));
        Tmp.Add(new Vector2Int((int)transform.position.x + Direction.y, (int)transform.position.y - Direction.x));

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
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

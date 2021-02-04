using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public MovementPattern Pattern;
    public int MaxCharges;
    [SerializeField] private int _CurrentCharges;
    public int CurrentCharges
    {
        get { return _CurrentCharges; }
        set
        {
            if (value >= MaxCharges)
            {
                _CurrentCharges = MaxCharges;
            }
            else
            {
                _CurrentCharges = value;
            }
        }
    }

    public void Select()
    {
        if (CurrentCharges > 0 && Global.GameManager.IsPlayerTurn)
        {
            Global.InventoryManager.Select(this);
        }
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

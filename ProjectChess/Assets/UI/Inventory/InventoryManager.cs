using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryButton[] Buttons;
    public InventoryButton SelectedButton;
    private MovementPattern BasePattern;

    public void OnCapture()
    {
        foreach (var Item in Buttons)
        {
            if(Item.Pattern == Global.GameManager.Player.Movement.LastCapturedEnemy.MovementPattern)
            {
                Item.CurrentCharges++;
            }
        }
    }

    public void Select(InventoryButton Button)
    {
        if (SelectedButton == null)
        {
            BasePattern = Global.GameManager.Player.Movement.MovementPattern;
            SelectedButton = Button;
            Global.GameManager.Player.Movement.MovementPattern = Button.Pattern;
        }
        else if (SelectedButton == Button)
        {
            SelectedButton = null;
            Global.GameManager.Player.Movement.MovementPattern = BasePattern;
        }
        else
        {
            SelectedButton = Button;
            Global.GameManager.Player.Movement.MovementPattern = Button.Pattern;
        }

        Global.GameManager.Player.Movement.Refresh();
    }

    public void OnMove()
    {
        if(SelectedButton == null)
        {
            return;
        }

        if (Global.GameManager.Player.Movement.MovementPattern == SelectedButton.Pattern)
        {
            SelectedButton.CurrentCharges--;
            SelectedButton = null;
            Global.GameManager.Player.Movement.MovementPattern = BasePattern;
        }
    }

    private void Setup()
    {
        Global.GameManager.Player.Movement.OnCapture.AddListener(OnCapture);
        Global.GameManager.Player.Movement.OnMove.AddListener(OnMove);
    }

    // Start is called before the first frame update
    void Start()
    {
        Global.GlobalObject.DelayFunction(Setup);
        Global.InventoryManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

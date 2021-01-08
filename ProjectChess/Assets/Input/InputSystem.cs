﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private Grid Grid;
    //[SerializeField] private Vector2Int GridSize;
    private List<IOnMouseEnter> OnMouseEnterObjects = new List<IOnMouseEnter>();
    private List<IOnMouseExit> OnMouseExitObjects = new List<IOnMouseExit>();
    private List<IClickable> ClickableObjects = new List<IClickable>();
    public Vector2Int MousePosition;

    private void Awake()
    {
        if (Grid == null)
        {
            Grid = GetComponent<Grid>();
        }

        PublicValues.InputSystem = this;
    }

    public void Subscribe(IOnMouseEnter Object)
    {
        OnMouseEnterObjects.Add(Object);
    }

    public void Subscribe(IOnMouseExit Object)
    {
        OnMouseExitObjects.Add(Object);
    }

    public void Subscribe(IClickable Object)
    {
        ClickableObjects.Add(Object);
    }

    public void Unubscribe(IOnMouseEnter Object)
    {
        OnMouseEnterObjects.Remove(Object);
    }

    public void Unubscribe(IOnMouseExit Object)
    {
        OnMouseExitObjects.Remove(Object);
    }

    public void Unubscribe(IClickable Object)
    {
        ClickableObjects.Remove(Object);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Tmp = PublicValues.Camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        Vector2Int NewMousePosition = new Vector2Int((int)Tmp.x, (int)Tmp.y);

        if (MousePosition != NewMousePosition)
        {
            foreach (var Item in OnMouseExitObjects)
            {
                if (Item.GetPosition() == MousePosition)
                    Item.OnMouseExit();
            }

            foreach (var Item in OnMouseEnterObjects)
            {
                if (Item.GetPosition() == NewMousePosition)
                    Item.OnMouseEnter();
            }
        }

        MousePosition = NewMousePosition;

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(1);
            foreach (var Item in ClickableObjects)
            {
                if (Item.GetPosition() == MousePosition)
                    Item.OnClick(0);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (var Item in ClickableObjects)
            {
                if (Item.GetPosition() == MousePosition)
                    Item.OnClick(1);
            }
        }
    }
}
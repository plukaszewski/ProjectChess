using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIndicator : MonoBehaviour, IOnMouseEnter, IOnMouseExit, IClickable
{
    public Movement MovementComponent;
    [SerializeField] private SpriteRenderer Sprite;
    private bool Active;

    private void Highlight(bool b)
    {
        Active = b;

        if(b)
        {
            Sprite.color = new Color(1f, 1f, 1f, .8f);
        }
        else
        {
            Sprite.color = new Color(1f, 1f, 1f, .4f);
        }
    }

    public void OnClick(int Button)
    {
        Debug.Log(2);
        MovementComponent.Move(new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.y));
    }

    public void OnMouseEnter()
    {
        Highlight(true);
    }

    public void OnMouseExit()
    {
        Highlight(false);
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        PublicValues.InputSystem.Subscribe(this as IOnMouseEnter);
        PublicValues.InputSystem.Subscribe(this as IOnMouseExit);
        Highlight(false);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}

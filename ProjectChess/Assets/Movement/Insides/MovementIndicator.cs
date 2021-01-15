using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIndicator : MonoBehaviour, IOnMouseEnter, IOnMouseExit, IClickable
{
    public Movement MovementComponent;
    [SerializeField] public SpriteRenderer Sprite;
    private bool Active;

    private void Highlight(bool b)
    {
        Active = b;

        if(b)
        {
            Sprite.color = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, .8f);
        }
        else
        {
            Sprite.color = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, .4f);
        }
    }

    public void OnClick(int Button)
    {
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

    public List<Vector2Int> GetPosition()
    {
        var Tmp = new List<Vector2Int>();
            Tmp.Add(new Vector2Int((int)transform.position.x, (int)transform.position.y));
        return Tmp;
    }

    private void OnDestroy()
    {
        Global.InputSystem.Unsubscribe(this as IOnMouseEnter);
        Global.InputSystem.Unsubscribe(this as IOnMouseExit);
        Global.InputSystem.Unsubscribe(this as IClickable);
    }

    private void Awake()
    {
        Global.InputSystem.Subscribe(this as IOnMouseEnter);
        Global.InputSystem.Subscribe(this as IOnMouseExit);
        Global.InputSystem.Subscribe(this as IClickable);
        Highlight(false);


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

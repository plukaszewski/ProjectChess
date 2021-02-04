using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public interface IUIBarReadable
{
    float GetMaxValue();
    float GetCurrentValue();
    [SerializeField] UnityEvent OnValueChange { get; }
}

public class UIBar : MonoBehaviour
{
    [SerializeField] public System.Type Type;
    public MonoBehaviour SourceAsGameObject;
    public int ComponentNumber;
    public IUIBarReadable Source;
    public Image Max;
    public Image MaxEnd;
    public Sprite BarMaxElementSprite;
    public Sprite BarMaxElementEndSprite;

    public Image Current;
    public Image CurrentEnd;
    public Sprite BarCurrentElementSprite;
    public Sprite BarCurrentElementEndSprite;

    public Vector2 ElementSize;

    void OnValueChange()
    {
        Debug.Log(1);
        Current.rectTransform.offsetMax = new Vector2(Source.GetCurrentValue(), 0f) * ElementSize;
    }

    void Setup()
    {
        Source = Global.GameManager.Player.GetComponent<IUIBarReadable>();

        Max.sprite = BarMaxElementSprite;
        MaxEnd.sprite = BarMaxElementEndSprite;
        GetComponent<RectTransform>().offsetMax = new Vector2(Source.GetMaxValue(), 0f) * ElementSize;
        Max.rectTransform.offsetMax = new Vector2(Source.GetMaxValue(), 0f) * ElementSize;

        Current.sprite = BarCurrentElementSprite;
        CurrentEnd.sprite = BarCurrentElementEndSprite;
        Current.rectTransform.offsetMax = new Vector2(Source.GetCurrentValue(), 0f) * ElementSize;

        Source.OnValueChange.AddListener(OnValueChange);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setup();
        Global.GlobalObject.DelayFunction(Setup);
    }

    // Update is called once per frame
    void Update()
    {
    }
}

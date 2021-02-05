using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IUIBarReadable
{
    public float MaxHealth;
    public float CurrentHealth;

    //IUIBarReadable
    public float GetMaxValue()
    {
        return MaxHealth;
    }

    public float GetCurrentValue()
    {
        return CurrentHealth;
    }

    UnityEvent _OnValueChange = new UnityEvent();
    public UnityEvent OnValueChange
    {
        get
        {
            return _OnValueChange;
        }
    }
    //~IUIBarReadable

    public float Heal(float Amount)
    {
        if(Amount > 0)
        {
            CurrentHealth += Amount;

            if(CurrentHealth > MaxHealth)
            {
                float Overheal = MaxHealth - CurrentHealth;
                CurrentHealth = MaxHealth;

                OnValueChange.Invoke();

                return Overheal;
            }

            OnValueChange.Invoke();
        }

        return 0;
    }

    public void Damage(float Amount)
    {
        if(Amount > 0)
        {
            CurrentHealth -= Amount;
            AudioManager.instance.PlaySound("DeathSound");
            OnValueChange.Invoke();

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Global.GameManager.Die();
    }

    void Setup()
    {
        OnValueChange.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

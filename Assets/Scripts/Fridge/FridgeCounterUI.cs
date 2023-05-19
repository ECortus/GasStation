using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeCounterUI : CounterUI
{
    [SerializeField] private Fridge fridge;

    protected override float CurrentValue 
    { 
        get
        {
            return fridge.MoneyAmount;
        }
    }
    protected override float MaxValue 
    { 
        get
        {
            return fridge.MaxMoneyAmount;
        }
    }

    public override void Refresh()
    {
        if(fridge.FillAmount == 0)
        {
            counter.text = $"EMPTY!";
            return;
        }

        counter.text = $"{CurrentValue}/{MaxValue}";
    }
}

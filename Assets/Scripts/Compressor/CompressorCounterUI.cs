using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressorCounterUI : CounterUI
{
    [SerializeField] private Compressor comp;

    protected override float CurrentValue 
    { 
        get
        {
            return comp.Amount;
        }
    }
    protected override float MaxValue 
    { 
        get
        {
            return comp.MaxAmount;
        }
    }

    public override void Refresh()
    {
        counter.text = $"{CurrentValue}/{MaxValue}";
    }
}

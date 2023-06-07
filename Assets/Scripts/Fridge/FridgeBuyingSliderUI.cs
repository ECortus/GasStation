using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeBuyingSliderUI : RequireMoneyInteractiveZoneUI
{
    [Space]
    [SerializeField] private Fridge fridge;

    protected override int Level
    {
        get
        {
            return fridge.Level;
        }
    }

    protected override int MaxLevel
    {
        get
        {
            return fridge.MaxLevel;
        }
    }

    protected override List<int> Costs { get { return fridge.CostAmountEachLevel; } }
    
    protected override void Complete()
    {
        fridge.Buy();
    }

    protected override bool ConditionToAllowInter 
    { 
        get
        {
            return Statistics.Money > 0;
        }
    }
}

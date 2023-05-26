using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBuyingSliderUI : RequireMoneyInteractiveZoneUI
{
    [Space]
    [SerializeField] private StationFilling station;

    protected override int Level
    {
        get
        {
            return station.Level;
        }
    }

    protected override int MaxLevel
    {
        get
        {
            return station.MaxLevel;
        }
    }

    protected override List<int> Costs { get { return station.CostAmountEachLevel; } }
    
    protected override void Complete()
    {
        station.Buy();
    }

    protected override bool ConditionToAllowInter 
    { 
        get
        {
            return Statistics.Money > 0;
        }
    }
}

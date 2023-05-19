using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationFillingUpgrade : RequireMoneyInteractiveZoneUI
{
    [Space]
    [SerializeField] private StationFilling station;

    void Update()
    {
        if(station.Level == station.MaxLevel)
        {
            gameObject.SetActive(false);
        }
    }

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
        station.Upgrade();
        Reset();
    }

    protected override bool ConditionToAllowInter 
    { 
        get
        {
            return Statistics.Money > 0;
        }
    }
}

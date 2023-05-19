using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerUpgrade : RequireMoneyInteractiveZoneUI
{
    [Space]
    [SerializeField] private Tanker tanker;

    void Update()
    {
        if(tanker.Level == tanker.MaxLevel)
        {
            gameObject.SetActive(false);
        }
    }

    protected override int Level
    {
        get
        {
            return tanker.Level;
        }
    }

    protected override int MaxLevel
    {
        get
        {
            return tanker.MaxLevel;
        }
    }

    protected override List<int> Costs { get { return tanker.CostAmountEachLevel; } }
    
    protected override void Complete()
    {
        tanker.Upgrade();
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

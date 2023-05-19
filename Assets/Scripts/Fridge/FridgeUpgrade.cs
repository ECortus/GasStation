using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeUpgrade : RequireMoneyInteractiveZoneUI
{
    [Space]
    [SerializeField] private Fridge fridge;

    void Update()
    {
        if(fridge.Level == fridge.MaxLevel)
        {
            gameObject.SetActive(false);
        }
    }

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
        fridge.Upgrade();
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

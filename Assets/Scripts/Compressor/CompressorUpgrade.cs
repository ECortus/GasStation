using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressorUpgrade : RequireMoneyInteractiveZoneUI
{
    [Space]
    [SerializeField] private Compressor compressor;

    void Update()
    {
        if(compressor.Level == compressor.MaxLevel)
        {
            gameObject.SetActive(false);
        }
    }

    protected override int Level
    {
        get
        {
            return compressor.Level;
        }
    }

    protected override int MaxLevel
    {
        get
        {
            return compressor.MaxLevel;
        }
    }

    protected override List<int> Costs { get { return compressor.CostAmountEachLevel; } }
    
    protected override void Complete()
    {
        compressor.Upgrade();
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

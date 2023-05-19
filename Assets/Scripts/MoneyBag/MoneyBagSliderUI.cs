using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBagSliderUI : InteractiveSliderUI
{
    [Space]
    [SerializeField] private MoneyBag bag;

    protected override void Complete()
    {
        bag.PlusToPlayer();
    }

    protected override bool ConditionToAllowInter 
    { 
        get
        {
            return true;
        }
    }
}

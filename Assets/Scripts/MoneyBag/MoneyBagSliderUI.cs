using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBagSliderUI : InteractiveSliderUI
{
    [Space]
    [SerializeField] private MoneyBag bag;
    [SerializeField] private MoneyBagTimer timer;

    protected override void Complete()
    {
        bag.PlusToPlayer();
        timer.On();
    }

    protected override bool ConditionToAllowInter 
    { 
        get
        {
            return true;
        }
    }
}

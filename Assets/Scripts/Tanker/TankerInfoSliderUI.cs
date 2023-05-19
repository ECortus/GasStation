using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerInfoSliderUI : InfoSliderUI
{
    [SerializeField] private Tanker tanker;

    protected override float CurrentValue 
    { 
        get
        {
            return tanker.FillAmount;
        }
    }
    protected override float MaxValue 
    { 
        get
        {
            return tanker.MaxFillAmount;
        }
    }
}

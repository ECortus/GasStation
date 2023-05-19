using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationFillingInfoSliderUI : InfoSliderUI
{
    [SerializeField] private StationFilling station;

    protected override float CurrentValue 
    { 
        get
        {
            return station.FillAmount;
        }
    }
    protected override float MaxValue 
    { 
        get
        {
            return station.MaxFillAmount;
        }
    }
}

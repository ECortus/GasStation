using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillStationFillingSliderUI : InteractiveSliderUI
{
    [Space]
    [SerializeField] private StationFilling station;

    protected override void Complete()
    {
        station.Fill();
    }

    protected override bool ConditionToAllowInter 
    { 
        get
        {
            return station.FillAmount < station.MaxFillAmount && station.tanker.FillAmount > 0;
        }
    }
}

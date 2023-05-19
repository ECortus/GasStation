using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillTankerSliderUI : InteractiveSliderUI
{
    [Space]
    [SerializeField] private Tanker tanker;
    [SerializeField] private GasTrucksController trucksController;
    
    protected override void Complete()
    {
        /* tanker.Fill(); */
        trucksController.CallToFill();
    }

    protected override bool ConditionToAllowInter 
    { 
        get
        {
            return /* tanker.FillAmount < tanker.MaxFillAmount && */ !trucksController.Called;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPriceStandCollision : InteractiveSliderUI
{
    [SerializeField] private GameObject menu;

    protected override void Complete()
    {
        menu.SetActive(true);
    }

    protected override bool ConditionToAllowInter => true;
}

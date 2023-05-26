using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacesPrice : MonoBehaviour
{
    public Transform happy, angry;

    [Space]
    [SerializeField] private GasPriceInfo info;
    [SerializeField] private GasSalePriceSliderUI slider;

    [Space]
    [SerializeField] private float minScale;
    [SerializeField] private float scale;

    private float Percent
    {
        get
        {
            return slider.ValuesRatio / info.MaxPercent;
        }
    }

    public void Refresh()
    {
        happy.localScale = Vector3.one * minScale + Vector3.one * scale * (1f - Percent);
        angry.localScale = Vector3.one * minScale + Vector3.one * scale * (Percent);
    }
}

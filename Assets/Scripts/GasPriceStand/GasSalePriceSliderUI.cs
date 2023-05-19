using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasSalePriceSliderUI : MonoBehaviour
{
    [SerializeField] private GasPriceInfo info;
    [SerializeField] private Slider slider;

    public void Refresh()
    {
        slider.minValue = info.MinPrice;
        slider.maxValue = info.MaxPrice;

        slider.value = info.GasSalePrice;
    }
}

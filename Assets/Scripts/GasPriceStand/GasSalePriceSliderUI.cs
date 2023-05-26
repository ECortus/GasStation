using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasSalePriceSliderUI : MonoBehaviour
{
    [SerializeField] private GasPriceInfo info;
    [SerializeField] private FacesPrice faces;
    [SerializeField] private Slider slider;

    public float ValuesRatio
    {
        get
        {
            float value = (info.GasSalePrice - info.GasBuyPrice) / info.GasBuyPrice;
            return value > 0f ? value : 0f;
        }
    }

    public void Refresh()
    {
        slider.minValue = 0f;
        slider.maxValue = info.MaxPercent;

        slider.value = ValuesRatio;

        faces.Refresh();
    }
}

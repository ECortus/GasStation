using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GasPriceInfoUI : MonoBehaviour
{
    [SerializeField] private GasPriceInfo info;
    [SerializeField] private GasSalePriceSliderUI saleSlider;

    public float ValuesRatio
    {
        get
        {
            return saleSlider.ValuesRatio;
        }
    }

    [Space]
    [SerializeField] private TextMeshProUGUI salePriceInStand;
    [SerializeField] private TextMeshProUGUI salePrice;
    [SerializeField] private TextMeshProUGUI buyPrice;

    float amount 
    {
        get
        {
            return System.MathF.Round((info.MaxPrice - info.GasBuyPrice) * 0.05f, 2);
        }
    }

    void Start()
    {

    }

    public void Refresh()
    {
        salePriceInStand.text = $"{ConvertFloatToText(info.GasSalePrice)}";
        salePrice.text = $"{ConvertFloatToText(info.GasSalePrice)}";
        buyPrice.text = $"{ConvertFloatToText(info.GasBuyPrice)}";

        saleSlider.Refresh();
    }

    string ConvertFloatToText(float value)
    {
        int wholePart = (int)value;
        int leftPart = (int)((value % 1f) * 100f);

        if(leftPart.ToString().Length < 2) return $"{wholePart}.0{leftPart}$";
        else return $"{wholePart}.{leftPart}$";
    }

    public void EqualSaleAndBuy()
    {
        info.EqualSaleAndBuy();
        Refresh();
    }

    public void IncreaseSale()
    {
        float value = amount;
        info.IncreaseSalePrice(value);
        Refresh();
    }

    public void DecreaseSale()
    {
        float value = amount;
        info.DecreaseSalePrice(value);
        Refresh();
    }

    public void IncreaseBuy(float amount)
    {
        info.IncreaseBuyPrice(amount);
        Refresh();
    }

    public void DecreaseBuy(float amount)
    {
        info.DecreaseBuyPrice(amount);
        Refresh();
    }
}

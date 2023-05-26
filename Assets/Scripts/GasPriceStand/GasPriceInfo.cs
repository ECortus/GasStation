using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPriceInfo : MonoBehaviour
{
    public static GasPriceInfo Instance { get; set; }
    void Awake() => Instance = this;

    [Space]
    [Range(0f, 1f)]
    public float MaxPercent = 0.2f;

    public float GasSalePrice
    {
        get
        {
            return PlayerPrefs.GetFloat(DataManager.GasSalePriceKey, MinPrice + GasBuyPrice * 0.1f);
        }
        set
        {
            PlayerPrefs.SetFloat(DataManager.GasSalePriceKey, value);
            PlayerPrefs.Save();
        }
    }

    public float GasBuyPrice
    {
        get
        {
            return PlayerPrefs.GetFloat(DataManager.GasBuyPriceKey, MinPrice);
        }
        set
        {
            PlayerPrefs.SetFloat(DataManager.GasBuyPriceKey, value);
            PlayerPrefs.Save();
        }
    }

    public float MaxPrice
    {
        get
        {
            return GasBuyPrice * (1f + MaxPercent);
        }
    }

    public float MinPrice
    {
        get
        {
            return 5f;
        }
    }

    public void IncreaseSalePrice(float amount)
    {
        float value = GasSalePrice + amount;
        if(value > MaxPrice) GasSalePrice = MaxPrice;
        else GasSalePrice = value;
    }

    public void DecreaseSalePrice(float amount)
    {
        float value = GasSalePrice - amount;
        if(value < MinPrice) GasSalePrice = MinPrice;
        else GasSalePrice = value;
    }

    public void IncreaseBuyPrice(float amount)
    {
        float value = GasBuyPrice + amount;
        if(value > MaxPrice) GasBuyPrice = MaxPrice;
        else GasBuyPrice = value;
    }

    public void DecreaseBuyPrice(float amount)
    {
        float value = GasBuyPrice - amount;
        if(value < MinPrice) GasBuyPrice = MinPrice;
        else GasBuyPrice = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker : MonoBehaviour
{
    int _level = 0;
    public int MaxLevel = 5;
    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }

    public void Upgrade()
    {
        int value = Level + 1;
        Level = value;

        slider.Refresh();

        /* Fill(); */
    }

    [Space]
    [SerializeField] private List<int> MaxFillAmountEachLevel = new List<int>();
    public List<int> CostAmountEachLevel = new List<int>();
    [HideInInspector] public int FillAmount;
    public int MaxFillAmount
    {
        get
        {
            return MaxFillAmountEachLevel[Level];
        }
    }

    [Space]
    [SerializeField] private TankerInfoSliderUI slider;

    void Start()
    {
        slider.Refresh();
    }

    public void Fill()
    {
        FillAmount = MaxFillAmount;
        slider.Refresh();
    }

    public void FillValue(int value)
    {
        FillAmount += value;
        slider.Refresh();
    }
    
    public void TransferToStationFilling(StationFilling station)
    {
        if(FillAmount == 0)
        {
            return;
        }
        else if(station.FillAmount + FillAmount > station.MaxFillAmount)
        {
            FillAmount -= station.MaxFillAmount - station.FillAmount;
            station.FillAmount = station.MaxFillAmount;
        }
        else
        {
            station.FillAmount += FillAmount;
            FillAmount = 0;
        }

        slider.Refresh();
    }
}

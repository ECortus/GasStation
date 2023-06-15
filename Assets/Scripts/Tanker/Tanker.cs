using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker : MonoBehaviour
{
    private string Name => gameObject.name;

    public int MaxLevel = 5;
    public int Level
    {
        get
        {
            return PlayerPrefs.GetInt(Name + "Level", 0);
        }
        set
        {
            PlayerPrefs.SetInt(Name + "Level", value);
            PlayerPrefs.Save();
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
    public int FillAmount
    {
        get
        {
            return PlayerPrefs.GetInt(Name + "Fill", 0);
        }
        set
        {
            PlayerPrefs.SetInt(Name + "Fill", value);
            PlayerPrefs.Save();
        }
    }
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

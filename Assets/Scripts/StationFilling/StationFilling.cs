using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationFilling : MonoBehaviour
{
    private string Name => gameObject.name;

    [SerializeField] private int SetupLevel = 0;
    public int MaxLevel = 5;
    [SerializeField] private List<int> MaxFillAmountEachLevel = new List<int>();
    public List<int> CostAmountEachLevel = new List<int>();
    /* [SerializeField] private int amountUpPerLevel = 1; */
    public int Level
    {
        get
        {
            return PlayerPrefs.GetInt(Name + "Level", SetupLevel);
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
    }

    /* [SerializeField] private int defaultMaxFillAmount; */
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
            return Level > -1 ? MaxFillAmountEachLevel[Level] : 0;
            /* return defaultMaxFillAmount + (amountUpPerLevel * Level); */
        }
    }

    private int MoneyAmount = 0;

    [Space]
    public Tanker tanker;
    public Transform barrelRopeParent;
    [SerializeField] private StationFillingCashStorage cashStorage;
    [SerializeField] private StationFillingInfoSliderUI slider;
    [SerializeField] private GameObject worker;
    public bool WorkerActive => worker.activeSelf;

    [Space]
    [SerializeField] private Animation sell;
    [SerializeField] private Animation buy;

    void Start()
    {
        if(SetupLevel > -2 && Level <= SetupLevel)
        {
            Level = SetupLevel;
        }

        if(Level > -1)
        {
            buy.Play("ShowConstruction");
            sell.Play("HideConstruction");
        }
        else
        {
            buy.Play("HideConstruction");
            sell.Play("ShowConstruction");
        }

        slider.Refresh();
    }

    public void Buy()
    {
        buy.Play("ShowConstruction");
        sell.Play("HideConstruction");

        Upgrade();
    }

    public void AddToAmount(int amnt)
    {
        MoneyAmount += amnt;
        AddToStorage();
    }

    public void AddToStorage()
    {
        cashStorage.AddCash();
    }

    public void Transfer()
    {
        if(MoneyAmount > 0)
        {
            cashStorage.TransferAllToPlayer();
            Money.Plus(MoneyAmount);
            MoneyAmount = 0;

            /* counter.Refresh(); */
        }
    }

    public void Fill()
    {
        tanker.TransferToStationFilling(this);
        slider.Refresh();
    }

    public bool TransferToClient(int amount)
    {
        if(FillAmount == 0)
        {
            return false;
        }
        else if(FillAmount - amount < 0)
        {
            return false;
        }
        else
        {
            FillAmount -= amount;
        }

        slider.Refresh();
        return true;
    }
}

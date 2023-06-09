using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    private string Name => gameObject.name;

    public int SetupLevel = 0;
    public int MaxLevel = 5;
    /* [SerializeField] private float delayDownPerLevel = 1f; */
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
    }

    [Space]
    [SerializeField] private int defaultMaxAmount;
    [HideInInspector] public int MoneyAmount;
    public int MaxMoneyAmount
    {
        get
        {
            return defaultMaxAmount;
        }
    }

    [SerializeField] private int defaultMaxFillAmount;
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
            return defaultMaxFillAmount;
        }
    }

    [Space]
    [SerializeField] private int minAmountPerDelay;
    [SerializeField] private int maxAmountPerDelay;
    private int amountPerDelay
    {
        get
        {
            return Random.Range(minAmountPerDelay, maxAmountPerDelay + 1);
        }
    }

    [Space]
    [SerializeField] private List<float> DelayEachLevel = new List<float>();
    public List<int> CostAmountEachLevel = new List<int>();
    /* [SerializeField] private float defaultDelay = 10f; */
    private float Delay
    {
        get
        {
            return DelayEachLevel[Level];
            /* return defaultDelay - delayDownPerLevel * Level; */
        }
    }
    WaitForSeconds wait => new WaitForSeconds(Delay);

    [Space]
    [SerializeField] private FridgeCashStorage storage;
    [SerializeField] private FridgeCounterUI counter;
    [SerializeField] private FridgeTriggerCollision trigger;
    [SerializeField] private Jars jars;

    [Space]
    [SerializeField] private Animation sell;
    [SerializeField] private Animation buy;

    public void Buy()
    {
        buy.Play("ShowConstruction");
        sell.Play("HideConstruction");

        Upgrade();
    }

    Coroutine coroutine;

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

        StartWork();
        counter.Refresh();

        if(FillAmount > 0) jars.Refresh();
    }

    public void StartWork()
    {
        if(coroutine == null) coroutine = StartCoroutine(Work());
    }

    public void StopWork()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Work()
    {
        yield return null;

        while(true)
        {
            if(MoneyAmount < MaxMoneyAmount && FillAmount > 0)
            {
                yield return wait;
                AddCash();
            }
            else
            {
                yield return null;
            }
        }
    }

    public void AddCash()
    {
        FillAmount--;
        jars.Refresh();

        MoneyAmount += amountPerDelay;
        if(MoneyAmount > MaxMoneyAmount) MoneyAmount = MaxMoneyAmount;

        storage.AddCash();
        if(!trigger.SomeoneInTrigger) counter.Refresh();
    }

    public void TransferMoney()
    {
        if(MoneyAmount > 0)
        {
            storage.TransferAllToPlayer();
            Money.Plus(MoneyAmount);
            MoneyAmount = 0;

            counter.Refresh();
        }
    }

    public void AddAmount(int amnt)
    {
        FillAmount += amnt;

        jars.Refresh();
        counter.Refresh();
    }
}

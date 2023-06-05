using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compressor : MonoBehaviour
{
    int _level = 0;
    public int MaxLevel = 5;
    /* [SerializeField] private float delayDownPerLevel = 1f; */
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

        counter.Refresh();
    }

    [Space]
    [SerializeField] private List<int> MaxAmounts;
    [HideInInspector] public int Amount;
    public int MaxAmount
    {
        get
        {
            return MaxAmounts[Level];
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
    [SerializeField] private List<int> DelayEachLevel = new List<int>();
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
    [SerializeField] private CompressorCashStorage storage;
    [SerializeField] private CompressorCounterUI counter;
    [SerializeField] private CompressorTriggerCollision trigger;

    Coroutine coroutine;

    void Start()
    {
        StartWork();
        counter.Refresh();
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
            if(Amount < MaxAmount)
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
        Amount += amountPerDelay;
        if(Amount > MaxAmount) Amount = MaxAmount;

        storage.AddCash();
        if(!trigger.SomeoneInTrigger) counter.Refresh();
    }

    public void Transfer()
    {
        if(Amount > 0)
        {
            storage.TransferAllToPlayer();
            Money.Plus(Amount);
            Amount = 0;

            counter.Refresh();
        }
    }
}

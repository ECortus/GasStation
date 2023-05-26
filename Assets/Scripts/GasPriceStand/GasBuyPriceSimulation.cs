using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBuyPriceSimulation : MonoBehaviour
{
    public static GasBuyPriceSimulation Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private GasPriceInfo info;
    [SerializeField] private GasPriceInfoUI infoUI;

    [SerializeField] private float minIncrease = 0.17f;
    [SerializeField] private float maxIncrease = 0.23f;
    private float IncreaseAmount => info.GasBuyPrice * (Random.Range(minIncrease, maxIncrease));

    [Space]
    [SerializeField] private float minDecrease = 0.03f;
    [SerializeField] private float maxDecrease = 0.06f;
    private float DecreaseAmount => info.GasBuyPrice * (Random.Range(minDecrease, maxDecrease));

    public float ValuesRatio
    {
        get
        {
            return infoUI.ValuesRatio;
        }
    }

    [Space]
    /* [SerializeField]  */private float delayBetweenCrease = 60f;

    Coroutine coroutine;

    void Start()
    {
        StartSim();
        infoUI.Refresh();
    }

    public void StartSim()
    {
        /* if(coroutine == null) coroutine = StartCoroutine(Work()); */
    }

    public void StopSim()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator Work()
    {
        WaitForSeconds wait = new WaitForSeconds(delayBetweenCrease);
        int value = 0;

        while(true)
        {
            yield return wait;

            value = Random.Range(0, 100);

            if(value > 50)
            {
                Increase();
            }
            else
            {
                Decrease();
            }
        }
    }

    public void Change()
    {
        int value = Random.Range(0, 100);

        if(value > 25)
        {
            Increase();
        }
        else
        {
            Decrease();
        }
    }

    public void Increase()
    {
        infoUI.IncreaseBuy(IncreaseAmount);
    }

    public void Decrease()
    {
        infoUI.DecreaseBuy(DecreaseAmount);
    }
}

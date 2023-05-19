using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBuyPriceSimulation : MonoBehaviour
{
    [SerializeField] private GasPriceInfoUI infoUI;

    [SerializeField] private float minIncrease = 0.5f;
    [SerializeField] private float maxIncrease = 3f;
    private float IncreaseAmount => Random.Range(minIncrease, maxIncrease);

    [Space]
    [SerializeField] private float minDecrease = 0.1f;
    [SerializeField] private float maxDecrease = 0.5f;
    private float DecreaseAmount => Random.Range(minDecrease, maxDecrease);

    [Space]
    [SerializeField] private float delayBetweenCrease = 60f;

    Coroutine coroutine;

    void Start()
    {
        StartSim();
    }

    public void StartSim()
    {
        if(coroutine == null) coroutine = StartCoroutine(Work());
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
            value = Random.Range(0, 100);

            if(value > 50)
            {
                infoUI.IncreaseBuy(IncreaseAmount);
            }
            else
            {
                infoUI.DecreaseBuy(DecreaseAmount);
            }

            yield return wait;
        }
    }
}

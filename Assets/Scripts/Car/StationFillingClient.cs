using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationFillingClient : Car
{
    [Space]
    [Header("Client par-s: ")]
    [SerializeField] private int requireGasAmount;
    /* [SerializeField] private int minReward, maxReward; */
    private int Reward
    {
        get
        {
            return (int)(GasPriceInfo.Instance.GasSalePrice * requireGasAmount);
            /* return Random.Range(minReward, maxReward + 1); */
        }
    }

    [HideInInspector] public StationFillingClientSimulation simulation;
    private StationFilling Station => simulation.Station;

    int DotIndex = 0;

    public void On(Vector3 pos, Vector3 rot)
    {
        transform.position = pos;
        transform.eulerAngles = rot;

        gameObject.SetActive(true);

        DotIndex = 0;
        SetTarget(simulation.GetDotByIndex(DotIndex));

        StartWork();
    }
    public void Off()
    {
        StopWork();
        ResetTarget();

        gameObject.SetActive(false);
    }

    Coroutine coroutine;

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
        while(true)
        {
            if(Vector3.Distance(transform.position, simulation.GetDotByIndex(DotIndex).position) < 0.5f)
            {
                if(DotIndex == simulation.ParkingIndex)
                {
                    yield return StartCoroutine(BuyGas());
                }
                else if(DotIndex == simulation.DotsCount - 1)
                {
                    Off();
                }
                else
                {
                    DotIndex++;
                    SetTarget(simulation.GetDotByIndex(DotIndex));
                }
            }

            yield return null;
        }
    }

    IEnumerator BuyGas()
    {
        SetMotor(0);
        ResetTarget();

        yield return new WaitForSeconds(2f);

        if(requireGasAmount > Station.FillAmount)
        {
            yield return new WaitUntil(() => Station.FillAmount >= requireGasAmount);
        }

        Station.TransferToClient(requireGasAmount);
        Station.AddToAmount(Reward);

        yield return new WaitForSeconds(2f);

        DotIndex++;
        SetTarget(simulation.GetDotByIndex(DotIndex));
        SetMotor(2);
    }
}

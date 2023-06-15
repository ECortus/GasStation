using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTruck : Car
{
    private int ParkingIndex => controller.ParkingIndex;
    private WayInfo way => controller.way;
    private TankerBarrel barrel => controller.barrel;

    private List<Transform> Dots => way.Dots;
    int DotIndex = 0;

    private GasTrucksController controller;
    private Tanker tanker => controller.tanker;

    public void On(GasTrucksController conrt, Vector3 pos, Vector3 rot)
    {
        controller = conrt;
        transform.position = pos;
        transform.eulerAngles = rot;

        gameObject.SetActive(true);

        DotIndex = 0;
        SetTarget(Dots[DotIndex]);

        StartWork();
        controller.Called = true;
    }
    public void Off()
    {
        controller.Called = false;

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
            if(Vector3.Distance(transform.position, Dots[DotIndex].position) < 2f)
            {
                if(DotIndex == ParkingIndex)
                {
                    yield return StartCoroutine(SellGas());
                }
                else if(DotIndex == Dots.Count - 1)
                {
                    Off();
                }
                else
                {
                    DotIndex++;
                    SetTarget(Dots[DotIndex]);
                }
            }

            yield return null;
        }
    }

    IEnumerator SellGas()
    {
        SetMotor(0);
        ResetTarget();

        int money = 0;
        int amount = tanker.MaxFillAmount - tanker.FillAmount;
        money = (int)(amount * GasPriceInfo.Instance.GasBuyPrice);

        barrel.enabled = true;
        yield return new WaitUntil(() => barrel.Connected);

        yield return new WaitForSeconds(2f);

        yield return RewardFunctions.Instance.FillTanker(tanker, money, amount);
        /* if(money <= Statistics.Money)
        {
            Money.Minus(money);
            tanker.Fill();

            GasBuyPriceSimulation.Instance.Change();
        }
        else if(Statistics.Money > 0 && money > Statistics.Money)
        {
            money = Statistics.Money;
            amount = (int)(money / GasPriceInfo.Instance.GasBuyPrice);

            Money.Minus(money);
            tanker.FillValue(amount);

            GasBuyPriceSimulation.Instance.Change();
        } */

        barrel.enabled = false;
        yield return new WaitForSeconds(1.5f);

        DotIndex++;
        SetTarget(Dots[DotIndex]);
        SetMotor(2);
    }
}

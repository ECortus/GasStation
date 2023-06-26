using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardFunctions : MonoBehaviour
{
    public static RewardFunctions Instance { get; set; }
    void Awake() => Instance = this;

    public IEnumerator FillTanker(Tanker tanker, int money, int amount)
    {
        yield return null;

        if(money <= Statistics.Money)
        {
            Money.Minus(money);
            tanker.Fill();
        }
        else if(Statistics.Money > 0 && money > Statistics.Money)
        {
            money = Statistics.Money;
            amount = (int)(money / GasPriceInfo.Instance.GasBuyPrice);

            Money.Minus(money);
            tanker.FillValue(amount);
        }

        GasBuyPriceSimulation.Instance.Change();
    }

    public void CallToFillTankerByAD(ref float time, float adDelay, GasTrucksController trucksController)
    {
        trucksController.CallToFill(false);
        time = adDelay;
    }

    public void PlusMoneyFromBag(int amount)
    {
        Money.Plus(amount);
    }

    public void EnableAutoPrice(AutoPrice autoPrice)
    {
        autoPrice.On();
    }
}

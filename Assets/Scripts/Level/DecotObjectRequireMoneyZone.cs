using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecotObjectRequireMoneyZone : MonoBehaviour
{
    [SerializeField] private DecorObject decor;
    private int MoneyAmount;
    [SerializeField] private TextMeshProUGUI amountText;

    private int Cost => decor.Cost;

    private string Tag = "Player";

    Coroutine coroutine;

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        /* if(Level != MaxLevel)
        {
            MoneyAmount = CostOfUpgrade;
            RefreshText();
        }
        else
        {
            amountText.text = $"--";
            this.enabled = false;

            gameObject.SetActive(false);
        } */
        MoneyAmount = Cost;
        RefreshText();
    }

    void RefreshText()
    {
        amountText.text = $"{MoneyAmount}";
    }

    void StartReduce()
    {
        if(coroutine == null) coroutine = StartCoroutine(ReduceRequiredAmount());
    }

    void StopReduce()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator ReduceRequiredAmount()
    {
        int amount = 1;
        int iter = 0;
        yield return null;

        while(true)
        {
            if(!ConditionToAllowInter) break;

            if(Statistics.Money > 10 && MoneyAmount - 10 > 0) amount = 10;
            else amount = 1;

            Money.Minus(amount);
            MoneyAmount -= amount;

            RefreshText();

            if(MoneyAmount <= 0)
            {
                Complete();
                StopReduce();
                break;
            }

            yield return new WaitForSeconds(0.075f / (1f + iter / 8f));
            iter++;
        }        
    }

    bool ConditionToAllowInter
    {
        get
        {
            return Statistics.Money > 0;
        }
    }
    void Complete() 
    {
        decor.Buy();
        Star.Plus(1);
    }

    void OnTriggerEnter(Collider col)
    {
        if(!ConditionToAllowInter || !this.enabled) return;

        if(col.tag == Tag)
        {
            StartReduce();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(!this.enabled) return;

        if(col.tag == Tag)
        {
            StopReduce();
        }
    }
}

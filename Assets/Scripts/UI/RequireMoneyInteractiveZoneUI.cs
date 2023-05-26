using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RequireMoneyInteractiveZoneUI : MonoBehaviour
{
    protected virtual int Level { get; }
    protected virtual int MaxLevel { get; }
    private int MoneyAmount;

    [Header("Info zone:")]
    [SerializeField] private TextMeshProUGUI amountText;

    protected virtual List<int> Costs { get; }
    /* [SerializeField] private int zeroUpgradeCost;
    [SerializeField] private int costUpPerLevel; */

    public int CostOfUpgrade
    {
        get
        {
            return Costs[Level + 1];
            /* return zeroUpgradeCost + (costUpPerLevel * Level); */
        }
    }

    private string Tag = "Player";

    Coroutine coroutine;

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        if(Level != MaxLevel)
        {
            MoneyAmount = CostOfUpgrade;
            RefreshText();
        }
        else
        {
            amountText.text = $"--";
            this.enabled = false;

            gameObject.SetActive(false);
        }
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

    protected virtual bool ConditionToAllowInter { get; }
    protected virtual void Complete() {}

    void OnTriggerEnter(Collider col)
    {
        if(!ConditionToAllowInter || !this.enabled) return;

        if(col.tag == Tag)
        {
            StartReduce();
        }
    }

    /* void OnTriggerStay(Collider col)
    {
        if(!available || !ConditionToAllowInter) return;

        if(col.tag == Tag)
        {

        }
    } */

    void OnTriggerExit(Collider col)
    {
        if(!this.enabled) return;

        if(col.tag == Tag)
        {
            StopReduce();
        }
    }
}
